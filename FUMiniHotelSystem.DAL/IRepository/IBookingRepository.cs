using FUMiniHotelSystem.BO;

namespace FUMiniHotelSystem.BLL.IRepository
{
    public interface IBookingRepository
    {
        List<Booking> GetAll();

        Booking? GetById(int id);

        void Add(Booking booking);

        void Update(Booking booking);

        void Delete(int id);
    }
}