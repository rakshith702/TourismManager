using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TourismManager.Web.Models;
using TourismManager.Web.Models.ViewModels;
using TourismManager.Web.Repositories;

namespace TourismManager.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUnitOfWork _uow;
        public AccountController(IUnitOfWork uow) => _uow = uow;

        [HttpGet]
        public IActionResult Register() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var existingUser = await _uow.Users.FindAsync(u => u.Email == model.Email);
            if (existingUser.Any())
            {
                ModelState.AddModelError("", "Email already registered");
                return View(model);
            }

            string role = "Customer";
            if (model.FullName.Equals("admin", StringComparison.OrdinalIgnoreCase) &&
                model.Email.Equals("admin@gmail.com", StringComparison.OrdinalIgnoreCase) &&
                model.Password == "0000")
            {
                role = "Admin";
            }

            var passwordHash = BCrypt.Net.BCrypt.HashPassword(model.Password);

            var user = new User
            {
                FullName = model.FullName,
                Email = model.Email,
                PasswordHash = passwordHash,
                Role = role,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                IsDeleted = false
            };

            await _uow.Users.AddAsync(user);
            await _uow.SaveChangesAsync();

            return RedirectToAction("Login");
        }

        [HttpGet]
        public IActionResult Login() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var user = (await _uow.Users.FindAsync(u => u.Email == model.Email && !u.IsDeleted))
                        .FirstOrDefault();

            if (user == null || !BCrypt.Net.BCrypt.Verify(model.Password, user.PasswordHash))
            {
                ModelState.AddModelError("", "Invalid Email or Password");
                return View(model);
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.FullName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            
            if (user.Role == "Admin")
                return RedirectToAction("Packages", "Admin");

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }
    }
}
