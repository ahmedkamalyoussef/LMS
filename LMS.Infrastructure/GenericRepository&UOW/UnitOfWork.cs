using LMS.Data.Entities;
using LMS.Data.IGenericRepository_IUOW;
using LMS.Domain.Entities;
using LMS.Infrastructure.Data;
using Microsoft.EntityFrameworkCore.Storage;

namespace LMS.Infrastructure.GenericRepository_UOW
{
    public class UnitOfWork : IUnitOfWork
    {
        private IDbContextTransaction transaction;

        private readonly ApplicationDbContext _context;
        
        public IGenericRepository<Student> Students { get; set; }
        public IGenericRepository<Teacher> Teachers { get; set; }
        public IGenericRepository<Admin> Admins { get; set; }
        public IGenericRepository<Course> Courses { get; set; }
        public IGenericRepository<Exam> Exams { get; set; }
        public IGenericRepository<Book> Books { get; set; }
        public IGenericRepository<Lecture> Lectures { get; set; }
        public IGenericRepository<Question> Questions { get; set; }
        public IGenericRepository<Answer> Answers { get; set; }
        public IGenericRepository<StudentCourse> StudentCourses { get; set; }
        public IGenericRepository<ExamResult> ExamResults { get; set; }
        public IGenericRepository<Evaluation> Evaluations { get; set; }


        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Students = new GenericRepository<Student>(_context);
            Teachers = new GenericRepository<Teacher>(_context);
            Admins = new GenericRepository<Admin>(_context);
            Courses= new GenericRepository<Course>(_context);
            Books = new GenericRepository<Book>(_context);
            Exams = new GenericRepository<Exam>(_context);
            Answers = new GenericRepository<Answer>(_context);
            Lectures = new GenericRepository<Lecture>(_context);
            Questions = new GenericRepository<Question>(_context);
            ExamResults = new GenericRepository<ExamResult>(_context);
            StudentCourses=new GenericRepository<StudentCourse>(_context);
            Evaluations = new GenericRepository<Evaluation>(_context);
        }

        public async Task CreateTransactionAsync()
        {
            transaction = await _context.Database.BeginTransactionAsync();
        }

        public async Task CommitAsync()
        {
            await transaction.CommitAsync();
        }

        public async Task CreateSavePointAsync(string point)
        {
            await transaction.CreateSavepointAsync(point);
        }

        public async Task RollbackAsync()
        {
            await transaction.RollbackAsync();
        }

        public async Task RollbackToSavePointAsync(string point)
        {
            await transaction.RollbackToSavepointAsync(point);
        }


        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
