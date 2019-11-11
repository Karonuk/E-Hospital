using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace E_Hospital.BLL.Data
{
    [DataContract]
    public class DoctorDto : UserDto
    {
        [DataMember] public string SpecializationName { get; set; }
    }
}
