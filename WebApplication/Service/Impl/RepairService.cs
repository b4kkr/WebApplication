using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApplication.Dto;
using WebApplication.Entities;
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
            return _mapper.Map<RepairDto>(_carMechanicContext.Repairs.SingleOrDefault(x => x.Guid == guid));
        }

        public RepairDto Update(Guid guid, RepairDto repairDto)
        {
            Repair repair = _carMechanicContext.Repairs.Include(x => x.Car).ThenInclude(x => x.Type).Include(x => x.Car.User)
                .SingleOrDefault(x => x.Guid == guid);
            if (repair == null) return null;
            repair.Paid = repairDto.Paid ?? false;
            repair.Price = repairDto.Price;
            repair.StatusEntity=
                _carMechanicContext.StatusEntities.SingleOrDefault(x => x.Status == Enum.Parse<Status>(repairDto.Status));
            repair.Works = repairDto.Works;
            repair.Car = _carMechanicContext.Cars.SingleOrDefault(x => x.Id == repairDto.Car.Id);
            repair.Car.Repair = repair;
            _carMechanicContext.SaveChanges();
            return _mapper.Map<RepairDto>(repair);
        }

        public List<RepairDto> GetAll()
        {
            return _mapper.Map<List<RepairDto>>(_carMechanicContext.Repairs.Include(x => x.Car).ThenInclude(x => x.Type).Include(x => x.Car.User).Include(x => x.StatusEntity).ToList());
        }

        public RepairDto Save(RepairDto repairDto)
        {
            Repair repair = _mapper.Map<Repair>(repairDto);
            repair.Car.Repair = repair;
            repair.StatusEntity =
                _carMechanicContext.StatusEntities.SingleOrDefault(x => x.Status == Status.AddedForService);
            repair.Car.Type =
                _carMechanicContext.CarTypes.SingleOrDefault(x =>
                    x.Model == repairDto.Car.Type.Model && x.TypeName == repair.Car.Type.TypeName) ?? _mapper.Map<CarType>(repairDto.Car.Type);
            _carMechanicContext.Repairs.Add(repair);
            _carMechanicContext.SaveChanges();
            RepairDto dto = _mapper.Map<RepairDto>(repair);
            return dto;
        }

        public void Delete(long id)
        {
            _carMechanicContext.Repairs.Remove(_carMechanicContext.Repairs.Find(id));
            _carMechanicContext.SaveChanges();
        }

        public RepairDto GetByEmailAndPassword(string email, string password)
        {
            User user = _carMechanicContext.Users.Include(x => x.Car).ThenInclude(x => x.Repair).ThenInclude(x => x.StatusEntity)
                .Include(x => x.Car.Type)
                .SingleOrDefault(x => x.Email == email && x.Password == password);
            if (user == null) return null;
            return _mapper.Map<RepairDto>(user.Car.Repair);
        }
    }
}