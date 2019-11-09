using System.ComponentModel.DataAnnotations.Schema;

namespace E_Hospital.DAL.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        [ForeignKey("Role")] public int RoleId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }

        public Role Role { get; set; }
    }
}
