using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GridFilteringMVC.Data;
using GridFilteringMVC.Models;
using System.Threading.Tasks;
using System.Data.Entity;

namespace GridFilteringMVC.Controllers
{
    public class HomeController : Controller
    {
        private MyDBContext db = new MyDBContext();

        public async Task<ActionResult> Index()
        {
            var departments = await db.Departments.ToListAsync();
            var employees = await db.Employees.ToListAsync();
            ViewBag.Departments = new SelectList(departments, "DepartmentID", "DepartmentName");
            ViewBag.Employees = new SelectList(employees, "ID", "EmployeeName");
            ViewBag.EmployeesNames = db.Employees.Distinct().Select(i => new SelectListItem() { Text = i.EmployeeName, Value = i.ID.ToString() }).ToList();

            List<EmployeeViewModel> employeeViewModels = new List<EmployeeViewModel>();
            foreach (var emp in employees)
            {
                employeeViewModels.Add(convertToEmployeeViewModel(emp));
            }
            
            return View();
        }

        public async Task<ActionResult> GetEmployeesByDepartment(string DepartmentName)
        {
            var departments = await db.Departments.ToListAsync();
            ViewBag.Departments = new SelectList(departments, "DepartmentID", "DepartmentName");

            List<Employee> employees = db.Employees.ToList();

            var empFromDepartment = employees.Where(x => x.Department.DepartmentName.Equals(DepartmentName));
            List<EmployeeViewModel> employeeViewModels = new List<EmployeeViewModel>();
            foreach (var emp in empFromDepartment)
            {
                employeeViewModels.Add(convertToEmployeeViewModel(emp));
            }
            return Json(employeeViewModels, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> GetEmployees()
        {
            var employees = await db.Employees.ToListAsync();

            List<EmployeeViewModel> employeeViewModels = new List<EmployeeViewModel>();

            foreach (var emp in employees)
            {
                employeeViewModels.Add(convertToEmployeeViewModel(emp));

            }

            return Json(employeeViewModels, JsonRequestBehavior.AllowGet);

        }
        
        [AcceptVerbs(HttpVerbs.Post)]
        public async Task<JsonResult> AutoCompleteEmployeeName(string text)
        {
            ViewBag.EmployeesNames = db.Employees.Distinct().Select(i => new SelectListItem() { Text = i.EmployeeName, Value = i.ID.ToString() }).ToList();

            List<Employee> employees = await db.Employees.ToListAsync();
            
            var employeeName = (from e in employees
                                where e.EmployeeName.ToLower().Contains(text)
                                select new { e.EmployeeName });

            return Json(employeeName, JsonRequestBehavior.AllowGet);

        }

        public async Task<ActionResult> GetEmployeesByName(string search)
        {
            var employees = await db.Employees.Where(emp => emp.EmployeeName.ToLower().Contains(search.ToLower())).ToListAsync();

            List<EmployeeViewModel> employeeViewModels = new List<EmployeeViewModel>();

            foreach (var emp in employees)
            {
                employeeViewModels.Add(convertToEmployeeViewModel(emp));

            }

            return Json(employeeViewModels, JsonRequestBehavior.AllowGet);

        }

        public async Task<ActionResult> GetEmployeeByGrade(int grade)
        {
            var employees = await db.Employees.Where(p => p.Grade == grade).ToListAsync();

            List<EmployeeViewModel> employeeViewModels = new List<EmployeeViewModel>();
            foreach (var emp in employees)
            {
                employeeViewModels.Add(convertToEmployeeViewModel(emp));
            }

            return Json(employeeViewModels, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> GetEmployeesByManager(string manager)
        {
            List<Employee> employees = await db.Employees.ToListAsync();
           
            //var empByManager = employees.Where(x => x.PerformanceManager.EmployeeName.Equals(manager)).ToList();

            List<EmployeeViewModel> employeeViewModels = new List<EmployeeViewModel>();
            
            foreach (var emp in employees)
            {
                employeeViewModels.Add(convertToEmployeeViewModel(emp));
            }
            var result = new List<EmployeeViewModel>();

            foreach (var emp in employeeViewModels)
            {
                if (emp.PerformanceManagerName.Equals(manager))
                {
                    result.Add(emp);
                }
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> GetEmployeesHiredAfter(DateTime hireDate)
        {
            List<Employee> employees = await db.Employees.ToListAsync();
            var empHiredAfter = employees.Where(e => e.HireDate.CompareTo(hireDate) >= 0);
            List<EmployeeViewModel> employeeViewModels = new List<EmployeeViewModel>();
           
            foreach (var emp in empHiredAfter)
            {
                employeeViewModels.Add(convertToEmployeeViewModel(emp));
            }
            return Json(employeeViewModels, JsonRequestBehavior.AllowGet);
        }


        private EmployeeViewModel convertToEmployeeViewModel(Employee employee)
        {
            EmployeeViewModel employeeViewModel = new EmployeeViewModel
            {
                DepartmentName = employee.Department.DepartmentName,
                EmployeeName = employee.EmployeeName,
                Grade = employee.Grade,
                HireDate = employee.HireDate,
                ID = employee.ID,
                PerformanceManagerName = (employee.PerformanceManager != null) ? employee.PerformanceManager.EmployeeName : "No one is my manager"
                
            };

            return employeeViewModel;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

    }
}