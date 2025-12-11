using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using SampleFlaskLogin.Models;
using SampleFlaskLogin.Services;

namespace SampleFlaskLogin.Controllers
{
    /// <summary>
    /// Handles user authentication: Login, Logout, Access Denied pages.
    /// </summary>
    public class AccountController : Controller
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        // ============================================================
        // 🔐 Login (GET)
        // ============================================================
        [HttpGet]
        public IActionResult Login(string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View(new LoginViewModel());
        }

        // ============================================================
        // 🔐 Login (POST)
        // ============================================================
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model, string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;

            // Validate model state (required fields)
            if (!ModelState.IsValid)
                return View(model);

            // Check user credentials
            if (!_userService.ValidateUser(model.Username, model.Password))
            {
                ModelState.AddModelError(string.Empty, "Invalid username or password.");
                return View(model);
            }

            // Create authentication ticket
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, model.Username)
            };

            var identity = new ClaimsIdentity(
                claims, 
                CookieAuthenticationDefaults.AuthenticationScheme
            );

            var principal = new ClaimsPrincipal(identity);

            // Sign in user using cookie auth
            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                principal
            );

            // Redirect to original URL if safe
            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                return Redirect(returnUrl);

            // Default redirect after login
            return RedirectToAction("Index", "Home");
        }

        // ============================================================
        // 🔓 Logout
        // ============================================================
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }

        // ============================================================
        // 🚫 Access Denied
        // ============================================================
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
