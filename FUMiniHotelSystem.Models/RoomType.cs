using System.ComponentModel.DataAnnotations;

namespace FUMiniHotelSystem.Models
{
    public class RoomType
    {
        public int RoomTypeID { get; set; }

        [Required(ErrorMessage = "Room type name is required.")]
        [StringLength(50, ErrorMessage = "Room type name must not exceed 50 characters.")]
        public string RoomTypeName { get; set; }

        [StringLength(250, ErrorMessage = "Description must not exceed 250 characters.")]
        public string TypeDescription { get; set; }

        [StringLength(250, ErrorMessage = "Note must not exceed 250 characters.")]
        public string TypeNote { get; set; }
    }
}