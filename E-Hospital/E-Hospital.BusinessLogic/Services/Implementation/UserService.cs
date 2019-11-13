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
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Single)]
    public class UserService:IDoctorService,IPatientService
    {
        public UserService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _requestsRepository = unitOfWork.GetRepository<VisitRequest>();
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
            var request = _requestsRepository.Single(x => x.Id == visitRequestId,x=>x.Doctor,x=>x.Patient);

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

        public void Logout(DoctorDto doctor)
        {
            var foundDoctor = _activeDoctors.FirstOrDefault(x => x.Key.Id == doctor.Id);

            if (foundDoctor.Key != null)
                _activeDoctors.Remove(foundDoctor.Key);
        }

        public void LogIn(DoctorDto doctor)
        {
            _activeDoctors.Add(doctor, OperationContext.Current.GetCallbackChannel<IDoctorCallback>());
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

            var selectedDoctor = _activePatients.FirstOrDefault(x => x.Key.Id == visitRequest.Doctor.Id);
            if (selectedDoctor.Key != null)
            {
                selectedDoctor.Value.UpdateRequestState(visitRequest);
            }
        }

        public void LogIn(PatientDto patient)
        {
            _activePatients.Add(patient, OperationContext.Current.GetCallbackChannel<IPatientCallback>());
        }

        public void LogOut(PatientDto patient)
        {
            var foundPatient = _activePatients.FirstOrDefault(x => x.Key.Id == patient.Id);
            if (foundPatient.Key != null)            
                _activePatients.Remove(foundPatient.Key);           
        }

        
        #endregion
        private readonly IRepository<VisitRequest> _requestsRepository;
        private readonly IMapper _mapper;
        private readonly Dictionary<DoctorDto, IDoctorCallback> _activeDoctors;
        private readonly Dictionary<PatientDto, IPatientCallback> _activePatients;
        private readonly IRepository<VisitRequest> _visitRequestRepository;
    }
}
