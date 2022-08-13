using Microsoft.AspNetCore.Mvc;

namespace ErvinPortfolioMain.Controllers
{
    public class AboutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
