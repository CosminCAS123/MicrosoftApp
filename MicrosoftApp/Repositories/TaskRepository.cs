using Microsoft.EntityFrameworkCore;
using MicrosoftApp.Data;
using MicrosoftApp.Migrations;
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
        public async Task<TaskList> GetByIdAsync(int id)
        {
            var to_return = await this._dbContext.TaskLists.FirstOrDefaultAsync(x => x.TaskListID == id);
            return to_return;
        }
        public async Task DeleteByIdAsync(int task_id)
        {
            //find by id, then remove
            var task_list = await GetByIdAsync(task_id);
            await Delete(task_list);

            
                
            
        }
        public async Task<List<TaskList>> GetAllByUserIdAsync(int id)
        {
            return await this._dbContext.TaskLists.Where(x => x.UserID == id).ToListAsync();
        }

        public async Task Delete(TaskList entity)
        {
            this._dbContext.TaskLists.Remove(entity);
            await UpdateAsync();
        }


       
     

        public async Task UpdateAsync()
        {
            await this._dbContext.SaveChangesAsync();
        }
    }
}
