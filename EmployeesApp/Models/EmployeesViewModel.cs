using EmployeesApp.Core.Models;

namespace EmployeesApp.Models
{
    public class EmployeesViewModel
    {
        public Employee[] Employees { get; set; } = [];
        public Department[] Departments { get; set; } = [];
        public ProgrammingLanguage[] ProgrammingLanguages { get; set; } = [];
    }
}
