using System;
using System.Windows;
using FUMiniHotelSystem.Models;
using FUMiniHotelSystem.BLL.Service;
using FUMiniHotelSystem.BO;

namespace FUMiniHotelSystem.CustomerController
{
    public partial class BookingDialog : Window
    {
        private readonly Room _room;
        private readonly Customer _customer;
        private readonly BookingService _bookingService;

        public BookingDialog(Room room, Customer customer)
        {
            InitializeComponent();
            _room = room;
            _customer = customer;
            _bookingService = new BookingService();

            RoomNumberBox.Text = room.RoomNumber;
            CheckInDatePicker.SelectedDate = DateTime.Today;
            CheckOutDatePicker.SelectedDate = DateTime.Today.AddDays(1);
        }

        public Booking? NewBooking { get; private set; }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            if (CheckInDatePicker.SelectedDate == null || CheckOutDatePicker.SelectedDate == null)
            {
                MessageBox.Show("Please select both check-in and check-out dates.", "Validation", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (CheckOutDatePicker.SelectedDate <= CheckInDatePicker.SelectedDate)
            {
                MessageBox.Show("Check-out date must be after check-in date.", "Validation", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            NewBooking = new Booking
            {
                CustomerID = _customer.CustomerID,
                RoomNumber = _room.RoomNumber,
                CheckInDate = CheckInDatePicker.SelectedDate.Value,
                CheckOutDate = CheckOutDatePicker.SelectedDate.Value,
                BookingStatus = "Pending"
            };

            _bookingService.Add(NewBooking);

            MessageBox.Show("Booking successful!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            DialogResult = true;
            Close();
        }


        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
