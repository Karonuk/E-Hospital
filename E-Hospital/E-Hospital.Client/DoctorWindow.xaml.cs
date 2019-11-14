using E_Hospital.Client.UserService;
using MahApps.Metro.Controls;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ServiceModel;
using System.Windows;
using E_Hospital.BLL.Data;

namespace E_Hospital.Client
{
    /// <summary>
    /// Interaction logic for DoctorWindow.xaml
    /// </summary>
    public partial class DoctorWindow : MetroWindow
    {
        public readonly ObservableCollection<VisitRequestDto> VisitRequests;

        public DoctorWindow(DoctorDto doctor)
        {
            InitializeComponent();

            _doctor       = doctor;
            VisitRequests = new ObservableCollection<VisitRequestDto>();

            var context = new InstanceContext(new DoctorHandler(this));

            _server = new DoctorServiceClient(context);

            InitializeRequests();

            VisitRequestsListBox.ItemsSource = VisitRequests;

            _server.LogInDoctor(_doctor);
        }

        private readonly DoctorDto           _doctor;
        private readonly DoctorServiceClient _server;

        private void InitializeRequests()
        {
            foreach (var i in _server.GetPendingRequests(_doctor))
            {
                VisitRequests.Add(i);
            }
        }

        private void DeclineButton_OnClick(object sender, RoutedEventArgs e)
        {
        }

        private void ApproveButton_OnClick(object sender, RoutedEventArgs e)
        {
        }

        private void DoctorWindow_OnClosing(object sender, CancelEventArgs e)
        {
            _server.LogoutDoctor(_doctor);
        }
    }
}