using AutoMapper;
using E_Hospital.BLL.Data;
using E_Hospital.DAL;
using E_Hospital.DAL.Entities;
using E_Hospital.DAL.Repositories.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace E_Hospital.BLL.Services.Implementation
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class UserService : IDoctorService, IPatientService
    {
        public UserService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _requestsRepository = unitOfWork.GetRepository<VisitRequest>();
            _userRepository = unitOfWork.GetRepository<User>();

            _mapper = mapper;

            _activeDoctors = new Dictionary<DoctorDto, IDoctorCallback>();
            _visitRequestRepository = unitOfWork.GetRepository<VisitRequest>();
            _activePatients = new Dictionary<PatientDto, IPatientCallback>();
        }
        #region Doctor
        public IEnumerable<VisitRequestDto> GetScheduleForToday(DoctorDto doctor)
        {
            var visitRequests = _requestsRepository.Get(d =>
                    d.DoctorId == doctor.Id
                    && d.IsApproved == true
                    && d.VisitTime.DayOfYear == DateTime.Today.DayOfYear
                    && d.VisitTime.Year == DateTime.Today.Year,
                cfg => cfg.Doctor, cfg => cfg.Patient);

            return _mapper.Map<VisitRequestDto[]>(visitRequests);
        }

        public IEnumerable<VisitRequestDto> GetPendingRequests(DoctorDto doctor)
        {
            var pendingRequests = _requestsRepository.Get(d => d.DoctorId == doctor.Id && d.IsApproved == null,
                cfg => cfg.Doctor, cfg => cfg.Patient);

            return _mapper.Map<VisitRequestDto[]>(pendingRequests);
        }

        public void ChangeRequestState(int visitRequestId, bool isApproved)
        {
            var request = _requestsRepository.Single(x => x.Id == visitRequestId, x => x.Doctor, x => x.Patient);

            request.IsApproved = isApproved;

            _requestsRepository.Update(request);

            var selectedPatient = _activePatients.FirstOrDefault(x => x.Key.Id == request.Patient.Id);
            if (selectedPatient.Key != null)
                selectedPatient.Value.UpdateRequestState(_mapper.Map<VisitRequestDto>(request));
        }

        public void ReceiveVisitRequest(DoctorDto doctor, VisitRequestDto visitRequest)
        {
            if (_activeDoctors.Any(x => x.Key.Id == doctor.Id))
            {
                _activeDoctors.First(x => x.Key.Id == doctor.Id).Value.UpdatePendingRequests(visitRequest);
            }
        }

        public void LogoutDoctor(DoctorDto doctor)
        {
            var foundDoctor = _activeDoctors.FirstOrDefault(x => x.Key.Id == doctor.Id);

            if (foundDoctor.Key != null)
                _activeDoctors.Remove(foundDoctor.Key);
        }

        public void LogInDoctor(DoctorDto doctor)
        {
            _activeDoctors.Add(doctor, OperationContext.Current.GetCallbackChannel<IDoctorCallback>());
        }

        public DoctorDto GetDoctor(int userID)
        {
            var user = _userRepository.Single(x => x.Id == userID, x => x.Doctor.Specialization);
            if (user != null)
            {
                var doctor = _mapper.Map<DoctorDto>(user);
                doctor.SpecializationName = user.Doctor.Specialization.Name;
                return doctor;
            }

            return null;
        }

        #endregion

        #region Patient
        public IEnumerable<VisitRequestDto> GetVisitRequests(PatientDto patient)
        {
            var visitRequests = _visitRequestRepository.Get(x => x.PatientId == patient.Id, x => x.Patient, x => x.Doctor);
            return _mapper.Map<VisitRequestDto[]>(visitRequests).AsEnumerable();
        }

        public void SendVisitRequest(VisitRequestDto visitRequest)
        {
            _visitRequestRepository.Add(_mapper.Map<VisitRequest>(visitRequest));

            ReceiveVisitRequest(visitRequest.Doctor, visitRequest);
        }

        public void LogInPatient(PatientDto patient)
        {
            _activePatients.Add(patient, OperationContext.Current.GetCallbackChannel<IPatientCallback>());
        }

        public void LogOutPatient(PatientDto patient)
        {
            var foundPatient = _activePatients.FirstOrDefault(x => x.Key.Id == patient.Id);
            if (foundPatient.Key != null)
                _activePatients.Remove(foundPatient.Key);
        }

        public PatientDto GetPatient(int userId)
        {
            var user = _userRepository.Single(x => x.Id == userId, x => x.Patient);
            if (user != null)
            {
                var patient = _mapper.Map<PatientDto>(user);
                patient.MedicalCard = user.Patient.MedicalCard;
                return patient;
            }
            return null;
        }
        #endregion

        private readonly IRepository<VisitRequest> _requestsRepository;
        private readonly IMapper _mapper;
        private readonly Dictionary<DoctorDto, IDoctorCallback> _activeDoctors;
        private readonly Dictionary<PatientDto, IPatientCallback> _activePatients;
        private readonly IRepository<VisitRequest> _visitRequestRepository;
        private readonly IRepository<User> _userRepository;
    }
}