using EmployeesApp.DataAccess.Enums;

namespace EmployeesApp.DataAccess.Entities
{
    public class EmployeeEntity : BaseEntity
    {
        public EmployeeEntity()
        {
            Experiences = new HashSet<ExperienceEntity>();
        }

        public string? Name { get; set; }
        public string? SecondName { get; set; }
        public DateTime BirthDate { get; set; }
        public Sex Sex { get; set; }
        public int DepartmentId { get; set; }
        public DepartmentEntity? Department { get; set; }

        public ICollection<ExperienceEntity> Experiences { get; set; }
    }
}
