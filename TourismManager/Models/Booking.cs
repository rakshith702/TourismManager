using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace TourismManager.Web.Models
{
    public class Booking : BaseEntity
    {
        public int PackageId { get; set; }

        [ValidateNever]
        public Package Package { get; set; } = default!;

        // User relationship - required
        public int UserId { get; set; }

        [ValidateNever]
        public User User { get; set; } = default!;

        [Required]
        [DataType(DataType.Date)]
        public DateTime TravelDate { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Persons must be at least 1")]
        public int Persons { get; set; }

        public decimal TotalAmount { get; set; }
        public string Status { get; set; } = "Pending";

        public bool IsCancelled { get; set; }
        public DateTime? CancelledAt { get; set; }

    }
}
