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
            Assert.IsTrue(_carMechanicContext.Cars.Count() > 0);
            Assert.IsNotNull(carDtoResult);
            Assert.IsNotNull(carDtoDbResult);
            Assert.AreEqual(carDtoDbResult.LicensePlate, carDtoResult.LicensePlate);
            Assert.AreEqual(carDtoDbResult.Vintage, carDtoResult.Vintage);
        }

        [TestMethod]
        public void TestGetCarById()
        {
        }

        [TestMethod]
        public void TestUpdateCar()
        {
        }

        [TestMethod]
        public void TestDeleteCar()
        {
        }
    }
}