using TourismManager.Web.Models;

namespace TourismManager.Web.Repositories
{
    public interface IPackageRepository : IGenericRepository<Package>
    {
        Task<IEnumerable<Package>> SearchAsync(string? q, int? minDays, int? maxPrice);
    }
}