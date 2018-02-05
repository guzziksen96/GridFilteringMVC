using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using GridFilteringMVC.Models;

namespace GridFilteringMVC.Data
{
    public class MyDBContext : DbContext
    {
        public MyDBContext() : base("MyDBContext")
        {

        }
        public static MyDBContext Create()
        {
            return new MyDBContext();
        }


        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }

    }
}