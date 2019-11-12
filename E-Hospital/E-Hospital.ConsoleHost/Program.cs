using Autofac;
using Autofac.Integration.Wcf;
using E_Hospital.BLL.Services.Implementation;
using E_Hospital.ConsoleHost.Configuration;
using System;
using System.ServiceModel;

namespace E_Hospital.ConsoleHost
{
    class Program
    {
        static IContainer container = ContainerConfiguration.ConfigureContainer();
        static void Main(string[] args)
        {
            var authHost         = new ServiceHost(typeof(AuthService));
            var registrationHost = new ServiceHost(typeof(RegistrationService));
            var doctorHost       = new ServiceHost(container.Resolve<DoctorService>());
            var patientHost      = new ServiceHost(container.Resolve<PatientService>());

            authHost.AddDependencyInjectionBehavior<AuthService>(container);
            registrationHost.AddDependencyInjectionBehavior<RegistrationService>(container);
            doctorHost.AddDependencyInjectionBehavior<DoctorService>(container);
            patientHost.AddDependencyInjectionBehavior<PatientService>(container);

            authHost.Open();
            Console.WriteLine("Auth Service Started...");
            registrationHost.Open();
            Console.WriteLine("Register Service Started...");
            doctorHost.Open();
            Console.WriteLine("Doctor Service Started...");
            patientHost.Open();
            Console.WriteLine("Patient Service Started...");

            Console.ReadLine();
        }
    }
}