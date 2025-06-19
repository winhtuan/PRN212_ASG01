namespace FUMiniHotelSystem.BO
{
    public class Booking
    {
        public int BookingID { get; set; }
        public int CustomerID { get; set; }
        public string RoomNumber { get; set; } = string.Empty;
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public string BookingStatus { get; set; } = "Pending";
    }
}