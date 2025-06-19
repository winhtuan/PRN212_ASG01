using System.Windows;
using FUMiniHotelSystem.ViewModel.Admin;

namespace FUMiniHotelSystem.AdminController
{
    public partial class AdminWindow : Window
    {
        public AdminWindow()
        {
            InitializeComponent();
            this.DataContext = new AdminWindowViewModel();
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Are you sure you want to log out?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                // Reopen the LoginWindow
                var login = new LoginWindow();
                login.Show();

                // Close the current AdminWindow
                this.Close();
            }
        }

        private void Quit_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Are you sure you want to exit the application?", "Exit", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                Application.Current.Shutdown();
            }
        }
    }
}