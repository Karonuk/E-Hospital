using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using E_Hospital.BLL.Data;
using E_Hospital.DAL;
using E_Hospital.DAL.Entities;
using E_Hospital.DAL.Repositories.Abstraction;

namespace E_Hospital.BLL.Services.Implementation
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Single)]
    public class PatientService : IPatientService
    {
        public PatientService(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _mapper                 = mapper;
            _visitRequestRepository = unitOfWork.GetRepository<VisitRequest>();
        }
        public IEnumerable<VisitRequestDto> GetVisitRequests(PatientDto currentPatient)
        {
            var visitRequests = _visitRequestRepository.Get();
            List<VisitRequestDto> visitRequestsDto = new List<VisitRequestDto>();
            foreach(var visitRequest in visitRequests)
            {
                visitRequestsDto.Add(_mapper.Map<VisitRequestDto>(visitRequest));
            }
            return visitRequestsDto.AsEnumerable();
        }

        public void SendVisitRequest(VisitRequestDto visitRequest)
        {
            _visitRequestRepository.Add(_mapper.Map<VisitRequest>(visitRequest));
            var doctorCallBack = OperationContext.Current.GetCallbackChannel<IDoctorCallback>();
            doctorCallBack.UpdatePendingRequests(visitRequest);
        }

        private readonly IMapper                   _mapper;
        private readonly IRepository<VisitRequest> _visitRequestRepository;
    }
}
