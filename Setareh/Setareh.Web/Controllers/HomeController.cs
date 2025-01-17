using Microsoft.AspNetCore.Mvc;

namespace Setareh.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {            
            return RedirectToAction("Index","AboutMe");
        }
    }
}
