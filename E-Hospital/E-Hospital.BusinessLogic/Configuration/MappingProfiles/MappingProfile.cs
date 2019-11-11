using System;
using AutoMapper;
using E_Hospital.BLL.Data;
using E_Hospital.DAL.Entities;

namespace E_Hospital.BLL.Configuration.MappingProfiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDto>()
                .ForMember(dst => dst.Role,
                    opt => opt.MapFrom(src => (UserRoles) Enum.Parse(typeof(UserRoles), src.Role.Name)));
            CreateMap<DoctorDto, User>();
            CreateMap<PatientDto, User>()
                .ForMember(dst => dst.Role, opt => opt.Ignore());
        }
    }
}