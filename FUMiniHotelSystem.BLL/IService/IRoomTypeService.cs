using FUMiniHotelSystem.Models;

namespace FUMiniHotelSystem.BLL.IService
{
    public interface IRoomTypeService
    {
        List<RoomType> GetAll();

        RoomType? GetById(int id);

        void Add(RoomType roomType);

        void Update(RoomType roomType);

        void Delete(int id);

        List<RoomType> Search(string keyword);
    }
}