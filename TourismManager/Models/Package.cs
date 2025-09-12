namespace TourismManager.Web.Models
{
    public class Package : BaseEntity
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public int Days { get; set; }
        public int Nights { get; set; }
        public decimal Price { get; set; }
        public int SeatsAvailable { get; set; }
        public string ImageFileName { get; set; } = "placeholder.jpg";
        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    }
}
