using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace E_Hospital.BLL.Data
{
    public class DoctorDto : UserDto
    {
        [DataMember] public int SpecializationId { get; set; }
        [DataMember] public SpecializationDto Specialization { get; set; }
    }
}
