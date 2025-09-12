using Microsoft.AspNetCore.Mvc;
using TourismManager.Web.Models;
using TourismManager.Web.Repositories;

namespace TourismManager.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _uow;
        public HomeController(IUnitOfWork uow) { _uow = uow; }

        public async Task<IActionResult> Index()
        {
           
            var allPackages = (await _uow.Packages.SearchAsync(null, null, null))
                              .OrderByDescending(p => p.Id);
            return View(allPackages);
        }

        [HttpGet]
        public IActionResult Contact()
        {
            return View(new Inquiry());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Contact(Inquiry model)
        {
            if (!ModelState.IsValid)
                return View(model);

            model.Resolved = false;
            model.CreatedAt = DateTime.UtcNow;
            model.IsDeleted = false;

            await _uow.Inquiries.AddAsync(model);
            await _uow.SaveChangesAsync();

            TempData["ok"] = "Thanks! We'll get back to you.";
            return RedirectToAction("Index", "Home");
        }

        public IActionResult AccessDenied() => View();
    }
}
