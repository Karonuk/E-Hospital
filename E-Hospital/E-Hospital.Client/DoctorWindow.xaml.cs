using E_Hospital.Client.UserService;
using MahApps.Metro.Controls;
using System.Collections.ObjectModel;
using System.ServiceModel;
using System.Windows;

namespace E_Hospital.Client
{
    /// <summary>
    /// Interaction logic for DoctorWindow.xaml
    /// </summary>
    public partial class DoctorWindow : MetroWindow
    {
        public DoctorWindow(DoctorDto doctor)
        {
            InitializeComponent();

            _doctor = doctor;
            _visitRequests = new ObservableCollection<VisitRequestDto>();

            var context = new InstanceContext(new DoctorHandler());

            _server = new DoctorServiceClient(context);
            
            InitializeRequests();

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
