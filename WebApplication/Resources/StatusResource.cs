using System.Collections.Generic;
using WebApplication.Dto;
using WebApplication.Interfaces;
using WebApplication.Service;

namespace WebApplication.Resources
{
    public class StatusResource : StatusInterface
    {
        private readonly IStatusService _service;

        public StatusResource(IStatusService service)
        {
            _service = service;
        }

        public override List<StatusDto> GetAll()
        {
            return _service.GetAll();
        }
    }
}