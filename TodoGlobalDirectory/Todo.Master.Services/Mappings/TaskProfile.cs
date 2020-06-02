using AutoMapper;
using Todo.Master.Data.Entities;
using Todo.Master.Services.Dtos;

namespace Todo.Master.Services.Mappings
{
    public class TaskProfile: Profile
    {
        public TaskProfile() 
        {
            CreateMap<Task, TaskListAndDetailViewModel>().ReverseMap();
            CreateMap<Task, TaskCreateViewModel>().ReverseMap();
            CreateMap<Task, TaskUpdateViewModel>().ReverseMap();
        }
    }
}
