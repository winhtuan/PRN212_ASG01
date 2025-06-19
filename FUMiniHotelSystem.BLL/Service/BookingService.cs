using FUMiniHotelSystem.BLL.IRepository;
using FUMiniHotelSystem.BLL.IService;
using FUMiniHotelSystem.BO;
using FUMiniHotelSystem.DAL.Repository;

namespace FUMiniHotelSystem.BLL.Service
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly List<Booking> _bookings;

        // Dependency injection constructor
        public BookingService(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository ?? throw new ArgumentNullException(nameof(bookingRepository));
            _bookings = new List<Booking>();
        }

        // Parameterless constructor for legacy usage (optional)
        public BookingService() : this(new BookingRepository()) { }

        public void Add(Booking booking)
        {
            if (booking == null)
                throw new ArgumentNullException(nameof(booking));

            _bookings.Add(booking);
            _bookingRepository.Add(booking);
        }

        public void Delete(int bookingId)
        {
            var booking = _bookings.FirstOrDefault(b => b.BookingID == bookingId);
            if (booking != null)
            {
                _bookings.Remove(booking);
                _bookingRepository.Delete(bookingId);
            }
        }

        public List<Booking> GetAll()
        {
            return _bookingRepository.GetAll();
        }

        public Booking? GetById(int bookingId)
        {
            return _bookingRepository.GetById(bookingId);
        }

        public List<Booking> GetByCustomerId(int customerId)
        {
            return _bookingRepository.GetAll()
                                     .Where(b => b.CustomerID == customerId)
                                     .ToList();
        }

        public void Update(Booking booking)
        {
            _bookingRepository.Update(booking);
        }

        public void UpdateStatus(int bookingId, string newStatus)
        {
            var booking = _bookingRepository.GetById(bookingId);
            if (booking != null)
            {
                booking.BookingStatus = newStatus;
                _bookingRepository.Update(booking);
            }
        }

        public List<Booking> Search(string keyword)
        {
            return _bookings.Where(b =>
                (!string.IsNullOrEmpty(keyword) &&
                (b.RoomNumber.Contains(keyword, StringComparison.OrdinalIgnoreCase)
                || b.BookingStatus.Contains(keyword, StringComparison.OrdinalIgnoreCase)
                || b.CustomerID.ToString() == keyword)))
                .OrderByDescending(b => b.CheckInDate)
                .ToList();
        }
    }
}
