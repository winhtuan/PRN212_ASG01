using System.Windows;
using FUMiniHotelSystem.Models;
using FUMiniHotelSystem.ViewModel.CustomerVM;
using FUMiniHotelSystem.CustomerController;

namespace FUMiniHotelSystem.CustomerController
{
    public partial class CustomerWindow : Window
    {
        private readonly Customer _currentCustomer;
        private readonly CustomerWindowViewModel _viewModel;

        public CustomerWindow(Customer customer)
        {
            InitializeComponent();
            _currentCustomer = customer;
            _viewModel = new CustomerWindowViewModel(_currentCustomer);

            _viewModel.OpenBookingDialogAction = room =>
            {
                var dialog = new BookingDialog(room, _currentCustomer);
                if (dialog.ShowDialog() == true && dialog.NewBooking != null) // Ensure NewBooking is not  null
                {
                    _viewModel.BookingHistory = new System.Collections.ObjectModel.ObservableCollection<BO.Booking>(
                        _viewModel.BookingHistory.Concat(new[] { dialog.NewBooking }) // Use an array to ensure type safety
                    );
                }
            };

            this.DataContext = _viewModel;
        }

        private void Window_Closed(object? sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
