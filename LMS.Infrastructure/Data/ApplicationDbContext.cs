using LMS.Data.Consts;
using LMS.Data.Entities;
using LMS.Domain.Configuration;
using LMS.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LMS.Infrastructure.Data
{
    public class ApplicationDbContext(DbContextOptions options) : IdentityDbContext<ApplicationUser>(options)
    {
        public DbSet<Teacher> Teacher { get; set; }
        public DbSet<Student> Student { get; set; }
        public DbSet<Admin> Admin { get; set; }
        public DbSet<Course> Course { get; set; }
        public DbSet<StudentCourse> StudentCourse { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Teacher>().ToTable("Teacher");
            modelBuilder.Entity<Student>().ToTable("Student");
            modelBuilder.Entity<Admin>().ToTable("Admin");

            #region Student
            new StudentEntityTypeConfiguration().Configure(modelBuilder.Entity<Student>());
            #endregion

            #region Teacher
            new TeacherEntityTypeConfiguration().Configure(modelBuilder.Entity<Teacher>());
            #endregion

            #region Course
            new CourseEntityTypeConfiguration().Configure(modelBuilder.Entity<Course>());
            #endregion

            #region StudentCourse
            new StudentCourseEntityTypeConfiguration().Configure(modelBuilder.Entity<StudentCourse>());
            #endregion

            #region Exam
            new ExamEntityTypeConfiguration().Configure(modelBuilder.Entity<Exam>());
            #endregion

            #region Exam Result 
            new ExamResultEntityTypeConfiguration().Configure(modelBuilder.Entity<ExamResult>());
            #endregion

            #region Book
            new BookEntityTypeConfiguration().Configure(modelBuilder.Entity<Book>());
            #endregion

            #region Answer
            new AnswerEntityTypeConfiguration().Configure(modelBuilder.Entity<Answer>());
            #endregion

            #region Evaluation
            new EvaluationEntityTypeConfiguration().Configure(modelBuilder.Entity<Evaluation>());
            #endregion

            #region Lecture
            new LectureEntityTypeConfiguration().Configure(modelBuilder.Entity<Lecture>());
            #endregion

            #region Question
            new QuestionEntityTypeConfiguration().Configure(modelBuilder.Entity<Question>());
            #endregion

            SeedRoles(modelBuilder);

        }

        private static void SeedRoles(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityRole>().HasData
                (
                  new IdentityRole() { Id = "1", Name = ConstRoles.Admin  , NormalizedName = ConstRoles.Admin ,ConcurrencyStamp = ConstRoles.Admin     },
                  new IdentityRole() { Id = "2", Name = ConstRoles.Teacher, NormalizedName = ConstRoles.Teacher, ConcurrencyStamp = ConstRoles.Teacher },
                  new IdentityRole() { Id = "3", Name = ConstRoles.Student, NormalizedName = ConstRoles.Student, ConcurrencyStamp = ConstRoles.Student }
                );

        }
    }
}
