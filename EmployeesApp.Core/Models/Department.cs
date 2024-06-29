using EmployeesApp.DataAccess.Entities;

namespace EmployeesApp.Core.Models
{
    public class Department
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int Floor { get; set; }

        public static Department Create(DepartmentEntity department)
        {
            return new Department
            {
                Id = department.Id,
                Name = department.Name,
                Floor = department.Floor
            };
        }
    }
}
