using Avalonia.Threading;
using Material.Icons;
using Material.Icons.Avalonia;
using Microsoft.Extensions.DependencyInjection;
using MicrosoftApp.Data;
using MicrosoftApp.Services;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicrosoftApp.ViewModels
{
    public class FirstMainColumnVM : ViewModelBase
    {
        public class TaskMenuItem : ReactiveObject
        {
            private MaterialIconKind icon;
            private string text = null!;
             public int TaskListID { get; set; }
          
            public MaterialIconKind Icon { get => this.icon; set => this.RaiseAndSetIfChanged(ref this.icon, value); } 
            public string Title { get => this.text; set => this.RaiseAndSetIfChanged(ref this.text, value); }
           
            public TaskMenuItem(MaterialIconKind icon , string text)
            {
                this.Icon = icon;
                this.Title = text;
            }
        }

        private TaskMenuItem selected_item;
        public TaskMenuItem SelectedItem { get => this.selected_item; set => this.RaiseAndSetIfChanged(ref this.selected_item, value); }
        private ITaskService task_service = null!;
        private string initials;
        public string Initials { get => this.initials; set => this.RaiseAndSetIfChanged(ref this.initials, value); }
        public User User { get; }

        private ObservableCollection<TaskMenuItem> tasks;
        public ObservableCollection<TaskMenuItem> TaskMenuItems { get => this.tasks ; set => this.RaiseAndSetIfChanged(ref this.tasks, value); }
        public ReactiveCommand<Unit, Unit> NewTaskListCommand { get; set; }
        public ReactiveCommand<Unit , Unit> RemoveTaskListCommand { get; set; }
        public  FirstMainColumnVM()
        {
            this.task_service = App.Current!.Services!.GetRequiredService<ITaskService>();
            this.User = MainWindowVM.CurrentUser;
            addMenuItems();
            this.NewTaskListCommand = ReactiveCommand.CreateFromTask(addnewtasklist);
            this.RemoveTaskListCommand = ReactiveCommand.CreateFromTask(removeTaskListCommand);
            this.Initials = getFirstTwoInitials();

        }

        private async Task removeTaskListCommand()
        {
            //we will remove from the ui and from the db as well
            //first from the ui, since  we dont want any freezing.
            int task_id = this.SelectedItem.TaskListID;
            this.TaskMenuItems.Remove(this.SelectedItem);
            await this.task_service.RemoveTasklistByIdAsync(task_id);


        }

        //Get first 2 initials from real name
        private string getFirstTwoInitials()
        {
            string full_name = this.User.FullName;
            string[] words = full_name.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            string initials = char.ToUpper(words[0][0]).ToString() + char.ToUpper(words[1][0]).ToString();
            return initials;

        }
        private async Task addnewtasklist()
        {
            
            var task_list = new TaskList(this.User.ID, "Default", new List<UserTask>());
            this.TaskMenuItems.Add(new TaskMenuItem(MaterialIconKind.ListBoxOutline, task_list.Name));
            await this.task_service.AddTaskListAsync(task_list);
          
           
          


        }

       
        private async Task addMenuItems()
        {


            //loop through all list of ListTasks and add them to the taskmenuitems
            this.TaskMenuItems = new ObservableCollection<TaskMenuItem>();
            var all_tasklists = await this.task_service.GetAllTaskListsAsync(User.ID);
            foreach(var tasklist in all_tasklists)
            {
                MaterialIconKind icon;
                switch (tasklist.Name)
                {
                    case "My Day":
                        icon = MaterialIconKind.WeatherSunny;
                            break;
                    case "Important":
                        icon = MaterialIconKind.StarOutline;
                        break;
                    case "Tasks":
                        icon = MaterialIconKind.HomeOutline;
                        break;
                    default:
                        icon = MaterialIconKind.ListBoxOutline;
                        break;
                }
                var taskmenuitem = new TaskMenuItem(icon, tasklist.Name);
                taskmenuitem.TaskListID = tasklist.TaskListID;
                this.TaskMenuItems.Add(taskmenuitem);
            }


        }


    }
}
