using ErvinPortfolio.DataAccess.Repository;
using ErvinPortfolio.DataAccess.Repository.IRepository;
using ErvinPortfolio.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace ErvinPortfolioWeb.Controllers
{
    public class ManageProjectController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ManageProjectController(IUnitOfWork obj, IWebHostEnvironment hostEnvironment)
        {
            _unitOfWork = obj;
            _webHostEnvironment = hostEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Upsert(int? id)
        {
            ProjectVM projectVM = new()
            {
                Project = new()
            };


            if (id == null || id == 0)
            {
                return View(projectVM);
            }

            else
            {
                projectVM.Project = _unitOfWork.Project.GetFirstOrDefault(u => u.Id == id);
                return View(projectVM);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(ProjectVM obj, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(wwwRootPath, @"images\");
                    var extension = Path.GetExtension(file.FileName);

                    if (obj.Project.ImageUrl != null)
                    {
                        var oldImagePath = Path.Combine(wwwRootPath, obj.Project.ImageUrl.TrimStart('\\'));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }
                    using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                    {
                        file.CopyTo(fileStreams);
                    }
                    obj.Project.ImageUrl = @"\images\" + fileName + extension;
                }
                if (obj.Project.Id == 0)
                {
                    _unitOfWork.Project.Add(obj.Project);
                }
                else
                {
                    _unitOfWork.Project.Update(obj.Project);
                }
                _unitOfWork.Save();
                TempData["success"] = "Project has been successfully updated";
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            var productList = _unitOfWork.Project.GetAll();
            return Json(new { data = productList });
        }
        #endregion
    }
}
