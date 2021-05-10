using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Dto;
using WebApplication.Entities;
using WebApplication.Interfaces;

namespace WebApplication.Service
{
    public interface ICarService
    {
        CarDto GetById(long id);
        CarDto Save(CarDto car);

        List<CarDto> GetAll();

        CarDto Update(long id, CarDto car);
    }
}