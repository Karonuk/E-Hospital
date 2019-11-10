using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using E_Hospital.BLL.Configuration.MappingProfiles;
using E_Hospital.BLL.Data;
using E_Hospital.BLL.Utils;
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
            _roleRepository = unitOfWork.GetRepository<Role>();
            _doctorRepository = unitOfWork.GetRepository<Doctor>();
            _patientRepository = unitOfWork.GetRepository<Patient>();
            _specializationRepository = unitOfWork.GetRepository<Specialization>();
            _mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>()));
        }            

        public bool RegisterDoctor(DoctorDto doctor)
        {            
            if (_userRepository.Single(x => x.Login == doctor.Login) != null)
                return false;

            string salt = EncryptionUtil.GenerateSalt();
            string passwd = EncryptionUtil.HashPassword(doctor.Password, salt);
            var user = new User()
            {
                LastName = doctor.LastName,
                FirstName = doctor.FirstName,
                Login = doctor.Login,
                Password = passwd,
                Salt = salt,
                PhoneNumber = doctor.PhoneNumber,
                Role = _roleRepository.Single(x => x.Name == doctor.Role.ToString()),
            };
            var newDoctor = new Doctor()
            {
                Specialization = _specializationRepository.Single(x => x.Name == doctor.SpecializationName),
                User = user,
            };
            _userRepository.Add(user);
            _doctorRepository.Add(newDoctor);
            return true;
        }

        public bool RegisterPatient(PatientDto patient)
        {
            if (_userRepository.Single(x => x.Login == patient.Login) != null)
                return false;

            string salt = EncryptionUtil.GenerateSalt();
            string passwd = EncryptionUtil.HashPassword(patient.Password, salt);
            var user = new User()
            {
                LastName = patient.LastName,
                FirstName = patient.FirstName,
                Login = patient.Login,
                Password = passwd,
                Salt = salt,
                PhoneNumber = patient.PhoneNumber,
                Role = _roleRepository.Single(x => x.Name == patient.Role.ToString()),
            };
            var newPatient = new Patient()
            {
                MedicalCard = patient.MedicalCard,
                User = user,
            };
            _userRepository.Add(user);
            _patientRepository.Add(newPatient);
            return true;
        }

        private readonly IRepository<Role> _roleRepository;
        private readonly IRepository<Doctor> _doctorRepository;
        private readonly IRepository<Specialization> _specializationRepository;
        private readonly IRepository<Patient> _patientRepository;
        private readonly IRepository<User> _userRepository;
        private readonly Mapper _mapper;
    }
}
