using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Dto;

namespace WebApplication.Interfaces
{
    public abstract class UserInterface : ControllerBase
    {
        [HttpPost]
        [Route("/users")]
        public abstract UserDto Save([FromBody] UserDto car);

        [HttpGet]
        [Route("/users")]
        public abstract List<UserDto> GetAll();

        [HttpGet]
        [Route("/users/{id:long}")]
        public abstract UserDto GetById(long id);

        [HttpPut]
        [Route("/users/{id:long}")]
        public abstract UserDto Update(long id, [FromBody] UserDto car);

        [HttpDelete]
        [Route("/users/{id:long}")]
        public abstract void Delete(long id);
    }
}