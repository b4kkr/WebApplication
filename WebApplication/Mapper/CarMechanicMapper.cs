using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebApplication.Dto;
using WebApplication.Entities;
using WebApplication.Repository;

namespace WebApplication.Mapper
{
    public class CarMechanicMapper : Profile
    {
        public CarMechanicMapper()
        {
            CreateMap<CarType, CarTypeDto>().ReverseMap();
            CreateMap<Car, CarDto>().ReverseMap();

            CreateMap<Repair, RepairDto>()
                .ForMember(dto => dto.RepairGuid,
                    opt => opt.MapFrom(x => x.Guid))
                .ForMember(dto => dto.Status,
                    opt => opt.MapFrom(x => x.Status.Description))
                .ReverseMap()
                .ForPath(repair => repair.Status.Description,
                    opt => opt.MapFrom(x => x.Status));
            CreateMap<User, UserDto>()
                .ForMember(dto => dto.RepairGuid,
                    opt => opt.MapFrom(x => x.Repair.Guid))
                .ReverseMap();
            CreateMap<List<Car>, List<CarDto>>().ReverseMap();

        }
    }
}