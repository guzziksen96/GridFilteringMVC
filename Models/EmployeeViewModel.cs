using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GridFilteringMVC.Models
{
    public class EmployeeViewModel
    {

        public int ID { get; set; }

        public string EmployeeName { get; set; }

        public DateTime HireDate { get; set; }

        public int? Grade { get; set; }
        
        public string PerformanceManagerName { get; set; }
        
        public string DepartmentName { get; set; }
    }
}