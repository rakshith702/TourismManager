using Microsoft.EntityFrameworkCore;
using TourismManager.Web.Data;
using TourismManager.Web.Models;

namespace TourismManager.Web.Repositories
{
    public class PackageRepository : GenericRepository<Package>, IPackageRepository
    {
        public PackageRepository(TourismDbContext db) : base(db) { }

        public async Task<IEnumerable<Package>> SearchAsync(string? q, int? minDays, int? maxPrice)
        {
            var query = _db.Packages.AsQueryable();
            if (!string.IsNullOrWhiteSpace(q))
                query = query.Where(p => p.Title.Contains(q) || p.Location.Contains(q));
            if (minDays.HasValue)
                query = query.Where(p => p.Days >= minDays);
            if (maxPrice.HasValue)
                query = query.Where(p => p.Price <= maxPrice);
            return await query.OrderBy(p => p.Price).ToListAsync();
        }
    }
}
