using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace E_Hospital.Client
{
    /// <summary>
    /// Interaction logic for ClientRegistration.xaml
    /// </summary>
    public partial class PatientRegistrationWindow : MetroWindow
    {
        private RegistrationService.PatientDto NewPatient { get; set; }

        public PatientRegistrationWindow()
        {
            InitializeComponent();

            NewPatient = new RegistrationService.PatientDto();
            _client    = new RegistrationService.RegistrationServiceClient();

            RegisterPanel.DataContext = NewPatient;
        }

        private bool CheckForNull()
        {
            return NewPatient.Login != null &&
                   NewPatient.FirstName != null &&
                   NewPatient.LastName != null &&
                   NewPatient.PhoneNumber != null &&
                   NewPatient.MedicalCard != null;
        }

        private async void RegistrationClick(object sender, RoutedEventArgs e)
        {
            if (!CheckForNull())
            {
                await this.ShowMessageAsync("Warning", "You need to fill all the fields.");
                return;
            }

            NewPatient.Password = ClientPasswordBox.Password;
            NewPatient.Role     = RegistrationService.UserRoles.Patient;

            if (_client.RegisterPatient(NewPatient))
                await this.ShowMessageAsync("Success", "Successfully registered.");
            else
                await this.ShowMessageAsync("Error", "Not registered.");
        }

        private readonly RegistrationService.RegistrationServiceClient _client;
    }
}