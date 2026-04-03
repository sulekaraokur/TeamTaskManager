using AutoMapper;
using TeamTaskManager.API.DTOs;
using TeamTaskManager.API.Entities;

namespace TeamTaskManager.API.Mappings;

public class MappingProfile : Profile
{
    
    public MappingProfile()
    {
        //"CreateTaskRequest" isimli DTO, "TaskItem" isimli Entity e dönüştü
        CreateMap<CreateTaskRequest, TaskItem>();

        //ilerde farklı modeller olursa hepsini alt alta buraya ekleyebiliriz.
        //CreateMap<AddCommentRequest, Comment>();
    }
}