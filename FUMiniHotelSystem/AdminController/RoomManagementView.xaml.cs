using System.Windows.Controls;
using FUMiniHotelSystem.ViewModel.Admin;

namespace FUMiniHotelSystem.AdminController
{
    /// <summary>
    /// Interaction logic for RoomManagementView.xaml
    /// </summary>
    public partial class RoomManagementView : UserControl
    {
        public RoomManagementView()
        {
            InitializeComponent();
            DataContext = new RoomManagementViewModel();
        }
    }
}