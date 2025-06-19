using FUMiniHotelSystem.BLL.IService;
using FUMiniHotelSystem.DAL.IRepository;
using FUMiniHotelSystem.DAL.Repository;
using FUMiniHotelSystem.Models;

namespace FUMiniHotelSystem.BLL.Service
{
    public class RoomTypeService : IRoomTypeService
    {
        private readonly IRoomTypeRepository _roomTypeRepository;

        public RoomTypeService()
        {
            _roomTypeRepository = RoomTypeRepository.Instance;
        }

        public List<RoomType> GetAll() => _roomTypeRepository.GetAll();

        public RoomType? GetById(int id) => _roomTypeRepository.GetById(id);

        public void Add(RoomType roomType)
        {
            if (!string.IsNullOrWhiteSpace(roomType.RoomTypeName))
            {
                _roomTypeRepository.Add(roomType);
            }
        }

        public void Update(RoomType roomType)
        {
            if (roomType.RoomTypeID > 0)
            {
                _roomTypeRepository.Update(roomType);
            }
        }

        public void Delete(int id)
        {
            if (id > 0)
            {
                _roomTypeRepository.Delete(id);
            }
        }

        public List<RoomType> Search(string keyword)
        {
            var all = _roomTypeRepository.GetAll();
            if (string.IsNullOrWhiteSpace(keyword)) return all;

            keyword = keyword.ToLower();
            return all.FindAll(rt =>
                rt.RoomTypeName.ToLower().Contains(keyword) ||
                (rt.TypeDescription?.ToLower().Contains(keyword) ?? false) ||
                (rt.TypeNote?.ToLower().Contains(keyword) ?? false)
            );
        }
    }
}