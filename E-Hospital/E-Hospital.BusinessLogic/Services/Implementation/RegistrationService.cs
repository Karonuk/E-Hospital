using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using E_Hospital.BLL.Configuration.MappingProfiles;
using E_Hospital.BLL.Data;
using E_Hospital.DAL;
using E_Hospital.DAL.Entities;
using E_Hospital.DAL.Repositories.Abstraction;

namespace E_Hospital.BLL.Services.Implementation
{
    class RegistrationService : IRegistrationService
    {
        public RegistrationService()
        {
            var unitOfWork = new UnitOfWork();
            _userRepository = unitOfWork.GetRepository<User>();
            _mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>()));
        }
        public bool Register(UserDto userDto)
        {
            var user = _userRepository.Single(x => x.Login == userDto.Login);
            if (user == null)
                return false;
            
        }
        private readonly IRepository<User> _userRepository;
        private readonly Mapper _mapper;
    }
}
