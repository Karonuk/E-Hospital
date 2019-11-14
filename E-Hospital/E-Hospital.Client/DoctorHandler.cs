using E_Hospital.Client.UserService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_Hospital.BLL.Data;

namespace E_Hospital.Client
{
    public class DoctorHandler : IDoctorServiceCallback
    {
        public DoctorHandler(DoctorWindow doctorWindow)
        {
            _doctorWindow = doctorWindow;
        }

        public void UpdatePendingRequests(VisitRequestDto visitRequest)
        {
            _doctorWindow?.Dispatcher?.Invoke(() => { _doctorWindow.VisitRequests.Add(visitRequest); });
        }

        private readonly DoctorWindow _doctorWindow;
    }
}