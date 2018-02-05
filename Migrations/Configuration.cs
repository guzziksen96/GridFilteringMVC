using System.Data.Entity.Validation;

namespace GridFilteringMVC.Migrations
{
    using GridFilteringMVC.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<GridFilteringMVC.Data.MyDBContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(GridFilteringMVC.Data.MyDBContext context)
        {
            Employee manager = new Employee
            {
                DepartmentID = 2,
                EmployeeName = "Igor",
                Grade = 10,
                HireDate = DateTime.Parse("2008-12-05"),
                ID = 8
            };

            var employees = new List<Employee>
            {
                manager,
                new Employee
                {
                    DepartmentID = 1,
                    EmployeeName = "Klaudia",
                    Grade = 10,
                    HireDate = DateTime.Parse("2011-02-05"),
                    ID = 1,
                    PerformanceManager = manager

                },
                new Employee
                {
                    DepartmentID = 2,
                    EmployeeName = "Kamila",
                    Grade = 9,
                    HireDate = DateTime.Parse("2015-12-05"),
                    ID = 2,
                    PerformanceManager = manager

                },
                new Employee
                {
                    DepartmentID = 5,
                    EmployeeName = "Oskar",
                    Grade = 10,
                    HireDate = DateTime.Parse("2014-02-05"),
                    ID = 3,
                    PerformanceManager = manager

                },
                new Employee
                {
                    DepartmentID = 5,
                    EmployeeName = "Max",
                    Grade = 10,
                    HireDate = DateTime.Parse("2011-02-05"),
                    ID = 4,
                    PerformanceManager = manager

                },
                new Employee
                {
                    DepartmentID = 2,
                    EmployeeName = "Micha³",
                    Grade = 10,
                    HireDate = DateTime.Parse("2012-02-05"),
                    ID = 5,
                    PerformanceManager = manager

                },
                new Employee
                {
                    DepartmentID = 1,
                    EmployeeName = "Karolina",
                    Grade = 10,
                    HireDate = DateTime.Parse("2010-02-05"),
                    ID = 6,
                    PerformanceManager = manager

                },
                new Employee
                {
                    DepartmentID = 3,
                    EmployeeName = "Olaf",
                    Grade = 10,
                    HireDate = DateTime.Parse("2009-03-05"),
                    ID = 7,
                    PerformanceManager = manager

                },

            };
            try
            {
                employees.ForEach(e => context.Employees.AddOrUpdate(e));
                context.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        System.Console.WriteLine("Property: {0} Error: {1}", validationError.PropertyName,
                            validationError.ErrorMessage);
                    }
                }
            }
        }
    }
}
