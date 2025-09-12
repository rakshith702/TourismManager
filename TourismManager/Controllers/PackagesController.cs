using Microsoft.AspNetCore.Mvc;
using TourismManager.Web.Repositories;

namespace TourismManager.Web.Controllers
{
    public class PackagesController : Controller
    {
        private readonly IUnitOfWork _uow;
        public PackagesController(IUnitOfWork uow) { _uow = uow; }

        public async Task<IActionResult> Index(string? q, int? minDays, int? maxPrice)
        {
            var data = await _uow.Packages.SearchAsync(q, minDays, maxPrice);
            ViewBag.Query = q; ViewBag.MinDays = minDays; ViewBag.MaxPrice = maxPrice;
            return View(data);
        }

        public async Task<IActionResult> Details(int id)
        {
            var pkg = await _uow.Packages.GetByIdAsync(id);
            if (pkg == null) return NotFound();
            return View(pkg);
        }
    }
}
