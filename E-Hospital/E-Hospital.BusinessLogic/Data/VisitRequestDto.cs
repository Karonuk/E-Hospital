using System;
using System.Runtime.Serialization;

namespace E_Hospital.BLL.Data
{
    [DataContract]
    public class VisitRequestDto
    {
        [DataMember] public string   Comment    { get; set; }
        [DataMember] public DateTime VisitTime  { get; set; }
        [DataMember] public bool?    IsApproved { get; set; }

        [DataMember] public DoctorDto  Doctor  { get; set; }
        [DataMember] public PatientDto Patient { get; set; }
    }
}