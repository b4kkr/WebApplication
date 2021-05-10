using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Dto;
using WebApplication.Entities;

namespace WebApplication.Interfaces
{
    public abstract class CarInterface : ControllerBase
    {
        [HttpPost]
        [Route("/cars")]
        public abstract ActionResult<CarDto> Save([FromBody] CarDto car);

        [HttpGet]
        [Route("/cars")]
        public abstract List<CarDto> GetAll();

        [HttpGet]
        [Route("/cars/{id:long}")]
        public abstract ActionResult<CarDto> GetById(long id);

        [HttpPut]
        [Route("/cars/{id:long}")]
        public abstract CarDto Update(long id, [FromBody] CarDto car);

        [HttpDelete]
        [Route("/cars/{id:long}")]
        public abstract void Delete(long id);
    }
}