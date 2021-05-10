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
        public abstract List<Repair> GetAll();

        [HttpGet]
        [Route("/repairs/{id:long}")]
        public abstract RepairDto GetById(long id);

        [HttpPost]
        [Route("/repairs")]
        public abstract RepairDto Save([FromBody] RepairDto repairDto);

        [HttpPut]
        [Route("/repairs/{id:long}")]
        public abstract RepairDto Update(long id, [FromBody] RepairDto repairDto);

        [HttpDelete]
        [Route("/repairs/{id:long}")]
        public abstract void Delete(long id);
    }
}