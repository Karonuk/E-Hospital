﻿using System;
using System.Collections.Generic;
using AutoMapper;
using E_Hospital.BLL.Data;
using E_Hospital.DAL;
using E_Hospital.DAL.Entities;
using E_Hospital.DAL.Repositories.Abstraction;

namespace E_Hospital.BLL.Services.Implementation
{
    public class DoctorService : IDoctorService
    {
        public DoctorService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _requestsRepository = unitOfWork.GetRepository<VisitRequest>();
            _mapper             = mapper;
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

            // TODO: subscribe doctor on receiving new requests.  
            return _mapper.Map<VisitRequestDto[]>(pendingRequests);
        }

        public void ChangeRequestState(int visitRequestId, bool isApproved)
        {
            var request = _requestsRepository.Single(x => x.Id == visitRequestId);

            request.IsApproved = isApproved;

            _requestsRepository.Update(request);
        }

        private readonly IRepository<VisitRequest> _requestsRepository;
        private readonly IMapper                   _mapper;
    }
}