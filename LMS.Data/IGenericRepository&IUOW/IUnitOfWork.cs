using LMS.Data.Entities;

namespace LMS.Data.IGenericRepository_IUOW
{
    public interface IUnitOfWork
    {
        IGenericRepository<Student> Students { get; set; }
        IGenericRepository<Teacher> Teachers { get; set; }
        IGenericRepository<Admin> Admins { get; set; }


        Task CreateTransactionAsync();
        Task CommitAsync();
        Task CreateSavePointAsync(string point);
        Task RollbackAsync();
        Task RollbackToSavePointAsync(string point);
        Task<int> SaveAsync();
    }
}
