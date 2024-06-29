using EmployeesApp.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmployeesApp.DataAccess
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<DepartmentEntity> Departments { get; set; }
        public DbSet<EmployeeEntity> Employees { get; set; }
        public DbSet<ProgrammingLanguageEntity> ProgrammingLanguages { get; set; }
        public DbSet<ExperienceEntity> Experiences { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
