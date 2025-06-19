using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using FUMiniHotelSystem.AdminController;
using FUMiniHotelSystem.Utils;

namespace FUMiniHotelSystem.ViewModel.Admin
{
    public class AdminWindowViewModel : INotifyPropertyChanged
    {
        private UserControl _currentView;

        public UserControl CurrentView
        {
            get => _currentView;
            set
            {
                if (_currentView != value)
                {
                    _currentView = value;
                    OnPropertyChanged();
                }
            }
        }

        // Các lệnh chuyển trang
        public RelayCommand ShowCustomerManagementCommand { get; }

        public RelayCommand ShowRoomManagementCommand { get; }
        public RelayCommand ShowReportCommand { get; }

        public AdminWindowViewModel()
        {
            // Mặc định hiển thị trang khách hàng
            CurrentView = new CustomerManagementView();

            ShowCustomerManagementCommand = new RelayCommand(_ => CurrentView = new CustomerManagementView());
            ShowRoomManagementCommand = new RelayCommand(_ => CurrentView = new RoomManagementView());
            ShowReportCommand = new RelayCommand(_ => CurrentView = new ReportView());
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        private void ShowCustomerManagement()
        {
            CurrentView = new CustomerManagementView();
        }
    }
}