using System;
using AutoMapper;
using E_Hospital.BLL.Configuration.MappingProfiles;
using E_Hospital.BLL.Data;
using E_Hospital.BLL.Utils;
using E_Hospital.DAL;
using E_Hospital.DAL.Entities;
using E_Hospital.DAL.Repositories.Abstraction;

namespace E_Hospital.BLL.Services.Implementation
{
    public class AuthService : IAuthService
    {
        public AuthService()
        {
            var unitOfWork = new UnitOfWork();

            _usersRepository = unitOfWork.GetRepository<User>();
            _mapper          = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>()));
        }
        
        public UserDto Login(string login, string password)
        {
            var user = _usersRepository.Single(u => u.Login == login, u => u.Role);

            // Not found a user with given login.
            if (user == null)
                return null;

            var pswHash = EncryptionUtil.HashPassword(password, user.Salt);
            return pswHash != user.Password ? null : _mapper.Map<UserDto>(user);
        }

        private readonly IRepository<User> _usersRepository;
        private readonly Mapper            _mapper;
    }
}