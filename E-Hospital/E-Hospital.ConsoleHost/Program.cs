using Autofac.Integration.Wcf;
using E_Hospital.BLL.Services.Implementation;
using E_Hospital.ConsoleHost.Configuration;
using System;
using System.ServiceModel;

namespace E_Hospital.ConsoleHost
{
    class Program
    {
        static void Main(string[] args)
        {
            var authHost = new ServiceHost(typeof(AuthService));
            var registrationHost = new ServiceHost(typeof(RegistrationService));

            authHost.AddDependencyInjectionBehavior<AuthService>(ContainerConfiguration.ConfigureContainer());
            registrationHost.AddDependencyInjectionBehavior<RegistrationService>(ContainerConfiguration.ConfigureContainer());

            authHost.Open();
            registrationHost.Open();

            Console.ReadLine();
        }
    }
}
