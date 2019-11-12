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
        public IEnumerable<VisitRequestDto> GetVisitRequests(PatientDto patient)
        {
            var visitRequests = _visitRequestRepository.Get(x=>x.PatientId== patient.Id,x=>x.Patient,x=>x.Doctor);

            return _mapper.Map<VisitRequestDto[]>(visitRequests).AsEnumerable();
        }

        public void SendVisitRequest(VisitRequestDto visitRequest)
        {
            _visitRequestRepository.Add(_mapper.Map<VisitRequest>(visitRequest));           
        }

        private readonly IMapper                   _mapper;
        private readonly IRepository<VisitRequest> _visitRequestRepository;
    }
}
