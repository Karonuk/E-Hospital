using System.ComponentModel.DataAnnotations.Schema;

namespace E_Hospital.DAL.Entities
{
    public class Patient
    {
        [ForeignKey("User")]
        public int Id { get; set; }
        public string MedicalCard { get; set; }

        public User User { get; set; }
    }
}
