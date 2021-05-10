using System.Collections;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Dto;
using WebApplication.Entities;
using WebApplication.Interfaces;
using WebApplication.Service;

namespace WebApplication.Resources
{
    public class CarResource : CarInterface
    {
        private readonly ICarService _carService;

        public CarResource(ICarService carService)
        {
            _carService = carService;
        }

        public override List<CarDto> GetAll()
        {
            return _carService.GetAll();
        }

        public override ActionResult<CarDto> Save(CarDto car)
        {
            _carService.Save(car);
            return car;
        }

        public override ActionResult<CarDto> GetById(long id)
        {
            return _carService.GetById(id);
        }

        public override CarDto Update(long id, CarDto car)
        {
            return _carService.Update(id, car);
        }

        public override void Delete(long id)
        {
            throw new System.NotImplementedException();
        }
    }
}