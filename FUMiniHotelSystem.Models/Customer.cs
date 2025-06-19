using System.ComponentModel.DataAnnotations;

namespace FUMiniHotelSystem.Models
{
    public class Customer
    {
        public int CustomerID { get; set; }

        [Required(ErrorMessage = "Full name is required.")]
        [StringLength(50, ErrorMessage = "Full name must not exceed 50 characters.")]
        public string CustomerFullName { get; set; }

        [Required(ErrorMessage = "Telephone number is required.")]
        [StringLength(12, ErrorMessage = "Telephone number must not exceed 12 characters.")]
        public string Telephone { get; set; }

        [Required(ErrorMessage = "Email address is required.")]
        [StringLength(50, ErrorMessage = "Email address must not exceed 50 characters.")]
        [EmailAddress(ErrorMessage = "Invalid email address format.")]
        public string EmailAddress { get; set; }

        [Required(ErrorMessage = "Birthday is required.")]
        public DateTime CustomerBirthday { get; set; }

        public int CustomerStatus { get; set; } = 1; /// 1 = Active, 2 = Deleted, 3 = Blocked

        public string CustomerStatusText
        {
            get
            {
                return CustomerStatus switch
                {
                    1 => "Active",
                    2 => "Deleted",
                    3 => "Blocked",
                    _ => "Unknown"
                };
            }
        }

        [Required(ErrorMessage = "Password is required.")]
        [StringLength(50, ErrorMessage = "Password must not exceed 50 characters.")]
        public string Password { get; set; }
    }
}