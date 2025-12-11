using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SampleFlaskLogin.Controllers
{
    /// <summary>
    /// Secured home controller.
    /// Requires user authentication to access the Index page.
    /// </summary>
    [Authorize]
    public class HomeController : Controller
    {
        /// <summary>
        /// Default dashboard / landing page after login.
        /// </summary>
        public IActionResult Index()
        {
            return View();
        }
    }
}
