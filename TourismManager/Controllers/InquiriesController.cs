using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TourismManager.Web.Repositories;

[Authorize(Roles = "Admin")]
public class InquiriesController : Controller
{
    private readonly IUnitOfWork _uow;
    public InquiriesController(IUnitOfWork uow) => _uow = uow;

    public async Task<IActionResult> Index()
    {
        var inquiries = await _uow.Inquiries.GetAllAsync();
        return View(inquiries);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Resolve(int id)
    {
        var inquiry = await _uow.Inquiries.GetByIdAsync(id);
        if (inquiry == null) return NotFound();

        inquiry.Resolved = true;
        _uow.Inquiries.Update(inquiry);
        await _uow.SaveChangesAsync();

        return RedirectToAction("Index");
    }
}
