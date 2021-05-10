using System;
using System.Collections.Generic;
using AutoMapper;
using WebApplication.Dto;
using WebApplication.Repository;

namespace WebApplication.Service.Impl
{
    public class RepairService : IRepairService
    {

        private IMapper _mapper;
        private CarMechanicContext _carMechanicContext;

        public RepairService(IMapper mapper, CarMechanicContext carMechanicContext)
        {
            _mapper = mapper;
            _carMechanicContext = carMechanicContext;
        }

        public RepairDto GetById(long id)
        {
            return _mapper.Map<RepairDto>(_carMechanicContext.Repairs.Find(id));
        }

        public RepairDto GetByGuid(Guid guid)
        {
            throw new NotImplementedException();
        }

        public RepairDto Update(long id, RepairDto repairDto)
        {
            throw new NotImplementedException();
        }

        public List<RepairDto> GetAll()
        {
            throw new NotImplementedException();
        }

        public RepairDto Save(RepairDto repairDto)
        {
            throw new NotImplementedException();
        }

        public void Delete(long id)
        {
            throw new NotImplementedException();
        }
    }
}