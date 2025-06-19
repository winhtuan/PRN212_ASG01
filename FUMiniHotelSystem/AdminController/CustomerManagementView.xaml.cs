using System.Windows;
using System.Windows.Controls;
using FUMiniHotelSystem.ViewModel.Admin;

namespace FUMiniHotelSystem.AdminController
{
    public partial class CustomerManagementView : UserControl
    {
        public CustomerManagementView()
        {
            InitializeComponent();
            DataContext = new CustomerManagementViewModel();
        }

        private void AddUser_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new AddCustomerDialog();
            dialog.Owner = Window.GetWindow(this);
            if (dialog.ShowDialog() == true)
            {
                var newCustomer = dialog.NewCustomer;
                var vm = (CustomerManagementViewModel)this.DataContext;
                vm.AddCustomerFromDialog(newCustomer);
            }
        }
    }
}