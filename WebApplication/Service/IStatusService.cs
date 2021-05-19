using System.Collections.Generic;
using WebApplication.Dto;
using WebApplication.Entities;

namespace WebApplication.Service
{
    public interface IStatusService
    {
        List<StatusDto> GetAll();
    }
}