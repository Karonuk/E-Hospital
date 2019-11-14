using System.ServiceModel;
using E_Hospital.BLL.Data;

namespace E_Hospital.BLL.Services
{
    [ServiceContract]
    public interface IRegistrationService
    {
        [OperationContract]
        bool RegisterDoctor(DoctorDto doctor);

        [OperationContract]
        bool RegisterPatient(PatientDto patient);
    }
}