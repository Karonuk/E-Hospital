using System.Runtime.Serialization;

namespace E_Hospital.BLL.Data
{
    [DataContract]
    public class PatientDto : UserDto
    {
        [DataMember] public string MedicalCard { get; set; }
    }
}