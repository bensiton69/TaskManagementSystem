using System;
using System.Threading.Tasks;
using TaskManagementSystem.Core.Models;
using TaskManagementSystem.DTOs;

namespace TaskManagementSystem.Core
{
    public interface ISystemTaskRepository
    {
        Task<SystemTask> GetSystemTask(Guid id, bool includeRelated);
        void Add(SystemTask systemTask);
        void Remove(SystemTask systemTask);
        void UpdateTask(Guid id, SystemTask task);

        Task<QueryResult<TaskDto>> GetSystemTasks(SystemTaskQuery filter);
    }
}