using System.Runtime.Serialization;

namespace E_Hospital.BLL.Data
{
    [DataContract]
    public enum UserRoles
    {
        [EnumMember] Doctor,
        [EnumMember] Patient
    }
}