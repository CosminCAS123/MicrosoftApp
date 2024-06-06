using Microsoft.EntityFrameworkCore;
using MicrosoftApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicrosoftApp.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private DatabaseContext _dbContext = null!;
        public TaskRepository(DatabaseContext context) => this._dbContext = context;

        public async Task AddAsync(TaskList entity)
        {
            //add tasklist 
           await this._dbContext.TaskLists.AddAsync(entity);
            await UpdateAsync();


        }

      
        public async Task Delete(TaskList entity)
        {
            this._dbContext.TaskLists.Remove(entity);
            await UpdateAsync();
        }

        public async Task<List<TaskList>> GetAllAsync()
        {
            return await this._dbContext.TaskLists.ToListAsync();
        }

        public async Task<List<TaskList>> GetByIdAsync(int id)
        {
            var task_list = await this._dbContext.TaskLists.Where(x => x.UserID == id).ToListAsync();
            return task_list!;
        }

        public async Task UpdateAsync()
        {
            await this._dbContext.SaveChangesAsync();
        }
    }
}
