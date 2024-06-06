using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicrosoftApp.Data
{
    [PrimaryKey("TaskListID")]
       
    public class TaskList
    {
        public int TaskListID { get; set; }
        public int UserID { get; set; }
        public string Name { get; set; }

        public List<UserTask> Tasks { get; set; }
        public TaskList()
        {

        }
        public TaskList(int userID, string name, List<UserTask> tasks)
        {
            this.UserID = userID;
            this.Name = name;
            this.Tasks = tasks;
        }



    }
}
