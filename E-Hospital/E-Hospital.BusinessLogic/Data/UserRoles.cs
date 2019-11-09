using System.Runtime.Serialization;

namespace E_Hospital.BLL.Data
{
    [DataContract]
    public enum UserRoles
    {
        [DataMember] Doctor,
        [DataMember] Patient
    }
}