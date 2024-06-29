using EmployeesApp.DataAccess.Entities;
using EmployeesApp.DataAccess.Enums;

namespace EmployeesApp.Core.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? SecondName { get; set; }
        public DateTime BirthDate { get; set; }
        public Sex Sex { get; set; }
        public int DepartmentId { get; set; }
        public string? DepartmentName { get; set; }

        public int ProgrammingLanguageId { get; set; }
		public string? ProgrammingLanguage { get; set; }

		public int Age
        {
            get
            {
                var currentDate = DateTime.Now.Date;
                var currentYearBirthDay = new DateTime(currentDate.Year, BirthDate.Month, BirthDate.Day);

                if (currentYearBirthDay > currentDate)
                {
                    return currentDate.Year - BirthDate.Year - 1;
                }

                return currentDate.Year - BirthDate.Year;
            }
        }

        public static Employee Create(EmployeeEntity entity)
        {
            var employee = new Employee
            {
                Id = entity.Id,
                Name = entity.Name,
                BirthDate = entity.BirthDate,
                DepartmentId = entity.DepartmentId,
				DepartmentName = entity.Department?.Name,
				SecondName = entity.SecondName,
                Sex = entity.Sex,
				ProgrammingLanguageId = entity.Experiences.First().ProgrammingLanguageId,
                ProgrammingLanguage = entity.Experiences.First().ProgrammingLanguage?.Name
            };

            return employee;
        }
    }
}
