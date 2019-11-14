using System.Runtime.Serialization;

namespace E_Hospital.BLL.Data
{
    [DataContract]
    public class UserDto
    {
        [DataMember] public int       Id          { get; set; }
        [DataMember] public string    Login       { get; set; }
        [DataMember] public string    Password    { get; set; }
        [DataMember] public UserRoles Role        { get; set; }
        [DataMember] public string    FirstName   { get; set; }
        [DataMember] public string    LastName    { get; set; }
        [DataMember] public string    PhoneNumber { get; set; }
    }
}