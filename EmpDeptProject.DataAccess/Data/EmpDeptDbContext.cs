using EmpDeptProject.Models.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpDeptProject.DataAccess.Data
{
    public class EmpDeptDbContext : DbContext
    {
        public EmpDeptDbContext(DbContextOptions<EmpDeptDbContext> options) : base
            (options)
        {

        }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
    }
}
