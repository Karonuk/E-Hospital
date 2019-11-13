using E_Hospital.BLL.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace E_Hospital.BLL.Services
{
    [ServiceContract (CallbackContract = typeof(IPatientCallback))]
    public interface IPatientService
    {
        [OperationContract] IEnumerable<VisitRequestDto> GetVisitRequests(PatientDto currentPatient);
        [OperationContract (IsOneWay =true)] void SendVisitRequest(VisitRequestDto visitRequest);
        [OperationContract(IsOneWay = true)] void LogIn(PatientDto patient);
        [OperationContract(IsOneWay = true)] void LogOut(PatientDto patient);        
        [OperationContract] PatientDto GetPatient(int userId);
    }
}
