using AutoMapper;
using LMS.Application.Authentication;
using LMS.Application.DTOs;
using LMS.Data.Entities;
using LMS.Domain.Entities;
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
            CreateMap<EditUserDTO, ApplicationUser>();
            #endregion

            #region Course
            CreateMap<CourseDTO, Course>();
            CreateMap<EditCourseDTO, Course>();
            CreateMap<Course, CourseResultDTO>();
            //.ForMember(dest => dest.Evaluation, opt => opt.MapFrom(src => CalculateAverageRate(src)));
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

            #region Exam
            CreateMap<ExamDTO, Exam>();
            CreateMap<EditExamDTO, Exam>();
            CreateMap<Exam, ExamResultDTO>();
            #endregion

            #region question
            CreateMap<QuestionDTO, Question>();
            CreateMap<EditQuestionDTO, Question>();
            CreateMap<Question, QuestionResultDTO>();
            #endregion

            #region answer
            CreateMap<AnswerDTO, Answer>();
            CreateMap<EditAnswerDTO, Answer>();
            CreateMap<Answer, AnswerResultDTO>();
            #endregion

            #region Evaluation
            CreateMap<EvaluationDTO, Evaluation>();
            #endregion
        }


    }
}
