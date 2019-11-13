using System.Collections.Generic;
using System.ServiceModel;
using E_Hospital.BLL.Data;

namespace E_Hospital.BLL.Services
{
    [ServiceContract(CallbackContract = typeof(IDoctorCallback))]
    public interface IDoctorService
    {
        [OperationContract(IsOneWay =true)]
        void LogIn(DoctorDto doctor);
        [OperationContract]
        IEnumerable<VisitRequestDto> GetScheduleForToday(DoctorDto doctor);

        [OperationContract]
        IEnumerable<VisitRequestDto> GetPendingRequests(DoctorDto doctor);

        [OperationContract(IsOneWay = true)]
        void ChangeRequestState(int visitRequestId, bool isApproved);

        [OperationContract(IsOneWay = true)]
        void ReceiveVisitRequest(DoctorDto doctor, VisitRequestDto visitRequest);

        [OperationContract(IsOneWay = true)]
        void Logout(DoctorDto doctor);
        [OperationContract]
        DoctorDto GetDoctor(int userId);
    }
}