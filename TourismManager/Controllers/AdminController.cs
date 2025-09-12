using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TourismManager.Web.Models;
using TourismManager.Web.Repositories;

namespace TourismManager.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly IUnitOfWork _uow;
        public AdminController(IUnitOfWork uow) { _uow = uow; }

        public async Task<IActionResult> Packages() => View(await _uow.Packages.GetAllAsync());

        [HttpPost]
        public async Task<IActionResult> AddOrUpdatePackage(Package pkg)
        {
            if (!ModelState.IsValid) return RedirectToAction(nameof(Packages));
            if (pkg.Id == 0) await _uow.Packages.AddAsync(pkg);
            else _uow.Packages.Update(pkg);
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Packages));
        }

        [HttpPost]
        public async Task<IActionResult> DeletePackage(int id)
        {
            var pkg = await _uow.Packages.GetByIdAsync(id);
            if (pkg != null)
            {
                var bookings = await _uow.Bookings.FindAsync(b => b.PackageId == id);
                foreach (var b in bookings)
                {
                    _uow.Bookings.Remove(b);
                }

                _uow.Packages.Remove(pkg);
                await _uow.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Packages));
        }

        public async Task<IActionResult> Bookings()
        {
            var bookings = await _uow.Bookings.GetAllIncludingAsync(b => b.Package, b => b.User);
            return View(bookings);
        }
    }
}
