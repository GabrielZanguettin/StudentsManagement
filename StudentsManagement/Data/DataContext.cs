using Microsoft.EntityFrameworkCore;
using StudentsManagement.Entities;

namespace StudentsManagement.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Student> Students { get; set; }
        public DbSet<Institution> Institutions { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<CourseSubject> CourseSubjects { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Student>(entity =>
            {
                entity.HasKey(s => s.Id);

                entity.Property(s => s.FirstName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(s => s.Email)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.HasIndex(s => s.Email)
                    .IsUnique()
                    .HasDatabaseName("UX_Students_Email");

                entity.HasOne(s => s.Course)
                    .WithMany(c => c.Students)
                    .HasForeignKey(s => s.CourseId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.ToTable(t =>
                {
                    t.HasCheckConstraint(
                        "CK_Students_Email_FaculdadeEdu",
                        "LOWER(`Email`) LIKE '%@faculdade.edu'"
                    );
                });
            });

            modelBuilder.Entity<Institution>(entity =>
            {
                entity.HasKey(i => i.Id);

                entity.Property(i => i.Name)
                    .IsRequired()
                    .HasMaxLength(150);
            });

            modelBuilder.Entity<Course>(entity =>
            {
                entity.HasKey(c => c.Id);

                entity.Property(c => c.Name)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.HasIndex(c => new { c.InstitutionId, c.Name })
                    .IsUnique();

                entity.HasOne(c => c.Institution)
                    .WithMany(i => i.Courses)
                    .HasForeignKey(c => c.InstitutionId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Subject>(entity =>
            {
                entity.HasKey(s => s.Id);

                entity.Property(s => s.Name)
                    .IsRequired()
                    .HasMaxLength(150);
            });

            modelBuilder.Entity<CourseSubject>(entity =>
            {
                entity.HasKey(cs => new { cs.CourseId, cs.SubjectId });

                entity.HasOne(cs => cs.Course)
                    .WithMany(c => c.CourseSubjects)
                    .HasForeignKey(cs => cs.CourseId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(cs => cs.Subject)
                    .WithMany(s => s.CourseSubjects)
                    .HasForeignKey(cs => cs.SubjectId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
