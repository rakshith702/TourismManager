using Microsoft.EntityFrameworkCore;
using TourismManager.Web.Data;
using TourismManager.Web.Models;
using TourismManager.Web.Repositories;

public class InquiryRepository : GenericRepository<Inquiry>, IInquiryRepository
{
    public InquiryRepository(TourismDbContext db) : base(db) { }

    public async Task<IEnumerable<Inquiry>> GetAllAsync() =>
        await _db.Inquiries.OrderByDescending(i => i.CreatedAt).ToListAsync();

    public async Task<IEnumerable<Inquiry>> GetUnresolvedAsync() =>
        await _db.Inquiries.Where(i => !i.Resolved).OrderByDescending(i => i.CreatedAt).ToListAsync();
}
