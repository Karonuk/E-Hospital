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
            CreateMap<DoctorDto, User>()
                .ForMember(dst => dst.Role, opt => opt.Ignore());
            CreateMap<User, DoctorDto>()
                .ForMember(dst => dst.SpecializationName, opt => opt.MapFrom(src => src.Doctor.Specialization.Name));
            CreateMap<PatientDto, User>()
                .ForMember(dst => dst.Role, opt => opt.Ignore());
            CreateMap<Doctor, DoctorDto>()
                .ForMember(dst => dst.SpecializationName, opt => opt.MapFrom(src => src.Specialization.Name))
                .ForMember(dst => dst.FirstName, opt => opt.MapFrom(src => src.User.FirstName))
                .ForMember(dst => dst.LastName, opt => opt.MapFrom(src => src.User.LastName))
                .ForMember(dst => dst.Login, opt => opt.MapFrom(src => src.User.Login))
                .ForMember(dst => dst.PhoneNumber, opt => opt.MapFrom(src => src.User.PhoneNumber));
            CreateMap<Patient, PatientDto>()
                .ForMember(dst => dst.FirstName, opt => opt.MapFrom(src => src.User.FirstName))
                .ForMember(dst => dst.LastName, opt => opt.MapFrom(src => src.User.LastName))
                .ForMember(dst => dst.Login, opt => opt.MapFrom(src => src.User.Login))
                .ForMember(dst => dst.PhoneNumber, opt => opt.MapFrom(src => src.User.PhoneNumber));
            CreateMap<User, PatientDto>();
            CreateMap<VisitRequest, VisitRequestDto>()
                .ForMember(dst => dst.Doctor, opt => opt.MapFrom(src => src.Doctor))
                .ForMember(dst => dst.Patient, opt => opt.MapFrom(src => src.Patient));
            CreateMap<VisitRequestDto, VisitRequest>()
                .ForMember(dst => dst.DoctorId, opt => opt.MapFrom(src => src.Doctor.Id))
                .ForMember(dst => dst.PatientId, opt => opt.MapFrom(src => src.Patient.Id))
                .ForMember(dst => dst.Comment, opt => opt.MapFrom(src => src.Comment))
                .ForMember(dst => dst.VisitTime, opt => opt.MapFrom(src => src.VisitTime))
                .ForMember(dst => dst.IsApproved, opt => opt.MapFrom(src => src.IsApproved));
            CreateMap<Specialization, SpecializationDto>();
        }
    }
}