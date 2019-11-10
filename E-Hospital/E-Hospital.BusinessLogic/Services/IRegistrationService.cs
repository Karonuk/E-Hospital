using E_Hospital.BLL.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace E_Hospital.BLL.Services
{
    [ServiceContract]
    public interface IRegistrationService
    {
        [OperationContract] bool RegisterDoctor(DoctorDto doctor);
        [OperationContract] bool RegisterPatient(PatientDto patient);
    }
}
