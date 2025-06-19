using FUMiniHotelSystem.AdminController;
using FUMiniHotelSystem.BLL.Service;
using FUMiniHotelSystem.BO;
using FUMiniHotelSystem.Models;
using FUMiniHotelSystem.Utils;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace FUMiniHotelSystem.ViewModel.Admin
{
    public class ReportViewModel : ViewModelBase
    {
        private readonly BookingService _bookingService;

        public ReportViewModel()
        {
            _bookingService = new BookingService();
            StartDate = DateTime.Today.AddMonths(-1);
            EndDate = DateTime.Today;

            GenerateReportCommand = new RelayCommand(_ => GenerateReport());
            UpdateStatusCommand = new RelayCommand(_ => UpdateSelectedBookingStatus(), _ => SelectedBooking != null);
        }

        private DateTime _startDate;
        public DateTime StartDate
        {
            get => _startDate;
            set => SetProperty(ref _startDate, value);
        }

        private DateTime _endDate;
        public DateTime EndDate
        {
            get => _endDate;
            set => SetProperty(ref _endDate, value);
        }

        private ObservableCollection<Booking> _reportResults = new();
        public ObservableCollection<Booking> ReportResults
        {
            get => _reportResults;
            set => SetProperty(ref _reportResults, value);
        }

        private Booking? _selectedBooking;
        public Booking? SelectedBooking
        {
            get => _selectedBooking;
            set => SetProperty(ref _selectedBooking, value);
        }

        public ICommand GenerateReportCommand { get; }
        public ICommand UpdateStatusCommand { get; }

        private void GenerateReport()
        {
            var result = _bookingService.GetAll()
                .Where(b => b.CheckInDate >= StartDate && b.CheckOutDate <= EndDate)
                .OrderByDescending(b => b.CheckInDate)
                .ToList();

            ReportResults = new ObservableCollection<Booking>(result);
        }

        private void UpdateSelectedBookingStatus()
        {
            if (SelectedBooking == null) return;

            var newStatus = PromptForStatus();
            if (!string.IsNullOrEmpty(newStatus))
            {
                _bookingService.UpdateStatus(SelectedBooking.BookingID, newStatus);
                GenerateReport(); // refresh data
            }
        }

        private string? PromptForStatus()
        {
            if (SelectedBooking == null)
                return null;

            var dialog = new UpdateStatusBooking(SelectedBooking);
            bool? result = dialog.ShowDialog();

            if (result == true)
            {
                return dialog.SelectedStatus;
            }

            return null;
        }
    }
}
