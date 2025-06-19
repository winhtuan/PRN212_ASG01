using FUMiniHotelSystem.Models;

namespace FUMiniHotelSystem.DAL.IRepository
{
    public interface IRoomTypeRepository
    {
        List<RoomType> GetAll();

        RoomType? GetById(int id);

        void Add(RoomType roomType);

        void Update(RoomType roomType);

        void Delete(int id);
    }
}