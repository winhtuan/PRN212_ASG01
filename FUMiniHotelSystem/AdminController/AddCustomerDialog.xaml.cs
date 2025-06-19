using System.Windows;
using System.Windows.Controls;
using FUMiniHotelSystem.Models;

namespace FUMiniHotelSystem.AdminController
{
    public partial class AddCustomerDialog : Window
    {
        public Customer NewCustomer { get; private set; } = null!;

        public AddCustomerDialog()
        {
            InitializeComponent();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(FullNameBox.Text) ||
                string.IsNullOrWhiteSpace(EmailBox.Text) ||
                string.IsNullOrWhiteSpace(PhoneBox.Text) ||
                !BirthdayPicker.SelectedDate.HasValue ||
                StatusBox.SelectedItem == null)
            {
                MessageBox.Show("Please fill in all required fields.");
                return;
            }

            if (!int.TryParse(((ComboBoxItem)StatusBox.SelectedItem).Tag?.ToString(), out int customerStatus))
            {
                MessageBox.Show("Invalid status selected.");
                return;
            }

            NewCustomer = new Customer
            {
                CustomerFullName = FullNameBox.Text,
                EmailAddress = EmailBox.Text,
                Telephone = PhoneBox.Text,
                CustomerBirthday = BirthdayPicker.SelectedDate.Value,
                CustomerStatus = customerStatus
            };

            DialogResult = true;
            Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}