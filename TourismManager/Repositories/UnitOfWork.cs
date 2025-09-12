using TourismManager.Web.Data;
using TourismManager.Web.Models;
using TourismManager.Web.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly TourismDbContext _db;
    public UnitOfWork(TourismDbContext db)
    {
        _db = db;
        Packages = new PackageRepository(_db);
        Bookings = new BookingRepository(_db);
        Payments = new GenericRepository<Payment>(_db);
        Inquiries = new GenericRepository<Inquiry>(_db);
        Users = new GenericRepository<User>(_db);
        Refunds = new GenericRepository<Refund>(_db);
    }

    public IPackageRepository Packages { get; }
    public IBookingRepository Bookings { get; }
    public IGenericRepository<Payment> Payments { get; }
    public IGenericRepository<Inquiry> Inquiries { get; }
    public IGenericRepository<User> Users { get; }
    public Task<int> SaveChangesAsync() => _db.SaveChangesAsync();
    public IGenericRepository<Refund> Refunds { get; }



}
