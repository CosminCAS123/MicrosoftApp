using MicrosoftApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicrosoftApp.Services
{
    public interface ITaskService
    {
        //interface for dealing with the tasks of each user 
        //you need to add tasks, remove tasks, get tasks, 
        Task AddTaskListAsync(TaskList taskList);
        Task RemoveTaskList(TaskList taskList);

        Task RemoveTasklistByIdAsync(int id);

        Task<List<TaskList>> GetAllTaskListsAsync(int user_id);


        
    }
}
