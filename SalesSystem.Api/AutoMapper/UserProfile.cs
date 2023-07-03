using AutoMapper;
using SalesSystem.Core.Models;
using SalesSystem.Core.ViewModels;

namespace SalesSystem.Api.AutoMapper
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<RegisterRequestModel, User>().ReverseMap();
            CreateMap<UpdateApplicationUserRequestModel, User>().ReverseMap();
        }
    }
}
