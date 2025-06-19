using FUMiniHotelSystem.DAL.IRepository;
using FUMiniHotelSystem.Models;

namespace FUMiniHotelSystem.DAL.Repository
{
    public class RoomRepository : IRoomRepository
    {
        private static RoomRepository _instance;
        private List<Room> _rooms;

        public static RoomRepository Instance => _instance ??= new RoomRepository();

        private RoomRepository()
        {
            var standard = new RoomType
            {
                RoomTypeID = 1,
                RoomTypeName = "Standard",
                TypeDescription = "Standard Room",
                TypeNote = ""
            };

            var deluxe = new RoomType
            {
                RoomTypeID = 2,
                RoomTypeName = "Deluxe",
                TypeDescription = "Deluxe Room with balcony",
                TypeNote = "Sea view"
            };

            _rooms = new List<Room>
            {
                new Room { RoomID = 1, RoomNumber = "101", RoomDescription = "Standard single room", RoomMaxCapacity = 2, RoomStatus = 2, RoomPricePerDate = 50, RoomType = standard },
                new Room { RoomID = 2, RoomNumber = "102", RoomDescription = "Standard double room", RoomMaxCapacity = 3, RoomStatus = 1, RoomPricePerDate = 60, RoomType = standard },
                new Room { RoomID = 3, RoomNumber = "103", RoomDescription = "Standard twin room", RoomMaxCapacity = 2, RoomStatus = 3, RoomPricePerDate = 55, RoomType = standard },
                new Room { RoomID = 4, RoomNumber = "201", RoomDescription = "Deluxe single room", RoomMaxCapacity = 2, RoomStatus = 2, RoomPricePerDate = 75, RoomType = deluxe },
                new Room { RoomID = 5, RoomNumber = "202", RoomDescription = "Deluxe double room", RoomMaxCapacity = 3, RoomStatus = 1, RoomPricePerDate = 85, RoomType = deluxe },
                new Room { RoomID = 6, RoomNumber = "203", RoomDescription = "Deluxe twin room", RoomMaxCapacity = 2, RoomStatus = 1, RoomPricePerDate = 80, RoomType = deluxe },
                new Room { RoomID = 7, RoomNumber = "301", RoomDescription = "Standard economy room", RoomMaxCapacity = 1, RoomStatus = 1, RoomPricePerDate = 40, RoomType = standard },
                new Room { RoomID = 8, RoomNumber = "302", RoomDescription = "Deluxe premium room", RoomMaxCapacity = 4, RoomStatus = 1, RoomPricePerDate = 95, RoomType = deluxe },
                new Room { RoomID = 9, RoomNumber = "303", RoomDescription = "Standard with desk", RoomMaxCapacity = 2, RoomStatus = 3, RoomPricePerDate = 52, RoomType = standard },
                new Room { RoomID = 10, RoomNumber = "401", RoomDescription = "Deluxe suite", RoomMaxCapacity = 4, RoomStatus = 1, RoomPricePerDate = 120, RoomType = deluxe }
            };
        }

        public List<Room> GetAll() => _rooms;

        public Room? GetById(int id) => _rooms.FirstOrDefault(r => r.RoomID == id);

        public void Add(Room room)
        {
            room.RoomID = _rooms.Any() ? _rooms.Max(r => r.RoomID) + 1 : 1;
            _rooms.Add(room);
        }

        public void Update(Room room)
        {
            var existing = GetById(room.RoomID);
            if (existing != null)
            {
                existing.RoomNumber = room.RoomNumber;
                existing.RoomDescription = room.RoomDescription;
                existing.RoomMaxCapacity = room.RoomMaxCapacity;
                existing.RoomStatus = room.RoomStatus;
                existing.RoomPricePerDate = room.RoomPricePerDate;
                existing.RoomType = room.RoomType;
            }
        }

        public void Delete(int id)
        {
            var room = GetById(id);
            if (room != null)
            {
                room.RoomStatus = 2; // Mark as deleted
            }
        }
    }
}