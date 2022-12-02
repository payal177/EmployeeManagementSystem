using EmpDeptProject.DataAccess.Repository.IRepository;
using EmpDeptProject.Models.Models;
using Microsoft.AspNetCore.Mvc;

namespace EmpDeptProject1.web.Controllers
{
    public class DepartmentsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public DepartmentsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public IActionResult Index()
        {
            var allDepartments = _unitOfWork.Department.GetAll();

            return View(allDepartments);
        }
        public IActionResult Details(int? id)
        {
            var allDepartment = _unitOfWork.Department.GetFirstorDefault(c => c.DeptId == id);

            return View(allDepartment);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Department Obj)
        {

            if (ModelState.IsValid)
            {
                _unitOfWork.Department.Add(Obj);
                _unitOfWork.Save();
                TempData["Success"] = "Department added successfully";
                return RedirectToAction(nameof(Index));

            }
            else
            {
                return View(Obj);
            }
        }
        //Edit get
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            // var categoryFromDb = _categoryRepository.GetFirstOrDefault(id);
            var ObjFirstOrDefault = _unitOfWork.Department.GetFirstorDefault(c => c.DeptId == id);
            // var categorySingleOrDefault = _db.Categories.SingleOrDefault(c => c.Id == id);
            if (ObjFirstOrDefault == null)
            {
                return NotFound();
            }
            return View(ObjFirstOrDefault);
       
        }
        //Edit-post
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Edit")]
        public IActionResult Edit(Department Obj)
        {

            if (ModelState.IsValid)
            {
                _unitOfWork.Department.Update(Obj);
                _unitOfWork.Save();
                TempData["Success"] = "Edited successfully";
                return RedirectToAction(nameof(Index));

            }
            else
            {

                return View(Obj);
            }

        }
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var ObjFromDb = _unitOfWork.Department.GetFirstorDefault(c => c.DeptId == id);

            if (ObjFromDb == null)
            {
                return NotFound();
            }
            return View(ObjFromDb);
         
        }
        [HttpPost]
        [ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {
            var obj = _unitOfWork.Department.GetFirstorDefault(c => c.DeptId == id);
            if (obj == null)
            {
                return NotFound();
            }
            _unitOfWork.Department.Remove(obj);
            _unitOfWork.Save();
            TempData["Success"] = "Deleted successfully";
            return RedirectToAction(nameof(Index));

        }
    }
}




        