using AutoMapper;
using LMS.Application.Helpers;
using LMS.Application.Interfaces;
using LMS.Data.IGenericRepository_IUOW;

namespace LMS.Application.Services
{
    public class TeacherService(IUnitOfWork unitOfWork, IMapper mapper, IUserHelpers userHelpers : ITeacherService
    {

        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;
        private readonly IUserHelpers _userHelpers = userHelpers;

        public Task<bool> EditTeacherImage(string imagePath)
        {
            throw new NotImplementedException();
        }

        public async Task<int> GetTeachersCount()
        {
            return await _unitOfWork.Teachers.CountAsync();
        }
    }
}
