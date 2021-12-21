using System;
using System.Threading.Tasks;
using TaskManagementSystem.Core.Models;

namespace TaskManagementSystem.Core
{
    public interface ISystemTaskRepository
    {
        // TODO: put request
        Task<SystemTask> GetSystemTask(Guid id, bool includeRelated);
        void Add(SystemTask systemTask);
        void Remove(SystemTask systemTask);
        void UpdateTask(Guid id, SystemTask task);

        Task<QueryResult<SystemTask>> GetSystemTasks(SystemTaskQuery filter);
    }
}