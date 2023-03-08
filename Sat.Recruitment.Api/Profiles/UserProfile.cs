using AutoMapper;
using Sat.Recruitment.Api.Models;

namespace Sat.Recruitment.Api.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserRequestModel, User>()
                .ForMember(dest => dest.Email,
                    opt => opt.MapFrom<EmailResolver>())
                .ForMember(dest => dest.Money,
                    opt => opt.MapFrom<DecimalResolver>());
            CreateMap<string, User>()
                .ForAllMembers(opt => opt.MapFrom<StringUserResolver>());
        }
    }
}