namespace StudentsManagement.Entities
{
    public class Course
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid InstitutionId { get; set; }
        public Institution Institution { get; set; }
        public ICollection<CourseSubject> CourseSubjects { get; set; } = new List<CourseSubject>();
        public ICollection<Student> Students { get; set; } = new List<Student>();
    }
}