using System.ServiceModel;
using E_Hospital.BLL.Data;

namespace E_Hospital.BLL.Services
{
    public interface IDoctorCallback
    {
        [OperationContract(IsOneWay = true)]
        void UpdatePendingRequests(VisitRequestDto visitRequest);
    }
}