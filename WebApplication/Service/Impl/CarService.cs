using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using WebApplication.Dto;
using WebApplication.Entities;
using WebApplication.Repository;

namespace WebApplication.Service.Impl
{
    public class CarService : ICarService
    {

        private readonly CarMechanicContext repository;
        private readonly IMapper carMapper;

        public CarService(CarMechanicContext repository, IMapper mapper)
        {
            this.repository = repository;
            carMapper = mapper;
        }
        
        public CarDto GetById(long id)
        {
            Car car = repository.Cars.FirstOrDefault(x => x.Id == id);
            return carMapper.Map<CarDto>(car);
        }

        public CarDto Save(CarDto car)
        {
            Car c = carMapper.Map<Car>(car);
            repository.Cars.Add(c);
            repository.SaveChanges();
            car.Id = c.Id;
            return car;
        }

        public List<CarDto> GetAll()
        {
            return carMapper.Map<List<CarDto>>(repository.Cars.ToList());
        }

        public CarDto Update(long id, CarDto car)
        {
            Car dbCar = repository.Cars.Find(id);
            dbCar.Vintage = car.Vintage;
            dbCar.LicensePlate = car.LicensePlate;
            dbCar.EngineSerial = car.EngineSerial;
            dbCar.Type.TypeName = car.Type.TypeName;
            dbCar.Type.Model = car.Type.Model;
            dbCar.User = repository.Users.FirstOrDefault(x => x.Id == car.User.Id);
            dbCar.User.Car = dbCar;
            dbCar.Repair = repository.Repairs.FirstOrDefault(x => x.Guid == Guid.Parse(car.User.RepairGuid));
            dbCar.Repair.Car = dbCar;
            repository.SaveChanges();
            return carMapper.Map<CarDto>(dbCar);
        }

        public void Delete(long id)
        {
            var car = repository.Cars.FirstOrDefault(x => x.Id == id);
            if (car != null) repository.Cars.Remove(car);
            repository.SaveChanges();
        }
    }
}