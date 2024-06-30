using AutoMapper;
using LMS.Application.Helpers;
using LMS.Application.Interfaces;
using LMS.Data.IGenericRepository_IUOW;

namespace LMS.Application.Services
{
    public class AnswerService(IUnitOfWork unitOfWork, IMapper mapper, IUserHelpers userHelpers) : IAnswerSevrice
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;
        private readonly IUserHelpers _userHelpers = userHelpers;
    }
}
