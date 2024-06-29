using EmployeesApp.Core.Models;
using EmployeesApp.DataAccess;
using EmployeesApp.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmployeesApp.Core.Services
{
    public class EmployeesService : IEmployeesService
    {
        private readonly AppDbContext _dbContext;

        public EmployeesService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Employee[]> GetAllAsync()
        {
            var entities = await _dbContext.Employees
				.Include(x => x.Department)
				.Include(x => x.Experiences)
                .ThenInclude(x => x.ProgrammingLanguage)
				.AsNoTracking().ToArrayAsync();

            return entities.Select(Employee.Create).ToArray();
        }

        public async Task CreateAsync(Employee newEmployee)
        {
            var newEntity = new EmployeeEntity
            {
                Name = newEmployee.Name,
                SecondName = newEmployee.SecondName,
                BirthDate = newEmployee.BirthDate,
				DepartmentId = newEmployee.DepartmentId,
                Sex = newEmployee.Sex,
                Experiences = [ new() { ProgrammingLanguageId = newEmployee.ProgrammingLanguageId } ]
            };

            await _dbContext.AddAsync(newEntity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Employee newEmployee)
        {
            var employee = await _dbContext.Employees
                .Include(x => x.Experiences)
                .FirstOrDefaultAsync(x => x.Id == newEmployee.Id);
            if (employee != null)
            {
                employee.BirthDate = newEmployee.BirthDate;
                employee.DepartmentId = newEmployee.DepartmentId;
                employee.Name = newEmployee.Name;
                employee.SecondName = newEmployee.SecondName;
                employee.Sex = newEmployee.Sex;

                var oldExperience = await _dbContext.Experiences.FirstOrDefaultAsync(x => x.Id == employee.Id);
                if (oldExperience != null)
                {
                    _dbContext.Remove(oldExperience);
                }

                _dbContext.Experiences.Add(new ExperienceEntity
                {
                    ProgrammingLanguageId = newEmployee.ProgrammingLanguageId,
                    EmployeeId = employee.Id
                });

                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(int id)
        {
            var employee = await _dbContext.Employees.FirstOrDefaultAsync(x => x.Id == id);
            if (employee != null)
            {
                _dbContext.Employees.Remove(employee);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<Department[]> GetDepartmentsAsync()
        {
            var entities = await _dbContext.Departments
                .AsNoTracking()
                .ToArrayAsync();

            return entities.Select(Department.Create).ToArray();
        }

        public async Task<ProgrammingLanguage[]> GetProgrammingLanguagesAsync()
        {
            var entities = await _dbContext.ProgrammingLanguages
                .AsNoTracking()
                .ToArrayAsync();

            return entities.Select(ProgrammingLanguage.Create).ToArray();
        }
    }
}
