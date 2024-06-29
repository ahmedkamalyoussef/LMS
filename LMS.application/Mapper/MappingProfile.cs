using AutoMapper;
using LMS.Application.Authentication;
using LMS.Application.DTOs;
using LMS.Data.Entities;
using System.Net.Mail;

namespace LMS.Application.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            #region Register
            CreateMap<RegisterUser, Teacher>()
                   .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => new MailAddress(src.Email).User));
            CreateMap<RegisterUser, Student>()
                   .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => new MailAddress(src.Email).User));
            #endregion

            #region Course
            CreateMap<CourseDTO, Course>();
            CreateMap<Course, CourseResultDTO>();
            #endregion

            #region Teacher & student
            CreateMap<Teacher, TeacherResultDTO>();
            CreateMap<Student, StudenResultDTO>();
            #endregion
        }

    }
}
