using MicrosoftApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicrosoftApp.Repositories
{
    public interface ITaskRepository
    {
        Task AddAsync(TaskList entity);
        Task Delete(TaskList entity);
        
        Task<List<TaskList>> GetAllByUserIdAsync(int id);
        Task UpdateAsync();
        Task DeleteByIdAsync(int task_id);
        
    }
}
