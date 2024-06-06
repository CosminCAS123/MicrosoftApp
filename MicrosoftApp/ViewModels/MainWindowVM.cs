using Avalonia.Controls;
using MicrosoftApp.Data;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicrosoftApp.ViewModels
{
    public class MainWindowVM : ViewModelBase
    {

        public static User? CurrentUser { get; set; } 
        public ViewModelBase FirstColumnVM { get => this.firstcolvm; set => this.RaiseAndSetIfChanged(ref this.firstcolvm , value); }
        private ViewModelBase firstcolvm;
        public MainWindowVM(User user)
        {
            CurrentUser = user;   
        }
    }
}
