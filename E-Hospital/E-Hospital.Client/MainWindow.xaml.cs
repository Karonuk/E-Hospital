using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System.Windows;
using E_Hospital.Client.AuthService;

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
            _authService = new AuthServiceClient();
            
        }

        private readonly AuthServiceClient _authService;

        private async void LogInButton_OnClick(object sender, RoutedEventArgs e)
        {
            if(string.IsNullOrWhiteSpace(LoginTextBox.Text) || string.IsNullOrWhiteSpace(PasswordTextBox.Password))
            {
                await this.ShowMessageAsync("Warning", "Please fill login and password fields");
                return;
            }

            var user = _authService.Login(LoginTextBox.Text,PasswordTextBox.Password);
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
