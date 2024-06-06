using MicrosoftApp.Data;
using MicrosoftApp.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicrosoftApp.Services
{
    public class TaskService : ITaskService
    {

        private ITaskRepository task_repository;
         public TaskService(ITaskRepository taskRepository)
         {
            this.task_repository = taskRepository;
         }
        public async Task AddTaskListAsync(TaskList taskList)
        {
            await this.task_repository.AddAsync(taskList);
        }

        public async Task<List<TaskList>> GetAllTaskListsAsync()
        {
            return await this.task_repository.GetAllAsync();
        }

        public async Task<List<TaskList>> GetTaskListByIdAsync(int id)
        {
            var taskList = await this.task_repository.GetByIdAsync(id);
            return taskList;
        }

        public async Task RemoveTaskList(TaskList taskList)
        {
            await this.task_repository.Delete(taskList);
        }
    }
}
