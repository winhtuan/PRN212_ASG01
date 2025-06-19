using FUMiniHotelSystem.Models;

namespace FUMiniHotelSystem.BLL.IService
{
    public interface IRoomService
    {
        List<Room> GetAll();

        Room? GetById(int id);

        void Add(Room room);

        void Update(Room room);

        void Delete(int id);
    }
}