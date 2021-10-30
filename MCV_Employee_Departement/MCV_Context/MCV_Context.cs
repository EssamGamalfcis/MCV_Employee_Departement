using MCV_Employee_Department.API.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MCV_Employee_Department.API.MCV_Context
{
    public class MCVContext : DbContext
    {
        public MCVContext(DbContextOptions options)
       : base(options)
        {
        }
        public DbSet<Employee> Employee { get; set; }
        public DbSet<Department> Department { get; set; }
    }
}
