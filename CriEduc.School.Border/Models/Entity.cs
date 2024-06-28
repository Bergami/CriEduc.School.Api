namespace CriEduc.School.Border.Models
{
    public abstract class Entity<T> where T : Entity<T>
    {
        public Guid Id { get; protected set; }
        public DateTime Created { get; protected set; }
    }
}
