using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicrosoftApp.Data
{
    [PrimaryKey("Id")]
    public class UserTask
    {
        public int Id { get; set; }
        public string ParentList { get; set; }
        public string Text { get; set; }
        public bool IsCompleted { get; set; }
        public bool IsImportant { get; set; }
        public string DueDate { get; set; }
        public UserTask(string parentList, string text, bool isCompleted, bool isImportant, string dueDate)
        {
            this.ParentList = parentList;
            this.Text = text;
            this.IsCompleted = isCompleted;
            this.IsImportant = isImportant;
            this.DueDate = dueDate;
        }
    }
}
