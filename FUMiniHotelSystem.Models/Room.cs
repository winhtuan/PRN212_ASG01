using System.ComponentModel.DataAnnotations;

namespace FUMiniHotelSystem.Models
{
    public class Room
    {
        public int RoomID { get; set; }

        [Required(ErrorMessage = "Please enter the room number.")]
        [StringLength(50, ErrorMessage = "Room number must not exceed 50 characters.")]
        public string RoomNumber { get; set; }

        [StringLength(220, ErrorMessage = "Room description must not exceed 220 characters.")]
        public string RoomDescription { get; set; }

        [Range(1, 50, ErrorMessage = "Room capacity must be between 1 and 50.")]
        public int RoomMaxCapacity { get; set; }

        public int RoomStatus { get; set; } = 1; /// 1 = Active, 2 = Booked , 3 = Maintenance, 4 = Deleted

        public string RoomStatusText
        {
            get
            {
                return RoomStatus switch
                {
                    1 => "Active",
                    2 => "Booked",
                    3 => "Maintenance",
                    4 => "Deleted",
                    _ => "Unknown"
                };
            }
        }

        [Range(0, double.MaxValue, ErrorMessage = "Room price must be a positive number.")]
        public decimal RoomPricePerDate { get; set; }

        [Required(ErrorMessage = "Please select a room type.")]
        public RoomType? RoomType { get; set; }

        // Convenience property for ComboBox binding
        public int RoomTypeID => RoomType?.RoomTypeID ?? 0;
    }
}