using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GridFilteringMVC.Models
{
    [Table("tblEmployee")]
    public class Employee
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [Display(Name = "Employee Name")]
        [StringLength(50, MinimumLength = 3)]
        public string EmployeeName { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Hire Date")]
        public DateTime HireDate { get; set; }

        [Range(1, 10, ErrorMessage = "The value must be in range 1-10")]
        [DisplayFormat(NullDisplayText = "No grade")]
        public int? Grade { get; set; }

        [ForeignKey("Department")]
        public int DepartmentID { get; set; }

        public virtual Department Department { get; set; }
        
        public virtual Employee PerformanceManager { get; set; }
    }
}