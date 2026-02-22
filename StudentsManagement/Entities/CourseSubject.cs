namespace StudentsManagement.Entities
{
    public class CourseSubject
    {
        public Guid CourseId { get; set; }
        public virtual Course Course { get; set; }
        public Guid SubjectId { get; set; }
        public virtual Subject Subject { get; set; }
    }
}
