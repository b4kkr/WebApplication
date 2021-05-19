using System;
using System.Collections.Generic;
using WebApplication.Dto;
using WebApplication.Entities;
using WebApplication.Interfaces;
using WebApplication.Service;
using WebApplication.Service.Impl;

namespace WebApplication.Resources
{
    public class RepairResource : RepairInterface
    {

        private readonly IRepairService _repairService;

        public RepairResource(IRepairService repairService)
        {
            _repairService = repairService;
        }

        public override List<RepairDto> GetAll()
        {
            return _repairService.GetAll();
        }

        public override RepairDto GetByGuid(string guid)
        {
            return _repairService.GetByGuid(Guid.Parse(guid));
        }

        public override RepairDto Save(RepairDto repairDto)
        {
            return _repairService.Save(repairDto);
        }

        public override RepairDto Update(string guid, RepairDto repairDto)
        {
            return _repairService.Update(Guid.Parse(guid), repairDto);
        }

        public override void Delete(long id)
        {
            _repairService.Delete(id);
        }

        public override RepairDto GetByEmailAndPassword(string email, string password)
        {
            return _repairService.GetByEmailAndPassword(email, password);
        }
    }
}