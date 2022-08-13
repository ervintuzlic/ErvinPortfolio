using ErvinPortfolio.DataAccess.Repository.IRepository;
using ErvinPortfolio.Models;
using ErvinPortfolio.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;

namespace ErvinPortfolioWeb.Controllers
{
    public class ProjectController : Controller
    {
        private readonly ILogger<ProjectController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public ProjectController(ILogger<ProjectController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }
        public IActionResult Details(int id)
        {
            Project project = _unitOfWork.Project.GetFirstOrDefault(u => u.Id == id);
            return View(project);
        }

        public IActionResult Index()
        {
            IEnumerable<Project> productList = _unitOfWork.Project.GetAll();
            return View(productList);
        }
    }
}
