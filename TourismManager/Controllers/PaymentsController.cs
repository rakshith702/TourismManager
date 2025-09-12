using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TourismManager.Web.Models;
using TourismManager.Web.Repositories;

namespace TourismManager.Web.Controllers
{
    [Authorize]
    public class PaymentsController : Controller
    {
        private readonly IUnitOfWork _uow;
        public PaymentsController(IUnitOfWork uow) => _uow = uow;

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            var payments = await _uow.Payments
                .GetAllIncludingAsync(p => p.Booking,
                                       p => p.Booking.User,
                                       p => p.Booking.Package);

            return View(payments);
        }

        [HttpGet]
        public async Task<IActionResult> Pay(int bookingId)
        {
            var booking = await _uow.Bookings.GetWithPackageAsync(bookingId);
            if (booking == null) return NotFound();

            return View(booking);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CompletePayment(int bookingId, string transactionRef)
        {
            var booking = await _uow.Bookings.GetByIdAsync(bookingId);
            if (booking == null) return NotFound();

            var payment = new Payment
            {
                BookingId = bookingId,
                Amount = booking.TotalAmount,
                Status = "Success",
                TransactionRef = transactionRef,
                CreatedAt = DateTime.UtcNow
            };

            await _uow.Payments.AddAsync(payment);
            booking.Status = "Confirmed";
            _uow.Bookings.Update(booking);

            await _uow.SaveChangesAsync();

            TempData["ok"] = "Payment successful!";
            return RedirectToAction("MyBookings", "Bookings");
        }
    }
}
