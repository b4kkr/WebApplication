using System;
using System.Collections.Generic;
using AutoMapper;
using WebApplication.Constants;
using WebApplication.Dto;
using WebApplication.Entities;

namespace WebApplication.Mapper
{
    public class CarMechanicMapper : Profile
    {
        public CarMechanicMapper()
        {
            CreateMap<CarType, CarTypeDto>().ReverseMap();
            CreateMap<Car, CarDto>()
                .ForMember(dto => dto.User, opt => opt.MapFrom(x => x.User)).ReverseMap();

            CreateMap<Repair, RepairDto>()
                .ForMember(dto => dto.RepairGuid,
                    opt => opt.MapFrom(x => x.Guid.ToString()))
                .ForMember(dto => dto.Status,
                    opt => opt.MapFrom(x => x.StatusEntity.Status.ToString()))
                .ForMember(dto => dto.Car, opt => opt.MapFrom(x => x.Car))
                .ReverseMap()
                .ForMember(entity => entity.StatusEntity,
                    opt => opt.Ignore())
                .ForMember(entity => entity.Paid, opt => opt.MapFrom(dto => dto.Paid ?? false))
                .ForMember(entity => entity.Guid,
                    opt => opt.MapFrom(dto => Guid.Parse(dto.RepairGuid ?? Guid.NewGuid().ToString())))
                .ForMember(e => e.Car,
                    opt => opt.MapFrom(x => x.Car));
            CreateMap<User, UserDto>()
                .ForMember(dto => dto.RepairGuid, opt => opt.MapFrom(x => x.Car.Repair.Guid.ToString()))
                .ReverseMap()
                .ForMember(entity => entity.Password,
                    opt => opt.MapFrom(dto => dto.Password ?? PasswordGenerator.Instance.Generate(8)))
                .ForMember(entity => entity.Car, opt => opt.Ignore());
            CreateMap<List<Car>, List<CarDto>>().ReverseMap();
            CreateMap<string, StatusEntity>()
                .ForMember(x => x.Status,
                    opt => opt.MapFrom(x => new StatusEntity(){Status = Enum.Parse<Status>(x ?? Status.AddedForService.ToString())}));

        }
    }
}