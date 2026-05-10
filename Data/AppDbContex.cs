using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SchoolManagmentSystem.Models;

namespace SchoolManagmentSystem.Data
{
    public class AppDbContex : IdentityDbContext<ApplicationUser>
    {
        public AppDbContex(DbContextOptions<AppDbContex> options) : base(options) { }
        

        public DbSet<Student> Students => Set<Student>();
        public DbSet<Subject> Subjects => Set<Subject>();
        public DbSet<Grade> Grades => Set<Grade>();
    }
}
