namespace E_Hospital.DAL
{
    using E_Hospital.DAL.Entities;
    using System.Data.Entity;

    public class EfContext : DbContext
    {
        public EfContext()
            : base("name=EfContext")
        {
        }

        public DbSet<User>           Users           { get; set; }
        public DbSet<Doctor>         Doctors         { get; set; }
        public DbSet<Patient>        Patients        { get; set; }
        public DbSet<Specialization> Specializations { get; set; }
        public DbSet<Role>           Roles           { get; set; }
        public DbSet<VisitRequest>   VisitRequests   { get; set; }
    }
}