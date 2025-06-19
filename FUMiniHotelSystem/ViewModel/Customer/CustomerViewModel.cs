using System.Collections.ObjectModel;
using FUMiniHotelSystem.BLL.Service;
using FUMiniHotelSystem.BO;
using FUMiniHotelSystem.Models;
using FUMiniHotelSystem.Utils;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using FUMiniHotelSystem.CustomerController;

namespace FUMiniHotelSystem.ViewModel.CustomerVM
{
    public class CustomerWindowViewModel : ViewModelBase
    {
        private readonly RoomService _roomService;
        private readonly BookingService _bookingService;
        public readonly Customer _currentCustomer;

        public Action<Room>? OpenBookingDialogAction { get; set; }

        public CustomerWindowViewModel(Customer customer)
        {
            _currentCustomer = customer;
            _roomService = new RoomService();
            _bookingService = new BookingService();

            GreetingMessage = $"Hello, {_currentCustomer.CustomerFullName}!";
            LoadActiveRooms();
            LoadBookingHistory();

            BookRoomCommand = new RelayCommand(roomObj =>
            {
                if (roomObj is Room room && OpenBookingDialogAction != null)
                {
                    OpenBookingDialogAction.Invoke(room);
                }
            });

            ViewProfileCommand = new RelayCommand(_ => ViewProfile());
            SearchCommand = new RelayCommand(_ => LoadActiveRooms()); // search triggers reload
        }

        // Greeting
        private string _greetingMessage = string.Empty;
        public string GreetingMessage
        {
            get => _greetingMessage;
            set => SetProperty(ref _greetingMessage, value);
        }

        // Search text
        private string _searchText = string.Empty;
        public string SearchText
        {
            get => _searchText;
            set => SetProperty(ref _searchText, value);
        }

        public ICommand SearchCommand { get; }

        // Tab 1: Available Rooms
        private ObservableCollection<Room> _availableRooms = new();
        public ObservableCollection<Room> AvailableRooms
        {
            get => _availableRooms;
            set => SetProperty(ref _availableRooms, value);
        }

        private void LoadActiveRooms()
        {
            var query = _roomService.GetAll()
                .Where(r => r.RoomStatus == 1);

            if (!string.IsNullOrWhiteSpace(SearchText))
            {
                var lower = SearchText.ToLower();
                query = query.Where(r =>
                    (!string.IsNullOrEmpty(r.RoomNumber) && r.RoomNumber.ToLower().Contains(lower)) ||
                    (!string.IsNullOrEmpty(r.RoomDescription) && r.RoomDescription.ToLower().Contains(lower)) ||
                    (r.RoomType?.RoomTypeName?.ToLower().Contains(lower) ?? false));
            }

            var rooms = query
                .OrderBy(r => r.RoomNumber)
                .ToList();

            AvailableRooms = new ObservableCollection<Room>(rooms);
        }

        // Tab 2: My Bookings
        private ObservableCollection<Booking> _bookingHistory = new();
        public ObservableCollection<Booking> BookingHistory
        {
            get => _bookingHistory;
            set => SetProperty(ref _bookingHistory, value);
        }

        private void LoadBookingHistory()
        {
            var bookings = _bookingService.GetAll()
                .Where(b => b.CustomerID == _currentCustomer.CustomerID)
                .OrderByDescending(b => b.CheckInDate)
                .ToList();

            BookingHistory = new ObservableCollection<Booking>(bookings);
        }

        private Room _selectedRoom;
        public Room SelectedRoom
        {
            get => _selectedRoom;
            set => SetProperty(ref _selectedRoom, value);
        }

        private Booking _selectedBooking;
        public Booking SelectedBooking
        {
            get => _selectedBooking;
            set => SetProperty(ref _selectedBooking, value);
        }

        public ICommand BookRoomCommand { get; }
        public ICommand ViewProfileCommand { get; }

        private void ViewProfile()
        {
            new ProfileDialog(_currentCustomer).ShowDialog();
        }
    }
}
