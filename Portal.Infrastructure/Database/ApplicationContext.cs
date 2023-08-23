using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Portal.Infrastructure.Entities;
using Portal.Infrastructure.Extend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Infrastructure.Database
{
    public partial class ApplicationContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        //this partial for connection string

        public ApplicationContext(DbContextOptions<ApplicationContext> opt): base(opt)
        {
            
        }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("server = .; database = PortalDb; Trusted_Connection = true; MultipleActiveResultSets = true; Trust Server Certificate = true");
        //}
    }

    public partial class ApplicationContext
    {
        //this partial for Dbset

        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<District> Districts { get; set; }

    }
}