using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
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
using E_Hospital.BLL.Data;
using E_Hospital.Client.UserService;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace E_Hospital.Client
{
    /// <summary>
    /// Interaction logic for PatientWindow.xaml
    /// </summary>
    public partial class PatientWindow : MetroWindow
    {
        public PatientWindow(PatientDto patient)
        {
            InitializeComponent();

            _patient        = patient;
            _patientService = new PatientServiceClient(new InstanceContext(new PatientHandler()));
        }

        private readonly PatientDto           _patient;
        private readonly PatientServiceClient _patientService;

        private async void CreateRequestButton_OnClick(object sender, RoutedEventArgs e)
        {
            var window = new VisitRequestWindow(_patient);

            if (window.ShowDialog() == false)
                return;

            _patientService.SendVisitRequest(window.VisitRequest);

            await this.ShowMessageAsync("Success", "Successfully created visit request");
        }
    }
}