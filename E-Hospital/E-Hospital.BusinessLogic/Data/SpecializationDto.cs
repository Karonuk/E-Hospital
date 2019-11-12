using System.Runtime.Serialization;

namespace E_Hospital.BLL.Data
{
    [DataContract]
    public class SpecializationDto
    {
        [DataMember] public string Name;
    }
}