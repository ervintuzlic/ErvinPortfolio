using Microsoft.AspNetCore.Mvc;

namespace ErvinPortfolioWeb.Controllers
{
    public class ProjectController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
