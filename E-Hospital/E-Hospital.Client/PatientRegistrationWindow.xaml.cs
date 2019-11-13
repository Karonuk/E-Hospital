
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
        public RegistrationService.PatientDto NewPatient { get; set; }
        public PatientRegistrationWindow()
        {
            NewPatient = new RegistrationService.PatientDto();
            _client = new RegistrationService.RegistrationServiceClient();
            InitializeComponent();
            _client.Open();
            RegisterPanel.DataContext = NewPatient;
            
        }

        private bool CheckForNull()
        {
            if (NewPatient.Login != null && NewPatient.FirstName != null &&
                NewPatient.LastName != null && NewPatient.PhoneNumber != null && NewPatient.MedicalCard != null)
                return true;
            else
                return false;
        }
        private void RegistrationClick(object sender, RoutedEventArgs e)
        {
            if (CheckForNull())
            {
                NewPatient.Password = ClientPasswordBox.Password;
                NewPatient.Role = RegistrationService.UserRoles.Patient;
                if (_client.RegisterPatient(NewPatient))
                    this.ShowMessageAsync("Succesfully", "Succesfully registered");
                else
                    this.ShowMessageAsync("Error", "Something went wrong");
                
            }
            else
            {
                this.ShowMessageAsync("Warning", "Please fill all fields");
            }
        }

        private readonly RegistrationService.RegistrationServiceClient _client;
    }
}
