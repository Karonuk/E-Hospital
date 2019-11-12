using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using AutoMapper;
using E_Hospital.BLL.Data;
using E_Hospital.DAL;
using E_Hospital.DAL.Entities;
using E_Hospital.DAL.Repositories.Abstraction;

namespace E_Hospital.BLL.Services.Implementation
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Single)]
    public class DoctorService : IDoctorService
    {
        public DoctorService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _requestsRepository = unitOfWork.GetRepository<VisitRequest>();
            _mapper             = mapper;
            _activeDoctors      = new Dictionary<DoctorDto, IDoctorCallback>();
        }

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

        private readonly IRepository<VisitRequest>              _requestsRepository;
        private readonly IMapper                                _mapper;
        private readonly Dictionary<DoctorDto, IDoctorCallback> _activeDoctors;
    }
}