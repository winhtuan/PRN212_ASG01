using FUMiniHotelSystem.BLL.IRepository;
using FUMiniHotelSystem.BO;

namespace FUMiniHotelSystem.DAL.Repository
{
    public class BookingRepository : IBookingRepository
    {
        private static BookingRepository? _instance;
        public static BookingRepository Instance { get; } = new BookingRepository();

        private readonly List<Booking> _bookings;

        // Ensure the constructor is accessible by making it public
        public BookingRepository()
        {
            _bookings = new List<Booking>
            {
                new Booking
                {
                    BookingID = 1,
                    CustomerID = 1,
                    RoomNumber = "101",
                    CheckInDate = new DateTime(2025, 6, 1),
                    CheckOutDate = new DateTime(2025, 6, 5),
                    BookingStatus = "Confirmed"
                },
                new Booking
                {
                    BookingID = 2,
                    CustomerID = 1,
                    RoomNumber = "102",
                    CheckInDate = new DateTime(2025, 6, 10),
                    CheckOutDate = new DateTime(2025, 6, 12),
                    BookingStatus = "Cancelled"
                },
                new Booking
                {
                    BookingID = 3,
                    CustomerID = 1,
                    RoomNumber = "201",
                    CheckInDate = new DateTime(2025, 5, 2),
                    CheckOutDate = new DateTime(2025, 5, 6),
                    BookingStatus = "Confirmed"
                },
            };
        }

        public List<Booking> GetAll()
        {
            return _bookings;
        }

        public Booking? GetById(int id)
        {
            return _bookings.FirstOrDefault(b => b.BookingID == id);
        }

        public void Add(Booking booking)
        {
            _bookings.Add(booking);
        }

        public void Update(Booking booking)
        {
            var existing = GetById(booking.BookingID);
            if (existing != null)
            {
                _bookings.Remove(existing);
                _bookings.Add(booking);
            }
        }

        public void Delete(int id)
        {
            var booking = GetById(id);
            if (booking != null)
            {
                _bookings.Remove(booking);
            }
        }
    }
}