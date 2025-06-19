using System.Collections.ObjectModel;
using System.Windows;
using FUMiniHotelSystem.Models;

namespace FUMiniHotelSystem.AdminController
{
    public partial class RoomDialog : Window
    {
        public Room Room { get; private set; }
        public ObservableCollection<RoomType> RoomTypes { get; }

        public RoomDialog(ObservableCollection<RoomType> roomTypes, Room existingRoom = null)
        {
            InitializeComponent();

            RoomTypes = roomTypes;

            Room = existingRoom != null
                ? new Room
                {
                    RoomID = existingRoom.RoomID,
                    RoomNumber = existingRoom.RoomNumber,
                    RoomDescription = existingRoom.RoomDescription,
                    RoomMaxCapacity = existingRoom.RoomMaxCapacity,
                    RoomPricePerDate = existingRoom.RoomPricePerDate,
                    RoomType = existingRoom.RoomType
                }
                : new Room();

            DataContext = this;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (Room.RoomType == null || string.IsNullOrWhiteSpace(Room.RoomNumber))
            {
                MessageBox.Show("Please fill in all required fields.", "Validation", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            DialogResult = true;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}