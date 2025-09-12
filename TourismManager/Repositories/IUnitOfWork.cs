using TourismManager.Web.Models;

namespace TourismManager.Web.Repositories
{
    public interface IUnitOfWork
    {
        IPackageRepository Packages { get; }
        IBookingRepository Bookings { get; }
        IGenericRepository<Payment> Payments { get; }
        IGenericRepository<Inquiry> Inquiries { get; }
        IGenericRepository<User> Users { get; }

        IGenericRepository<Refund> Refunds { get; }

        Task<int> SaveChangesAsync();
    }
}
