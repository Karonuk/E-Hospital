using System.Runtime.Serialization;

namespace E_Hospital.BLL.Data
{
    [DataContract]
    public class DoctorDto : UserDto
    {
        [DataMember] public string SpecializationName { get; set; }
    }
}