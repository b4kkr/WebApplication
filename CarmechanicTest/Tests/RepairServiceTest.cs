using System;
using System.Linq;
using AutoMapper;
using CarmechanicTest.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApplication.Dto;
using WebApplication.Entities;
using WebApplication.Mapper;
using WebApplication.Repository;
using WebApplication.Service.Impl;

namespace CarmechanicTest
{
    [TestClass]
    public class RepairServiceTest
    {
        
        private CarMechanicContext _carMechanicContext;
        private IMapper _mapper;
        private RepairService _repairService;
        private Repair initialTestRepair;
        
        [TestInitialize]
        public void InitializeTests()
        {
            if (_repairService == null)
            {
                var dbContextOptions = new DbContextOptionsBuilder<CarMechanicContext>().UseInMemoryDatabase("testDB")
                    .Options;
                _carMechanicContext = new CarMechanicContext(dbContextOptions);
                _carMechanicContext.Database.EnsureCreated();

                var mappingConfig = new MapperConfiguration(conf => { conf.AddProfile<CarMechanicMapper>(); });
                _mapper = mappingConfig.CreateMapper();
                _repairService = new RepairService(_mapper, _carMechanicContext);
                
                var testCar = TestConstants.GetTestCar(typeName: "BMW", model: "E46 320i", engineSerial: "N90", licensePlate:"asd123");
                var testUser = TestConstants.GetTestUser(name: "Teszt Felhasznalo", email: "user@mail.hu", phoneNumber: "456789");
                initialTestRepair = TestConstants.GetTestRepair(price: 1000000, paid: false, status: Status.WaitingForParts, works: "Turbo Charger change");
                testCar.User = testUser;
                testUser.Car = testCar;
                testCar.Repair = initialTestRepair;
                initialTestRepair.Car = testCar;
                
                _carMechanicContext.Repairs.Add(initialTestRepair);
                _carMechanicContext.StatusEntities.Add(new StatusEntity() {Status = Status.Finished});
                _carMechanicContext.StatusEntities.Add(new StatusEntity() {Status = Status.AddedForService});
                _carMechanicContext.StatusEntities.Add(new StatusEntity() {Status = Status.WaitingForParts});
                _carMechanicContext.StatusEntities.Add(new StatusEntity() {Status = Status.WorkingOnCarNow});
                
                _carMechanicContext.SaveChanges();
            }
        }

        [TestCleanup]
        public void TestCleanup()
        {
            _carMechanicContext.Database.EnsureDeleted();
        }

        [TestMethod]
        public void TestCreateRepair()
        {
            //Arrange
            var testCar = TestConstants.GetTestCar();
            var testUser = TestConstants.GetTestUser();
            var testRepair = TestConstants.GetTestRepair();
            testCar.User = testUser;
            testUser.Car = testCar;
            testCar.Repair = testRepair;
            testRepair.Car = testCar;
            var repairDto = _mapper.Map<RepairDto>(testRepair);
            //Act
            var repairDtoResult = _repairService.Save(repairDto);
            var repairDtoDbResult =
                _carMechanicContext.Repairs.SingleOrDefault(x => x.Guid == Guid.Parse(repairDto.RepairGuid));

            //Assert
            Assert.IsNotNull(repairDtoResult);
            Assert.IsNotNull(repairDtoDbResult);
            Assert.AreEqual(repairDtoDbResult
                .Guid, Guid.Parse(repairDtoResult.RepairGuid));
            
        }

        [TestMethod]
        public void TestGetRepairById()
        {
            //Act
            var result = _repairService.GetById(initialTestRepair.Id);

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(initialTestRepair.Guid.ToString(), result.RepairGuid);
        }
        [TestMethod]
        public void TestGetRepairByGuid()
        {
            //Act
            var result = _repairService.GetByGuid(initialTestRepair.Guid);

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(initialTestRepair.Id, result.Id);
        }

        [TestMethod]
        public void TestGetByEmailAndPassword()
        {
            var email = initialTestRepair.Car.User.Email;
            var password = initialTestRepair.Car.User.Password;

            var result = _repairService.GetByEmailAndPassword(email, password);

            Assert.IsNotNull(result);
            Assert.AreEqual(initialTestRepair.Guid.ToString(), result.RepairGuid);
        }

        [TestMethod]
        public void TestUpdateRepair()
        {
            //Arrange
            var newCar = TestConstants.GetTestCar("Engine Serial", "licensePlate", "Audi", "S7");
            var newRepair = TestConstants.GetTestRepair(Status.Finished, true, 999999, "Some test works");
            newCar.Vintage = DateTime.Now;
            _carMechanicContext.Cars.Add(newCar);
            _carMechanicContext.SaveChanges();
            var repairDto = _mapper.Map<RepairDto>(newRepair);
            repairDto.Car = _mapper.Map<CarDto>(newCar);
            
            //Act
            var result = _repairService.Update(initialTestRepair.Guid, repairDto);
            //Assert
            Assert.AreEqual(newRepair.Price, result.Price);
            Assert.AreEqual(newRepair.Works, result.Works);
            Assert.AreEqual(newRepair.StatusEntity.Status.ToString(), result.Status);
            Assert.AreEqual(newCar.EngineSerial, result.Car.EngineSerial);
            Assert.AreEqual(newCar.LicensePlate, result.Car.LicensePlate);
        }

        [TestMethod]
        public void TestDeleteRepair()
        {
            //Act
            _repairService.Delete(initialTestRepair.Id);
            
            //Assert
            Assert.IsNull(_carMechanicContext.Repairs.FirstOrDefault(x => x.Id == initialTestRepair.Id));
        }
    }
}