using System.ComponentModel.DataAnnotations.Schema;

namespace E_Hospital.DAL.Entities
{
    public class Doctor
    {
        [ForeignKey("User")] public int Id { get; set; }
        [ForeignKey("Specialization")] public int SpecializationId { get; set; }

        public User User { get; set; }
        public Specialization Specialization { get; set; }
    }
}