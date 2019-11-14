using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using E_Hospital.BLL.Data;
using E_Hospital.Client.RegistrationService;
using E_Hospital.Client.UserService;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using DoctorDto = E_Hospital.Client.RegistrationService.DoctorDto;
using UserRoles = E_Hospital.Client.RegistrationService.UserRoles;

namespace E_Hospital.Client
{
    public partial class DoctorRegisterWindow : MetroWindow
    {
        public DoctorRegisterWindow()
        {
            InitializeComponent();

            _commonService       = new CommonServiceClient();
            _registrationService = new RegistrationServiceClient();

            InitializeSpecializations();

            SpecializationsComboBox.ItemsSource = _specializations;
        }

        private          List<SpecializationDto>   _specializations;
        private          SpecializationDto         _selectedSpecialization;
        private readonly CommonServiceClient       _commonService;
        private readonly RegistrationServiceClient _registrationService;

        private void InitializeSpecializations()
        {
            _specializations = _commonService.GetSpecializations().ToList();
        }

        private async void RegisterButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(FirstNameTextBox.Text) ||
                string.IsNullOrWhiteSpace(LastNameTextBox.Text) ||
                string.IsNullOrWhiteSpace(PhoneNumberTextBox.Text) ||
                string.IsNullOrWhiteSpace(LoginTextBox.Text) ||
                string.IsNullOrWhiteSpace(PasswordTextBox.Password) ||
                _selectedSpecialization == null)
            {
                await this.ShowMessageAsync("Warning", "You need to fill all the fields.");
                return;
            }

            var doctor = new DoctorDto
            {
                Login              = LoginTextBox.Text,
                Password           = PasswordTextBox.Password,
                FirstName          = FirstNameTextBox.Text,
                LastName           = LastNameTextBox.Text,
                PhoneNumber        = PhoneNumberTextBox.Text,
                SpecializationName = _selectedSpecialization.Name,
                Role               = UserRoles.Doctor
            };

            var res = _registrationService.RegisterDoctor(doctor);

            if (res)
                await this.ShowMessageAsync("Success", "Successfully registered.");
            else
                await this.ShowMessageAsync("Error", "Not registered.");

            Close();
        }

        private void SpecializationsComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedSpecialization = (SpecializationDto) SpecializationsComboBox.SelectedItem;
        }
    }
}