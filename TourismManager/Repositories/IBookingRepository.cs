using TourismManager.Web.Models;

namespace TourismManager.Web.Repositories
{
    public interface IBookingRepository : IGenericRepository<Booking>
    {
        Task<IEnumerable<Booking>> GetByUserAsync(int userId);
        Task<Booking?> GetWithPackageAsync(int bookingId);
    }
}
