namespace TourismManager.Web.Models
{
    public class Inquiry : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public bool Resolved { get; set; }
    }
}
