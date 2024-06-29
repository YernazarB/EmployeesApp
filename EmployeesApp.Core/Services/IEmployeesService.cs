using EmployeesApp.Core.Models;

namespace EmployeesApp.Core.Services
{
    public interface IEmployeesService
    {
        Task<Employee[]> GetAllAsync();
        Task CreateAsync(Employee newEmployee);
        Task UpdateAsync(Employee newEmployee);
        Task DeleteAsync(int id);

        Task<Department[]> GetDepartmentsAsync();
        Task<ProgrammingLanguage[]> GetProgrammingLanguagesAsync();
    }
}
