using LMS.Data.Entities;
using LMS.Data.IGenericRepository_IUOW;
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

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Students = new GenericRepository<Student>(_context);
            Teachers = new GenericRepository<Teacher>(_context);
            Admins = new GenericRepository<Admin>(_context);
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
