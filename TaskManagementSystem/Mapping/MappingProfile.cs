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
            CreateMap<AppUser, KeyValuePairDto>()
                .ForMember(kvp => kvp.Name, opt => opt.MapFrom(a => a.UserName));
            CreateMap<SystemTask, TaskDto>()
                .ForMember(t => t.OwnerUserName, opt => opt.MapFrom(s => s.Owner.UserName));
            CreateMap<QueryResult<SystemTask>, QueryResult<TaskDto>>()
                .ForMember(qt => qt.Items, opt => opt.MapFrom(qs => qs.Items));
            CreateMap<AppUser, UserTaskDto>()
                .ForMember(u => u.SystemTasks, opt => opt.MapFrom(a => a.SystemTasks));

            CreateMap<TaskDto, SystemTask>();
            CreateMap<UserTaskDto, AppUser>()
                .ForMember(a => a.SystemTasks, opt => opt.MapFrom(u => u.SystemTasks));

        }
    }
}
