namespace StudentsManagement.Entities
{
    public class Subject
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<CourseSubject> CourseSubjects { get; set; } = new List<CourseSubject>();
    }
}