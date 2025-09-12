namespace TourismManager.Web.Models
{
    public class Payment : BaseEntity
    {
        public int BookingId { get; set; }
        public Booking Booking { get; set; } = default!;
        public decimal Amount { get; set; }
        public string Method { get; set; } = "Online"; 
        public string TransactionRef { get; set; } = string.Empty;
        public string Status { get; set; } = "Success"; 
    }
}
