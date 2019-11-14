using System.ServiceModel;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System.Windows;
using E_Hospital.Client.AuthService;
using E_Hospital.Client.UserService;
using UserRoles = E_Hospital.Client.AuthService.UserRoles;

namespace E_Hospital.Client
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            _authService    = new AuthServiceClient();
            _doctorService  = new DoctorServiceClient(new InstanceContext(new DoctorHandler(null)));
            _patientService = new PatientServiceClient(new InstanceContext(new PatientHandler()));
        }

        private readonly AuthServiceClient    _authService;
        private readonly DoctorServiceClient  _doctorService;
        private readonly PatientServiceClient _patientService;

        private async void LogInButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(LoginTextBox.Text) || string.IsNullOrWhiteSpace(PasswordTextBox.Password))
            {
                await this.ShowMessageAsync("Warning", "Please fill login and password fields");
                return;
            }

            var user = _authService.Login(LoginTextBox.Text, PasswordTextBox.Password);

            if (user == null)
            {
                await this.ShowMessageAsync("Warning", "You entered incorrect login or password");
                return;
            }

            switch (user.Role)
            {
                case UserRoles.Doctor:
                    var doctor = _doctorService.GetDoctor(user.Id);

                    new DoctorWindow(doctor).Show();
                    Close();
                    break;

                case UserRoles.Patient:
                    var patient = _patientService.GetPatient(user.Id);

                    new PatientWindow(patient).Show();
                    Close();
                    break;
            }
        }

        private void RegisterAsDoctor_OnClick(object sender, RoutedEventArgs e)
        {
            var window = new DoctorRegisterWindow();

            window.ShowDialog();
        }

        private void RegisterAsPatient_OnClick(object sender, RoutedEventArgs e)
        {
            var window = new PatientRegistrationWindow();

            window.ShowDialog();
        }
    }
}