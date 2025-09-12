using System.ComponentModel.DataAnnotations;
using TourismManager.Web.Models;

namespace TourismManager.Models
{
    public class User
    {
        [Required]
        public string FullName { get; set; } = string.Empty;

        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string PasswordHash { get; set; } = string.Empty;

        [Required]
        public string Role { get; set; } = "Customer";

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public bool IsDeleted { get; set; } = false;

        
        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    }
}
