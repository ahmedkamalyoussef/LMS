using LMS.Application.Interfaces;
using LMS.Data.IGenericRepository_IUOW;

namespace LMS.Application.Services
{
    public class StudentService(IUnitOfWork unitOfWork) : IStudentService
    {

        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<int> GetStudentsCount()
        {
            return await _unitOfWork.Students.CountAsync();
        }
    }
}
