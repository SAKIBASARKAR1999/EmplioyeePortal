using Employeeportal.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace Employeeportal.data
{
    public class dbDATA : IdentityDbContext<User>
    {
        public dbDATA(DbContextOptions<dbDATA> options) : base(options)
        {

        }

        public DbSet<employee> employees { get; set; }
        public DbSet<designation> designations { get; set; }
        public DbSet<department> departments { get; set; }


        public DbSet<employeeINFO> employeeINFOs { get; set; }


    }
}
