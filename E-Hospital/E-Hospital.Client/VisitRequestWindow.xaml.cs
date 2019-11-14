using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using E_Hospital.BLL.Data;
using E_Hospital.Client.UserService;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace E_Hospital.Client
{
    public partial class VisitRequestWindow : MetroWindow
    {
        public VisitRequestDto VisitRequest { get; set; }

        public VisitRequestWindow(PatientDto patient)
        {
            InitializeComponent();

            _service = new CommonServiceClient();
            _patient = patient;

            InitializeDoctors();

            DoctorsComboBox.ItemsSource = _doctors;
        }

        private readonly PatientDto          _patient;
        private readonly CommonServiceClient _service;
        private          List<DoctorDto>     _doctors;
        private          DoctorDto           _selectedDoctor;

        private void InitializeDoctors()
        {
            _doctors = _service.GetDoctors().ToList();
        }

        private async void SendButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(CommentTextBox.Text) ||
                _selectedDoctor == null)
            {
                await this.ShowMessageAsync("Warning", "You need to fill all the fields");
                return;
            }

            VisitRequest = new VisitRequestDto
            {
                Comment    = CommentTextBox.Text,
                VisitTime  = DateTime.Now,
                IsApproved = null,
                Doctor     = _selectedDoctor,
                Patient    = _patient
            };

            DialogResult = true;
            Close();
        }

        private void DoctorsComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedDoctor = (DoctorDto) DoctorsComboBox.SelectedItem;
        }
    }
}