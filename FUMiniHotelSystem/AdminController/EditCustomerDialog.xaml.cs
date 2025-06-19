using System.Windows;
using System.Windows.Controls;
using FUMiniHotelSystem.Models;

namespace FUMiniHotelSystem.AdminController
{
    public partial class EditCustomerDialog : Window
    {
        public Customer UpdatedCustomer { get; private set; }

        public EditCustomerDialog(Customer customer)
        {
            InitializeComponent();

            // Populate UI with customer data
            FullNameBox.Text = customer.CustomerFullName;
            EmailBox.Text = customer.EmailAddress;
            PhoneBox.Text = customer.Telephone;
            BirthdayPicker.SelectedDate = customer.CustomerBirthday;

            // Select correct status in ComboBox
            foreach (ComboBoxItem item in StatusBox.Items)
            {
                if (int.TryParse(item.Tag?.ToString(), out int tagVal) && tagVal == customer.CustomerStatus)
                {
                    item.IsSelected = true;
                    break;
                }
            }

            // Initialize the updated customer with ID
            UpdatedCustomer = new Customer
            {
                CustomerID = customer.CustomerID
            };
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(FullNameBox.Text) ||
                string.IsNullOrWhiteSpace(EmailBox.Text) ||
                string.IsNullOrWhiteSpace(PhoneBox.Text) ||
                !BirthdayPicker.SelectedDate.HasValue ||
                !(StatusBox.SelectedItem is ComboBoxItem selectedItem) ||
                !int.TryParse(selectedItem.Tag?.ToString(), out int customerStatus))
            {
                MessageBox.Show("Please complete all fields.");
                return;
            }

            UpdatedCustomer.CustomerFullName = FullNameBox.Text;
            UpdatedCustomer.EmailAddress = EmailBox.Text;
            UpdatedCustomer.Telephone = PhoneBox.Text;
            UpdatedCustomer.CustomerBirthday = BirthdayPicker.SelectedDate.Value;
            UpdatedCustomer.CustomerStatus = customerStatus;

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