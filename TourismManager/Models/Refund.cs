using TourismManager.Web.Models;

public class Refund : BaseEntity
{
    public int BookingId { get; set; }
    public Booking Booking { get; set; }
    public decimal RefundedAmount { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
