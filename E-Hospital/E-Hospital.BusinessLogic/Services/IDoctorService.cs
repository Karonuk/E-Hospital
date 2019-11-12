using System.Collections.Generic;
using System.ServiceModel;
using E_Hospital.BLL.Data;

namespace E_Hospital.BLL.Services
{
    [ServiceContract]
    public interface IDoctorService
    {
        [OperationContract]
        IEnumerable<VisitRequestDto> GetScheduleForToday(DoctorDto doctor);
    }
}