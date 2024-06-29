namespace EmployeesApp.DataAccess.Entities
{
    public class ExperienceEntity : BaseEntity
    {
        public int EmployeeId { get; set; }
        public EmployeeEntity? Employee { get; set; }
        public int ProgrammingLanguageId { get; set; }
        public ProgrammingLanguageEntity? ProgrammingLanguage { get; set; }
    }
}
