using E_Hospital.Client.RegistrationService;
using E_Hospital.Client.UserService;
using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace E_Hospital.Client
{
    /// <summary>
    /// Interaction logic for DoctorWindow.xaml
    /// </summary>
    public partial class DoctorWindow : MetroWindow
    {
        public DoctorWindow(UserService.DoctorDto doctor)
        {
            InitializeComponent();

            _doctor = doctor;
            _visitRequests = new ObservableCollection<VisitRequestDto>();

            var context = new InstanceContext(new DoctorHandler());

            _server = new DoctorServiceClient(context);

            VisitRequestsListBox.ItemsSource = _visitRequests;
        }

        private readonly UserService.DoctorDto _doctor;
        private readonly ObservableCollection<VisitRequestDto> _visitRequests;
        private readonly DoctorServiceClient _server;

        private void InitializeRequests()
        {
            foreach(var i in _server.GetPendingRequests(_doctor))
            {
                _visitRequests.Add(i);
            }
        }

        private void DeclineButton_OnClick(object sender, RoutedEventArgs e)
        {

        }

        private void ApproveButton_OnClick(object sender, RoutedEventArgs e)
        {

        }
    }
}
