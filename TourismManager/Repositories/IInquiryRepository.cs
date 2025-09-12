using TourismManager.Web.Models;
using TourismManager.Web.Repositories;

public interface IInquiryRepository : IGenericRepository<Inquiry>
{
    Task<IEnumerable<Inquiry>> GetAllAsync();
    Task<IEnumerable<Inquiry>> GetUnresolvedAsync();
}
