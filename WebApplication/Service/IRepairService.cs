using System;
using System.Collections.Generic;
using WebApplication.Dto;

namespace WebApplication.Service
{
    public interface IRepairService
    {
        RepairDto GetById(long id);
        RepairDto GetByGuid(Guid guid);

        RepairDto Update(long id, RepairDto repairDto);

        List<RepairDto> GetAll();

        RepairDto Save(RepairDto repairDto);
        void Delete(long id);
    }
}