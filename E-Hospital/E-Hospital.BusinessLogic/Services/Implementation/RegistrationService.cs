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

        public bool RegisterDoctor(DoctorDto _doctor)
        {            
            if (_userRepository.Single(x => x.Login == _doctor.Login) != null)
                return false;

            string salt = EncryptionUtil.GenerateSalt();
            string passwd = EncryptionUtil.HashPassword(_doctor.Password, salt);
            var user = new User()
            {
                LastName = _doctor.LastName,
                FirstName = _doctor.FirstName,
                Login = _doctor.Login,
                Password = passwd,
                Salt = salt,
                PhoneNumber = _doctor.PhoneNumber,
                Role = _roleRepository.Single(x => x.Name == _doctor.Role.ToString()),
            };
            var doctor = new Doctor()
            {
                Specialization = _specializationRepository.Single(x => x.Name == _doctor.SpecializationName),
                User = user,
            };
            _userRepository.Add(user);
            _doctorRepository.Add(doctor);
            return true;
        }

        public bool RegisterPatient(PatientDto _patient)
        {
            if (_userRepository.Single(x => x.Login == _patient.Login) != null)
                return false;

            string salt = EncryptionUtil.GenerateSalt();
            string passwd = EncryptionUtil.HashPassword(_patient.Password, salt);
            var user = new User()
            {
                LastName = _patient.LastName,
                FirstName = _patient.FirstName,
                Login = _patient.Login,
                Password = passwd,
                Salt = salt,
                PhoneNumber = _patient.PhoneNumber,
                Role = _roleRepository.Single(x => x.Name == _patient.Role.ToString()),
            };
            var patient = new Patient()
            {
                MedicalCard = _patient.MedicalCard,
                User = user,
            };
            _userRepository.Add(user);
            _patientRepository.Add(patient);
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
