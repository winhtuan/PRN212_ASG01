using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using FUMiniHotelSystem.BO;

namespace FUMiniHotelSystem.AdminController
{
    public partial class UpdateStatusBooking : Window
    {
        private readonly Booking _booking;

        public string SelectedStatus => (StatusComboBox.SelectedItem as ComboBoxItem)?.Content.ToString() ?? "";

        public UpdateStatusBooking(Booking booking)
        {
            InitializeComponent();
            _booking = booking;

            RoomNumberBox.Text = booking.RoomNumber;
            CheckInDatePicker.SelectedDate = booking.CheckInDate;
            CheckOutDatePicker.SelectedDate = booking.CheckOutDate;

            // Set selected status
            StatusComboBox.SelectedItem = StatusComboBox.Items
                .Cast<ComboBoxItem>()
                .FirstOrDefault(i => i.Content.ToString() == booking.BookingStatus);
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(SelectedStatus))
            {
                _booking.BookingStatus = SelectedStatus;
                DialogResult = true;
                Close();
            }
            else
            {
                MessageBox.Show("Please select a booking status.", "Validation", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }

}
