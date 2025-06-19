using System.Collections.ObjectModel;
using System.Windows.Input;
using FUMiniHotelSystem.AdminController;
using FUMiniHotelSystem.BLL.Service;
using FUMiniHotelSystem.Models;
using FUMiniHotelSystem.Utils;

namespace FUMiniHotelSystem.ViewModel.Admin
{
    public class RoomManagementViewModel : ViewModelBase
    {
        private readonly RoomService _roomService;
        private readonly RoomTypeService _roomTypeService;

        public RoomManagementViewModel()
        {
            _roomService = new RoomService();
            _roomTypeService = new RoomTypeService();

            Rooms = new ObservableCollection<Room>(_roomService.GetAll());
            RoomTypes = new ObservableCollection<RoomType>(_roomTypeService.GetAll());

            InsertCommand = new RelayCommand(_ => OpenInsertDialog());
            UpdateCommand = new RelayCommand(_ => OpenUpdateDialog(), _ => SelectedRoom != null);
            DeleteCommand = new RelayCommand(_ => DeleteRoom(), _ => SelectedRoom != null);
            SearchCommand = new RelayCommand(_ => SearchRoom());
        }

        public ObservableCollection<Room> Rooms { get; set; }
        public ObservableCollection<RoomType> RoomTypes { get; set; }

        private Room _selectedRoom;

        public Room SelectedRoom
        {
            get => _selectedRoom;
            set => SetProperty(ref _selectedRoom, value);
        }

        private string _searchText;

        public string SearchText
        {
            get => _searchText;
            set => SetProperty(ref _searchText, value);
        }

        public ICommand InsertCommand { get; }
        public ICommand UpdateCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand SearchCommand { get; }

        private void OpenInsertDialog()
        {
            var dialog = new RoomDialog(RoomTypes);
            if (dialog.ShowDialog() == true)
            {
                var room = dialog.Room;
                _roomService.Add(room);
                Rooms.Add(room);
            }
        }

        private void OpenUpdateDialog()
        {
            if (SelectedRoom == null) return;

            var dialog = new RoomDialog(RoomTypes, SelectedRoom);
            if (dialog.ShowDialog() == true)
            {
                var updatedRoom = dialog.Room;
                updatedRoom.RoomID = SelectedRoom.RoomID;
                _roomService.Update(updatedRoom);
                RefreshRooms();
            }
        }

        private void DeleteRoom()
        {
            if (SelectedRoom != null)
            {
                _roomService.Delete(SelectedRoom.RoomID);
                Rooms.Remove(SelectedRoom);
            }
        }

        private void SearchRoom()
        {
            var keyword = SearchText?.Trim().ToLower() ?? "";

            var result = _roomService.GetAll()
                .Where(r =>
                    string.IsNullOrEmpty(keyword) ||
                    (!string.IsNullOrEmpty(r.RoomNumber) && r.RoomNumber.ToLower().Contains(keyword)) ||
                    (!string.IsNullOrEmpty(r.RoomDescription) && r.RoomDescription.ToLower().Contains(keyword)) ||
                    (!string.IsNullOrEmpty(r.RoomType?.RoomTypeName) && r.RoomType.RoomTypeName.ToLower().Contains(keyword))
                )
                .OrderBy(r => r.RoomNumber)
                .ToList();

            Rooms = new ObservableCollection<Room>(result);
            OnPropertyChanged(nameof(Rooms));
        }

        private void RefreshRooms()
        {
            Rooms = new ObservableCollection<Room>(
                _roomService.GetAll().OrderBy(r => r.RoomNumber)
            );
            OnPropertyChanged(nameof(Rooms));
        }
    }
}