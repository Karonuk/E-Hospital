using Autofac;
using Autofac.Integration.Wcf;
using E_Hospital.BLL.Services.Implementation;
using E_Hospital.ConsoleHost.Configuration;
using System;
using System.ServiceModel;
using System.Text;

namespace E_Hospital.ConsoleHost
{
    class Program
    {
        static IContainer container = ContainerConfiguration.ConfigureContainer();
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF7;
            try
            {
                var authHost = new ServiceHost(typeof(AuthService));
                var registrationHost = new ServiceHost(typeof(RegistrationService));
                var userHost = new ServiceHost(container.Resolve<UserService>());

                authHost.AddDependencyInjectionBehavior<AuthService>(container);
                registrationHost.AddDependencyInjectionBehavior<RegistrationService>(container);
                userHost.AddDependencyInjectionBehavior<UserService>(container);               
                userHost.Open();
                Console.WriteLine("User Service Started...");
                authHost.Open();
                Console.WriteLine("Auth Service Started...");
                registrationHost.Open();
                Console.WriteLine("Register Service Started...");

                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}