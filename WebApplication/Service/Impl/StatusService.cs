using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using WebApplication.Dto;
using WebApplication.Entities;
using WebApplication.Repository;

namespace WebApplication.Service.Impl
{
    public class StatusService  : IStatusService
    {

        private readonly IMapper _mapper;
        private readonly CarMechanicContext _carMechanicContext;

        public StatusService(IMapper mapper, CarMechanicContext carMechanicContext)
        {
            _mapper = mapper;
            _carMechanicContext = carMechanicContext;
        }

        public List<StatusDto> GetAll()
        {
            List<StatusEntity> statusEntities = _carMechanicContext.StatusEntities.ToList();
            List<StatusDto> statusDtos = new List<StatusDto>();
            statusEntities.ForEach(x => statusDtos.Add(_mapper.Map<StatusDto>(x)));
            return statusDtos;
        }
    }
}