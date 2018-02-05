using GridFilteringMVC.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;

namespace GridFilteringMVC.Data
{
    public class DatabaseInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<MyDBContext>
    {
        protected override void Seed(MyDBContext context)
        {
            var departments = new List<Department>
            {
                new Department {DepartmentID = 1, DepartmentName = "IT",},
                new Department {DepartmentID = 2, DepartmentName = "Marketing",},
                new Department {DepartmentID = 3, DepartmentName = "Sales",},
                new Department {DepartmentID = 4, DepartmentName = "HR",},
                new Department {DepartmentID = 6, DepartmentName = "Shipping",},
                new Department {DepartmentID = 5, DepartmentName = "Management",}

            };

            departments.ForEach(d => context.Departments.AddOrUpdate(d));
            context.SaveChanges();

            var employees = new List<Employee>
            {
                new Employee
                {
                    DepartmentID = 1,
                    EmployeeName = "Klaudia",
                    Grade = 10,
                    HireDate = DateTime.Parse("2011-02-05"),
                    ID = 1
                    //PerformanceManager =
                    //    new Employee
                    //    {
                    //        DepartmentID = 2,
                    //        EmployeeName = "Monika",
                    //        Grade = 9,
                    //        HireDate = DateTime.Parse("2015-12-05"),
                    //        ID = 8
                    //    }

                },
                new Employee
                {
                    DepartmentID = 2,
                    EmployeeName = "Kamila",
                    Grade = 9,
                    HireDate = DateTime.Parse("2015-12-05"),
                    ID = 2
                    //PerformanceManager =
                    //    new Employee
                    //    {
                    //        DepartmentID = 2,
                    //        EmployeeName = "Igor",
                    //        Grade = 7,
                    //        HireDate = DateTime.Parse("2017-12-05"),
                    //        ID = 9
                    //    }
                },
                new Employee
                {
                    DepartmentID = 5,
                    EmployeeName = "Oskar",
                    Grade = 10,
                    HireDate = DateTime.Parse("2014-02-05"),
                    ID = 3

                },
                new Employee
                {
                    DepartmentID = 5,
                    EmployeeName = "Max",
                    Grade = 10,
                    HireDate = DateTime.Parse("2011-02-05"),
                    ID = 4

                },
                new Employee
                {
                    DepartmentID = 2,
                    EmployeeName = "Michał",
                    Grade = 10,
                    HireDate = DateTime.Parse("2012-02-05"),
                    ID = 5

                },
                new Employee
                {
                    DepartmentID = 1,
                    EmployeeName = "Karolina",
                    Grade = 10,
                    HireDate = DateTime.Parse("2010-02-05"),
                    ID = 6,


                },
                new Employee
                {
                    DepartmentID = 3,
                    EmployeeName = "Olaf",
                    Grade = 10,
                    HireDate = DateTime.Parse("2009-03-05"),
                    ID = 7

                },

            };

            employees.ForEach(e => context.Employees.AddOrUpdate(e));
            context.SaveChanges();

        }
    }
}