using System.Windows.Controls;
using FUMiniHotelSystem.ViewModel.Admin;

namespace FUMiniHotelSystem.AdminController
{
    public partial class ReportView : UserControl
    {
        public ReportView()
        {
            InitializeComponent();
            this.DataContext = new ReportViewModel();
        }
    }
}
