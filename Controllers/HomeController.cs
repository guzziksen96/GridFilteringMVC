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

        public ActionResult Index()
        {
            var departments = db.Departments.ToList();
            var employees = db.Employees.ToList();
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

        public ActionResult GetEmployeesByDepartment(string DepartmentName)
        {
            var departments = db.Departments.ToList();
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

        public ActionResult GetEmployees()
        {
            var employees = db.Employees.ToList();

            List<EmployeeViewModel> employeeViewModels = new List<EmployeeViewModel>();

            foreach (var emp in employees)
            {
                employeeViewModels.Add(convertToEmployeeViewModel(emp));

            }

            return Json(employeeViewModels, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public async Task<JsonResult> AutoCompleteEmployeeName(string text)
        {
            ViewBag.EmployeesNames = db.Employees.Distinct().Select(i => new SelectListItem() { Text = i.EmployeeName, Value = i.ID.ToString() }).ToList();

            List<Employee> employees = await db.Employees.ToListAsync();

            //if (!string.IsNullOrEmpty(text))
            //{
            //    employees = employees.Where(p => p.EmployeeName.StartsWith(text)).ToList();

            //}

            var employeeName = (from e in employees
                                where e.EmployeeName.Contains(text)
                                select new { e.EmployeeName });

            return Json(employeeName, JsonRequestBehavior.AllowGet);

        }

        public ActionResult GetEmployeesByName(string search)
        {
            var employees = db.Employees.Where(emp => emp.EmployeeName.ToLower().Contains(search.ToLower()));

            List<EmployeeViewModel> employeeViewModels = new List<EmployeeViewModel>();

            foreach (var emp in employees)
            {
                employeeViewModels.Add(convertToEmployeeViewModel(emp));

            }

            return Json(employeeViewModels, JsonRequestBehavior.AllowGet);

        }

        public ActionResult GetEmployeeByGrade(int grade)
        {
            var employees = db.Employees.Where(p =>
                p.Grade == grade).ToList();

            List<EmployeeViewModel> employeeViewModels = new List<EmployeeViewModel>();
            foreach (var emp in employees)
            {
                employeeViewModels.Add(convertToEmployeeViewModel(emp));
            }

            return Json(employeeViewModels);
        }

        public ActionResult GetEmployeesByManager(string Manager)
        {
            List<Employee> employees = db.Employees.ToList();
            var empByManager = employees.Where(x => x.PerformanceManager.EmployeeName.Equals(Manager));
            List<EmployeeViewModel> employeeViewModels = new List<EmployeeViewModel>();
            
            foreach (var emp in empByManager)
            {
                employeeViewModels.Add(convertToEmployeeViewModel(emp));
            }
            return Json(employeeViewModels);
        }

        public ActionResult GetEmployeesHiredAfter(DateTime hireDate)
        {
            List<Employee> employees = db.Employees.ToList();

            var empHiredAfter = employees.Where(x => x.HireDate >= hireDate);

            List<EmployeeViewModel> employeeViewModels = new List<EmployeeViewModel>();

            foreach (var emp in empHiredAfter)
            {
                employeeViewModels.Add(convertToEmployeeViewModel(emp));
            }
            return Json(employeeViewModels);
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


        private SelectListItem GetPerformanceManager(int ID)
        {
            var employee = db.Employees.Find(ID);

            SelectListItem si = new SelectListItem { Value = employee.PerformanceManager.ID.ToString(), Text = employee.PerformanceManager.EmployeeName };

            return si;
        }


    }
}