namespace EmployeesApp.DataAccess.Entities
{
    public abstract class BaseEntity : IEquatable<BaseEntity>
    {
        public int Id { get; set; }

        public override bool Equals(object? obj)
        {
            if (obj == null)
                return false;

            var other = obj as BaseEntity;
            if (other == null) 
                return false;

            return Equals(other);
        }

        public bool Equals(BaseEntity? other)
        {
            if (other == null)
                return false;

            return Id == other.Id;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
