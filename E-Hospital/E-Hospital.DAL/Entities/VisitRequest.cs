using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Hospital.DAL.Entities
{
    public class VisitRequest
    {
        public int Id { get; set; }
        [ForeignKey("Doctor")] public int DoctorId { get; set; }
        [ForeignKey("Patient")] public int PatientId { get; set; }
        public string Comment { get; set; }
        public DateTime VisitTime { get; set; }
        public bool? IsApproved { get; set; } // Null - doctor hasn't decided on the request.

        public Doctor Doctor { get; set; }
        public Patient Patient { get; set; }
    }
}