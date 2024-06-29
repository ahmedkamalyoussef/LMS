using LMS.Data.Entities;
using LMS.Domain.Entities;

namespace LMS.Data.IGenericRepository_IUOW
{
    public interface IUnitOfWork
    {
        IGenericRepository<Student> Students { get; set; }
        IGenericRepository<Teacher> Teachers { get; set; }
        IGenericRepository<Admin> Admins { get; set; }
        IGenericRepository<Course> Courses { get; set; }
        IGenericRepository<Exam> Exams { get; set; }
        IGenericRepository<Book> Books { get; set; }
        IGenericRepository<Lecture> Lectures { get; set; }
        IGenericRepository<Question> Questions { get; set; }
        IGenericRepository<Answer> Answers { get; set; }
        IGenericRepository<StudentCourse> StudentCourses { get; set; }
        IGenericRepository<ExamResult> ExamResults { get; set; }
        IGenericRepository<Evaluation> Evaluations { get; set; }


        Task CreateTransactionAsync();
        Task CommitAsync();
        Task CreateSavePointAsync(string point);
        Task RollbackAsync();
        Task RollbackToSavePointAsync(string point);
        Task<int> SaveAsync();
    }
}
