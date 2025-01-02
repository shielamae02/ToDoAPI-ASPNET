using AutoMapper;
using ToDoAPI_ASPNET.Models.Entities;
using ToDoAPI_ASPNET.Models.Dtos.Users;

namespace ToDoAPI_ASPNET.MappingProfiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, UserDetailsDto>();
    }
}
