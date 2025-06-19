using FUMiniHotelSystem.DAL.IRepository;
using FUMiniHotelSystem.Models;

namespace FUMiniHotelSystem.DAL.Repository
{
    public class RoomTypeRepository : IRoomTypeRepository
    {
        private static RoomTypeRepository _instance;
        private List<RoomType> _roomTypes;

        public static RoomTypeRepository Instance => _instance ??= new RoomTypeRepository();

        private RoomTypeRepository()
        {
            _roomTypes = new List<RoomType>
            {
                new RoomType { RoomTypeID = 1, RoomTypeName = "Standard", TypeDescription = "Basic room", TypeNote = "" },
                new RoomType { RoomTypeID = 2, RoomTypeName = "Deluxe", TypeDescription = "Spacious deluxe room", TypeNote = "Balcony" }
            };
        }

        public List<RoomType> GetAll() => _roomTypes;

        public RoomType? GetById(int id) => _roomTypes.FirstOrDefault(rt => rt.RoomTypeID == id);

        public void Add(RoomType roomType)
        {
            roomType.RoomTypeID = _roomTypes.Max(rt => rt.RoomTypeID) + 1;
            _roomTypes.Add(roomType);
        }

        public void Update(RoomType roomType)
        {
            var existing = GetById(roomType.RoomTypeID);
            if (existing != null)
            {
                existing.RoomTypeName = roomType.RoomTypeName;
                existing.TypeDescription = roomType.TypeDescription;
                existing.TypeNote = roomType.TypeNote;
            }
        }

        public void Delete(int id)
        {
            var roomType = GetById(id);
            if (roomType != null)
                _roomTypes.Remove(roomType);
        }
    }
}