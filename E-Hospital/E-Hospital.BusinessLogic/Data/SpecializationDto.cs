using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace E_Hospital.BLL.Data
{
    public class SpecializationDto
    {
        [DataMember] public int Id { get; set; }
        [DataMember] public string Name { get; set; }
    }
}
