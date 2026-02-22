namespace StudentsManagement.Entities
{
    public class Student
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string Email { get; set; }
        public Guid CourseId { get; set; }
        public virtual Course Course { get; set; }
    }
}
