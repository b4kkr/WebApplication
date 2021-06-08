using System;
using System.Linq;
using AutoMapper;
using CarmechanicTest.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApplication.Dto;
using WebApplication.Entities;
using WebApplication.Mapper;
using WebApplication.Service.Impl;
using WebApplication.Repository;

namespace CarmechanicTest
{
    [TestClass]
    public class CarServiceTest
    {
        private CarMechanicContext _carMechanicContext;
        private IMapper _mapper;
        private CarService _carService;
        private Car initialTestCar;

        [TestInitialize]
        public void InitializeTests()
        {
            if (_carService == null)
            {
                var dbContextOptions = new DbContextOptionsBuilder<CarMechanicContext>().UseInMemoryDatabase("testDB")
                    .Options;
                _carMechanicContext = new CarMechanicContext(dbContextOptions);
                _carMechanicContext.Database.EnsureCreated();

                var mappingConfig = new MapperConfiguration(conf => { conf.AddProfile<CarMechanicMapper>(); });
                _mapper = mappingConfig.CreateMapper();
                _carService = new CarService(_carMechanicContext, _mapper);
                
                initialTestCar = TestConstants.GetTestCar(typeName: "BMW", model: "E46 320i", engineSerial: "N90", licensePlate:"asd123");
                            var testUser = TestConstants.GetTestUser(name: "Teszt Felhasznalo", email: "user@mail.hu", phoneNumber: "456789");
                            var testRepair = TestConstants.GetTestRepair(price: 1000000, paid: false, status: Status.WaitingForParts, works: "Turbo Charger change");
                            initialTestCar.User = testUser;
                            testUser.Car = initialTestCar;
                            initialTestCar.Repair = testRepair;
                            testRepair.Car = initialTestCar;
                
                            _carMechanicContext.Cars.Add(initialTestCar);
                            _carMechanicContext.SaveChanges();
            }
        }

        [TestCleanup]
        public void Cleanup()
        {
            _carMechanicContext.Database.EnsureDeleted();
        }

        [TestMethod]
        public void TestCreateCar()
        {
            //Arrange
            var testCar = TestConstants.GetTestCar();
            var testUser = TestConstants.GetTestUser();
            var testRepair = TestConstants.GetTestRepair();
            testCar.User = testUser;
            testUser.Car = testCar;
            testCar.Repair = testRepair;
            testRepair.Car = testCar;
            var carDto = _mapper.Map<CarDto>(testCar);
            //Act
            var carDtoResult = _carService.Save(carDto);
            var carDtoDbResult =
                _carMechanicContext.Cars.SingleOrDefault(x => x.LicensePlate == carDtoResult.LicensePlate);

            //Assert
            Assert.IsNotNull(carDtoResult);
            Assert.IsNotNull(carDtoDbResult);
            Assert.AreEqual(carDtoDbResult.LicensePlate, carDtoResult.LicensePlate);
            Assert.AreEqual(carDtoDbResult.Vintage, carDtoResult.Vintage);
        }

        [TestMethod]
        public void TestGetCarById()
        {
            //Act
            var result = _carService.GetById(initialTestCar.Id);

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(initialTestCar.LicensePlate, result.LicensePlate);
            Assert.AreEqual(initialTestCar.EngineSerial, result.EngineSerial);
        }

        [TestMethod]
        //Updates the initialTestCar with new fields
        public void TestUpdateCar()
        {
            //Arrange
            var newUser = TestConstants.GetTestUser("New Username", "newemail@mail.hu", "06301234567");
            var newCar = TestConstants.GetTestCar("Engine Serial", "licensePlate", "Audi", "S7");
            var newRepair = TestConstants.GetTestRepair(Status.Finished, true, 999999, "Some test works");
            newCar.Vintage = DateTime.Now;
            _carMechanicContext.Repairs.Add(newRepair);
            _carMechanicContext.Users.Add(newUser);
            _carMechanicContext.SaveChanges();
            var carDto = _mapper.Map<CarDto>(newCar);
            carDto.User = _mapper.Map<UserDto>(newUser);
            carDto.User.RepairGuid = newRepair.Guid.ToString();
            //Act
            var result = _carService.Update(initialTestCar.Id, carDto);
            //Assert
            Assert.AreEqual(newCar.EngineSerial, result.EngineSerial);
            Assert.AreEqual(newCar.LicensePlate, result.LicensePlate);
            Assert.AreEqual(newCar.Vintage, result.Vintage);
            Assert.AreEqual(newUser.Email, result.User.Email);
            Assert.AreEqual(newUser.Name, result.User.Name);
            Assert.AreEqual(newUser.Password, result.User.Password);
            Assert.AreEqual(newRepair.Guid.ToString(), result.User.RepairGuid);
        }

        [TestMethod]
        public void TestDeleteCar()
        {
            //Act
            _carService.Delete(initialTestCar.Id);
            
            //Assert
            Assert.IsNull(_carMechanicContext.Cars.FirstOrDefault(x => x.Id == initialTestCar.Id));
        }
    }
}