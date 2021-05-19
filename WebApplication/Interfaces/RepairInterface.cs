using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Dto;
using WebApplication.Entities;

namespace WebApplication.Interfaces
{
    public abstract class RepairInterface : ControllerBase
    {
        [HttpGet]
        [Route("/repairs")]
        public abstract List<RepairDto> GetAll();

        [HttpGet]
        [Route("/repairs/{guid}")]
        public abstract RepairDto GetByGuid(string guid);

        [HttpPost]
        [Route("/repairs")]
        public abstract RepairDto Save([FromBody] RepairDto repairDto);

        [HttpPut]
        [Route("/repairs/{guid}")]
        public abstract RepairDto Update(string guid, [FromBody] RepairDto repairDto);

        [HttpDelete]
        [Route("/repairs/{id:long}")]
        public abstract void Delete(long id);

        [HttpGet]
        [Route("/repairs/repair")]
        public abstract RepairDto GetByEmailAndPassword([FromQuery(Name = "email")] string email, [FromQuery(Name = "password")] string password);
    }
}