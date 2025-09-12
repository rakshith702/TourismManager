using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TourismManager.Web.Models;
using TourismManager.Web.Repositories;

namespace TourismManager.Web.Controllers
{
    [Authorize]
    public class BookingsController : Controller
    {
        private readonly IUnitOfWork _uow;
        public BookingsController(IUnitOfWork uow) { _uow = uow; }

        [HttpGet]
        public async Task<IActionResult> Create(int packageId)
        {
            var pkg = await _uow.Packages.GetByIdAsync(packageId);
            if (pkg == null) return NotFound();

            ViewBag.Package = pkg;
            return View(new Booking
            {
                PackageId = packageId,
                TravelDate = DateTime.Today.AddDays(7),
                Persons = 1,
                TotalAmount = pkg.Price
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Booking model)
        {
            var pkg = await _uow.Packages.GetByIdAsync(model.PackageId);
            if (pkg == null) return NotFound();

            if (model.Persons <= 0)
                ModelState.AddModelError("Persons", "Persons must be at least 1");

            if (!ModelState.IsValid)
            {
                ViewBag.Package = pkg;
                return View(model);
            }

            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == System.Security.Claims.ClaimTypes.NameIdentifier);
            if (userIdClaim == null) return RedirectToAction("Login", "Account");

            model.UserId = int.Parse(userIdClaim.Value);
            model.TotalAmount = pkg.Price * model.Persons;
            model.Status = "Pending";
            model.CreatedAt = DateTime.UtcNow;
            model.IsDeleted = false;

            await _uow.Bookings.AddAsync(model);
            await _uow.SaveChangesAsync();

            return RedirectToAction("Pay", "Payments", new { bookingId = model.Id });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Cancel(int id)
        {
            var booking = await _uow.Bookings.GetByIdAsync(id);
            if (booking == null)
            {
                TempData["err"] = "Booking not found.";
                return RedirectToAction("MyBookings");
            }

            if (booking.IsCancelled)
            {
                TempData["err"] = "This booking is already cancelled.";
                return RedirectToAction("MyBookings");
            }

            booking.IsCancelled = true;
            booking.CancelledAt = DateTime.UtcNow;
            booking.Status = "Cancelled";

            decimal refundAmount = booking.TotalAmount * 0.85m;

            var refund = new Refund
            {
                BookingId = booking.Id,
                RefundedAmount = refundAmount
            };

            await _uow.Refunds.AddAsync(refund);
            _uow.Bookings.Update(booking);

            await _uow.SaveChangesAsync();

            TempData["ok"] = $"Booking cancelled successfully. Refund of ₹{refundAmount} has been processed.";
            return RedirectToAction("MyBookings");
        }

        [HttpGet]
        public async Task<IActionResult> MyBookings()
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == System.Security.Claims.ClaimTypes.NameIdentifier);
            if (userIdClaim == null) return RedirectToAction("Login", "Account");

            int userId = int.Parse(userIdClaim.Value);
            var bookings = await _uow.Bookings.GetByUserAsync(userId);

            return View(bookings);
        }
    }
}
