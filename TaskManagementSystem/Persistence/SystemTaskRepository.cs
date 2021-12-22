using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TaskManagementSystem.Core;
using TaskManagementSystem.Core.Models;
using TaskManagementSystem.DTOs;
using TaskManagementSystem.Extensions;

namespace TaskManagementSystem.Persistence
{
    public class SystemTaskRepository : ISystemTaskRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public SystemTaskRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<SystemTask>  GetSystemTask(Guid id, bool includeRelated)
        {
            if (!includeRelated)
                return await _context.SystemTasks.FindAsync(id);

            return await _context.SystemTasks.FindAsync(id);
        }

        public void Add(SystemTask systemTask)
        {
            _context.SystemTasks.Add(systemTask);
        }

        public void Remove(SystemTask systemTask)
        {
            _context.Remove(systemTask);
        }

        public async Task<QueryResult<TaskDto>> GetSystemTasks(SystemTaskQuery queryObj)
        {
            var result = new QueryResult<SystemTask>();
            var query = _context.SystemTasks.Include(s => s.Owner)
                .AsQueryable();
            
            //Filtering
            if (String.IsNullOrEmpty(queryObj.UserName) == false)
                query = query.Where(s => s.Owner.UserName == queryObj.UserName);
            if (queryObj.UrgentLevel.HasValue)
                query = query.Where(s => (int)s.UrgentLevel == queryObj.UrgentLevel.Value);
            if (queryObj.Status.HasValue)
                query = query.Where(s => (int)s.Status == queryObj.Status.Value);

            var columnsMap = new Dictionary<string, Expression<Func<SystemTask, object>>>()
            {
                ["urgentLevel"] = st => st.UrgentLevel,
                ["userName"] = st => st.Owner.UserName,
                ["status"] = st => st.Status,
                ["title"] = st => st.Title,
                ["creationDate"] = st => st.CreationDateTime,
                ["deadline"] = st => st.Deadline,
            };

            query = query.ApplyOrdering(queryObj, columnsMap);

            result.TotalItems = await query.CountAsync();

            query = query.ApplyPaging(queryObj);

            result.Items = await query.ToListAsync();

            
            return _mapper.Map<QueryResult<SystemTask>, QueryResult<TaskDto>>(result);
        }

        public void UpdateTask(Guid id, SystemTask task)
        {
            _context.Entry(task).State = EntityState.Modified;
        }


    }
        
}