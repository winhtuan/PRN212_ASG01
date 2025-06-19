using System.Windows;
using FUMiniHotelSystem.AdminController;
using FUMiniHotelSystem.BLL.IService;
using FUMiniHotelSystem.BLL.Service;
using FUMiniHotelSystem.CustomerController;
using FUMiniHotelSystem.Models;
using FUMiniHotelSystem.Utils;

namespace FUMiniHotelSystem
{
    public partial class LoginWindow : Window
    {
        private readonly ICustomerService _customerService;

        public LoginWindow()
        {
            InitializeComponent();
            _customerService = new CustomerService();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string inputEmail = txtUser.Text.Trim();
            string inputPassword = txtPass.Password.Trim();

            string adminEmail = AppConfig.GetAdminEmail();
            string adminPassword = AppConfig.GetAdminPassword();

            if (inputEmail == adminEmail && inputPassword == adminPassword)
            {
                MessageBox.Show($"Hello {adminEmail}", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Hide();
                new AdminWindow().Show();
                return;
            }

            Customer? customer = _customerService.GetByEmail(inputEmail);

            if (customer != null && customer.Password == inputPassword && customer.CustomerStatus == 1)
            {
                MessageBox.Show($"Welcome {customer.CustomerFullName}", "Customer", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Hide();
                new CustomerWindow(customer).Show();
                return;
            }

            // 3. Thất bại
            MessageBox.Show("Invalid email or password.", "Login Failed", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}