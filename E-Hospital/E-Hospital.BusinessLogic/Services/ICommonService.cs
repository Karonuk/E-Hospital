using E_Hospital.BLL.Data;
using System.Collections.Generic;
using System.ServiceModel;

namespace E_Hospital.BLL.Services
{
    [ServiceContract]
    public interface ICommonService
    {
        [OperationContract]
        IEnumerable<SpecializationDto> GetSpecializations();

        [OperationContract]
        IEnumerable<DoctorDto> GetDoctors();
    }
}