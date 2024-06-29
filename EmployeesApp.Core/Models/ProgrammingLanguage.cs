using EmployeesApp.DataAccess.Entities;

namespace EmployeesApp.Core.Models
{
    public class ProgrammingLanguage
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        public static ProgrammingLanguage Create(ProgrammingLanguageEntity entity)
        {
            return new ProgrammingLanguage
            {
                Id = entity.Id,
                Name = entity.Name
            };
        }
    }
}
