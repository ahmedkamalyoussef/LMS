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

            #region Teacher & student
            CreateMap<Teacher, TeacherResultDTO>();
            CreateMap<Student, StudenResultDTO>();
            #endregion

            #region Course
            CreateMap<CourseDTO, Course>();
            CreateMap<Course, CourseResultDTO>();
            #endregion

            #region Book
            CreateMap<BookDTO, Book>();
            CreateMap<EditBookDTO, Book>();
            CreateMap<Book, LectureDtoResultDTO>();
            #endregion

            #region Lecture
            CreateMap<LectureDTO, Lecture>();
            CreateMap<EditLectureDTO, Lecture>();
            CreateMap<Lecture, LectureResultDTO>();
            #endregion
        }

    }
}
