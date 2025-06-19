using System.Windows;
using FUMiniHotelSystem.Models;

namespace FUMiniHotelSystem.CustomerController
{
    public partial class ProfileDialog : Window
    {
        public ProfileDialog(Customer customer)
        {
            InitializeComponent();
            DataContext = customer;
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
