using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Dto;

namespace WebApplication.Interfaces
{
    public abstract class StatusInterface : ControllerBase
    {
        [HttpGet]
        [Route("/statuses")]
        public abstract List<StatusDto> GetAll();
    }
}