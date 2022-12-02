using EmpDeptProject.DataAccess.Data;
using EmpDeptProject.DataAccess.Repository;
using EmpDeptProject.DataAccess.Repository.IRepository;
using EmpDeptProject.Models.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;



namespace EmpDeptProject.Web.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly IUnitOfWork _unitofWork;
        public readonly EmpDeptDbContext _db;
        public EmployeesController(IUnitOfWork unitofWork, EmpDeptDbContext db)
        {
       

            _unitofWork = unitofWork;
            _db = db;
        }
        public IActionResult Index()
        {
            var allEmployees = _db.Employees.Include(e => e.Departments);
            // var allEmployees = _unitofWork.Employee.GetAll();

            return View(allEmployees);
        }
        public IActionResult Details(int id)
        {
            var allEmployees = _unitofWork.Employee.GetFirstorDefault(c => c.EmpId == id);
            return View(allEmployees);
        }
        public IActionResult Create()
        {
            ViewBag.DeptId = new SelectList(_unitofWork.Department.GetAll(), "DeptId", "DeptName");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Employee Obj)
        {
            if (ModelState.IsValid)
            {
                _unitofWork.Employee.Add(Obj);
                //_db.SaveChanges();
                _unitofWork.Save();
                TempData["Success"] = "Employee added successfully";
                return RedirectToAction(nameof(Index));
            }



            ViewBag.DeptId = new SelectList(_unitofWork.Department.GetAll(), "DeptId", "DeptName");

            return View(Obj);




        }
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var employeeFirstorDefault = _unitofWork.Employee.GetFirstorDefault(c => c.EmpId == id);//SigleorDefailt(),FirstorDefault()
            if (employeeFirstorDefault == null)
            {
                return NotFound();
            }
            ViewBag.DeptId = new SelectList(_unitofWork.Department.GetAll(), "DeptId", "DeptName");

            return View(employeeFirstorDefault);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Edit")]
        public IActionResult Edit(Employee Obj)
        {



            if (ModelState.IsValid)
            {
                _unitofWork.Employee.Update(Obj);
                _unitofWork.Save();
                TempData["Success"] = " Employee updated successfully";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ViewBag.DeptId = new SelectList(_unitofWork.Department.GetAll(), "DeptId", "DeptName");
                return View(Obj);
            }



        }
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var employeeFromDb = _unitofWork.Employee.GetFirstorDefault(c => c.EmpId == id);//SigleorDefailt(),FirstorDefault()
            if (employeeFromDb == null)
            {
                return NotFound();
            }
            return View(employeeFromDb);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {
            var obj = _unitofWork.Employee.GetFirstorDefault(c => c.EmpId == id);
            if (obj == null)
            {
                return NotFound();
            }
            _unitofWork.Employee.Remove(obj);
            _unitofWork.Save();
            TempData["Success"] = "Employee deleted successfully";
            return RedirectToAction(nameof(Index));
        }
    }
}