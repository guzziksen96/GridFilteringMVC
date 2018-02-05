using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GridFilteringMVC.Models
{
        [Table("tblDepartment")]
        public class Department
        {
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
            public Department()
            {
                this.Employees = new HashSet<Employee>();
            }

            [Key]
            public int DepartmentID { get; set; }

            [Required]
            [StringLength(50)]
            public string DepartmentName { get; set; }

            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
            public virtual ICollection<Employee> Employees { get; set; }
        
    }
}