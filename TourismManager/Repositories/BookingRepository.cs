using Microsoft.EntityFrameworkCore;
using TourismManager.Web.Data;
using TourismManager.Web.Models;

namespace TourismManager.Web.Repositories
{
    public class BookingRepository : GenericRepository<Booking>, IBookingRepository
    {
        public BookingRepository(TourismDbContext db) : base(db) { }

        public async Task<IEnumerable<Booking>> GetByUserAsync(int userId)
        {
            return await _db.Bookings
                            .Include(b => b.Package)
                            .Where(b => b.UserId == userId && !b.IsDeleted)
                            .OrderByDescending(b => b.CreatedAt)
                            .ToListAsync();
        }

        public async Task<Booking?> GetWithPackageAsync(int bookingId)
        {
            return await _db.Bookings
                            .Include(b => b.Package)
                            .FirstOrDefaultAsync(b => b.Id == bookingId);
        }
    }
}
