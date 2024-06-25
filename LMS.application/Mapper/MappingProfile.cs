using AutoMapper;
using LMS.Application.Authentication;
using LMS.Data.Entities;
using System.Net.Mail;

namespace LMS.Application.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<IRegisterUser, ApplicationUser>()
                   .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => new MailAddress(src.Email).User));
        }
        
    }
}
