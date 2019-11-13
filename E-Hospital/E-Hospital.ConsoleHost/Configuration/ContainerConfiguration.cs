using Autofac;
using E_Hospital.BLL.Services.Implementation;
using E_Hospital.DAL;
using E_Hospital.DAL.Repositories.Abstraction;
using E_Hospital.DAL.Repositories.Implementation;
using System.Data.Entity;
using AutoMapper;
using E_Hospital.BLL.Configuration.MappingProfiles;

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

            builder.Register(ctx => { return new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>()); })
                .As<IConfigurationProvider>();

            builder.Register(ctx => new Mapper(ctx.Resolve<IConfigurationProvider>()))
               .As<IMapper>();

            builder.RegisterType<AuthService>();
            builder.RegisterType<RegistrationService>();
            builder.RegisterType<UserService>();




            return builder.Build();
        }
    }
}