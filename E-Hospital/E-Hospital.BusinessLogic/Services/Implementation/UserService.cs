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
    class UserService:IDoctorService,IPatientService
    {
        public UserService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _requestsRepository = unitOfWork.GetRepository<VisitRequest>();
            _mapper = mapper;
            _activeDoctors = new Dictionary<DoctorDto, IDoctorCallback>();
            _visitRequestRepository = unitOfWork.GetRepository<VisitRequest>();
        }
        //Doctor service
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

            if (_activeDoctors.All(x => x.Key.Id != doctor.Id))
            {
                var callback = OperationContext.Current.GetCallbackChannel<IDoctorCallback>();
                _activeDoctors.Add(doctor, callback);
            }

            return _mapper.Map<VisitRequestDto[]>(pendingRequests);
        }

        public void ChangeRequestState(int visitRequestId, bool isApproved)
        {
            var request = _requestsRepository.Single(x => x.Id == visitRequestId);

            request.IsApproved = isApproved;

            _requestsRepository.Update(request);
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

        //Patient
        public IEnumerable<VisitRequestDto> GetVisitRequests(PatientDto patient)
        {
            var visitRequests = _visitRequestRepository.Get(x => x.PatientId == patient.Id, x => x.Patient, x => x.Doctor);

            return _mapper.Map<VisitRequestDto[]>(visitRequests).AsEnumerable();
        }

        public void SendVisitRequest(VisitRequestDto visitRequest)
        {
            _visitRequestRepository.Add(_mapper.Map<VisitRequest>(visitRequest));
        }

        private readonly IRepository<VisitRequest> _requestsRepository;
        private readonly IMapper _mapper;
        private readonly Dictionary<DoctorDto, IDoctorCallback> _activeDoctors;
        private readonly IRepository<VisitRequest> _visitRequestRepository;
    }
}
