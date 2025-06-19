using FUMiniHotelSystem.BLL.IService;
using FUMiniHotelSystem.DAL.IRepository;
using FUMiniHotelSystem.DAL.Repository;
using FUMiniHotelSystem.Models;

namespace FUMiniHotelSystem.BLL.Service
{
    public class RoomService : IRoomService
    {
        private readonly IRoomRepository _roomRepository;

        public RoomService()
        {
            _roomRepository = RoomRepository.Instance;
        }

        public List<Room> GetAll() => _roomRepository.GetAll();

        public Room? GetById(int id) => _roomRepository.GetById(id);

        public void Add(Room room)
        {
            if (!string.IsNullOrWhiteSpace(room.RoomNumber))
            {
                _roomRepository.Add(room);
            }
        }

        public void Update(Room room)
        {
            if (room.RoomID > 0)
            {
                _roomRepository.Update(room);
            }
        }

        public void Delete(int id)
        {
            _roomRepository.Delete(id);
        }

        public List<Room> Search(string keyword)
        {
            var all = _roomRepository.GetAll();
            if (string.IsNullOrWhiteSpace(keyword)) return all;

            keyword = keyword.ToLower();
            return all.Where(r =>
                r.RoomNumber.ToLower().Contains(keyword) ||
                (r.RoomDescription?.ToLower().Contains(keyword) ?? false) ||
                (r.RoomType?.RoomTypeName.ToLower().Contains(keyword) ?? false)
            ).ToList();
        }
    }
}