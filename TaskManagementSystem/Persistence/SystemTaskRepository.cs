using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TaskManagementSystem.Core;
using TaskManagementSystem.Core.Models;
using TaskManagementSystem.Extensions;

namespace TaskManagementSystem.Persistence
{
    public class SystemTaskRepository : ISystemTaskRepository
    {
        private readonly DataContext _context;
        public SystemTaskRepository(DataContext context)
        {
            this._context = context;
        }

        public async Task<SystemTask>  GetSystemTask(Guid id, bool includeRelated)
        {
            if (!includeRelated)
                return await _context.SystemTasks.FindAsync(id);

            return await _context.SystemTasks.FindAsync(id);
            //return await _context.SystemTasks
            //    .Include(v => v.)
            //    .ThenInclude(vf => vf.Feature)
            //    .Include(v => v.Model)
            //    .ThenInclude(m => m.Make)
            //    .SingleOrDefaultAsync(v => v.Id == id);
        }

        public void Add(SystemTask systemTask)
        {
            _context.SystemTasks.Add(systemTask);
        }

        public void Remove(SystemTask systemTask)
        {
            _context.Remove(systemTask);
        }

        public async Task<QueryResult<SystemTask>> GetSystemTasks(SystemTaskQuery queryObj)
        {
            var result = new QueryResult<SystemTask>();
            var query = _context.SystemTasks
                .AsQueryable();

            //Filtering
            //if (queryObj.MakeId.HasValue)
            //    query = query.Where(v => v.Model.MakeId == queryObj.MakeId.Value);

            //if (queryObj.ModelId.HasValue)
            //    query = query.Where(v => v.ModelId == queryObj.ModelId.Value);

            var columnsMap = new Dictionary<string, Expression<Func<SystemTask, object>>>()
            {
                ["urgentLevel"] = st => st.UrgentLevel,
                ["status"] = st => st.Status,
                ["title"] = st => st.Title,
                ["creationDate"] = st => st.CreationDateTime,
                ["deadline"] = st => st.Deadline,
            };

            query = query.ApplyOrdering(queryObj, columnsMap);

            result.TotalItems = await query.CountAsync();

            query = query.ApplyPaging(queryObj);

            result.Items = await query.ToListAsync();

            return result;
        }
        public async void UpdateTask(Guid id, SystemTask task)
        {
            _context.Entry(task).State = EntityState.Modified;
        }


    }
        
}