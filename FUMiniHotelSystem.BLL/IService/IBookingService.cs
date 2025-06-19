using FUMiniHotelSystem.BO;

namespace FUMiniHotelSystem.BLL.IService
{
    public interface IBookingService
    {
        List<Booking> GetAll();
        void Add(Booking booking);

    }
}