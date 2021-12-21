using System.Linq;
using AutoMapper;
using TaskManagementSystem.Core.Models;
using TaskManagementSystem.DTOs;
using TaskManagementSystem.Services;

namespace TaskManagementSystem.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<SystemTask, TaskDto>();
            CreateMap<AppUser, UserTaskDto>()
                .ForMember(u => u.SystemTasks, opt => opt.MapFrom(a => a.SystemTasks));

            CreateMap<TaskDto, SystemTask>();
            CreateMap<UserTaskDto, AppUser>()
                .ForMember(a => a.SystemTasks, opt => opt.MapFrom(u => u.SystemTasks));

        }
    }
}
