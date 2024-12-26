using AutoMapper;
using ToDoAPI_ASPNET.Models.Entities;
using ToDoAPI_ASPNET.Models.Dtos.ToDoItems;

namespace ToDoAPI_ASPNET.MappingProfiles;

public class ToDoItemProfile : Profile
{
    public ToDoItemProfile()
    {
        CreateMap<ToDoItemCreateDto, ToDoItem>();
        CreateMap<ToDoItemUpdateDto, ToDoItem>();
        CreateMap<ToDoItem, ToDoItemDto>();
    }
}
