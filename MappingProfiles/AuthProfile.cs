using AutoMapper;
using ToDoAPI_ASPNET.Models.Entities;
using ToDoAPI_ASPNET.Models.Dtos.Auth;

namespace ToDoAPI_ASPNET.MappingProfiles;

public class AuthProfile : Profile
{
    public AuthProfile()
    {
        CreateMap<AuthRegisterDto, User>()
            .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password));
    }
}
