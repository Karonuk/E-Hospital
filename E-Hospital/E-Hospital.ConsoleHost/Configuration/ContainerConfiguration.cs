using Autofac;
using E_Hospital.BLL.Services.Implementation;
using E_Hospital.DAL;
using E_Hospital.DAL.Repositories.Abstraction;
using E_Hospital.DAL.Repositories.Implementation;
using System.Data.Entity;

namespace E_Hospital.ConsoleHost.Configuration
{
    public static class ContainerConfiguration
    {
        public static IContainer ConfigureContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<EfContext>().As<DbContext>();
            builder.RegisterGeneric(typeof(EfRepository<>)).As(typeof(IRepository<>));
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>();

            builder.RegisterType<AuthService>();
            builder.RegisterType<RegistrationService>();

            return builder.Build();
        }
    }
}
