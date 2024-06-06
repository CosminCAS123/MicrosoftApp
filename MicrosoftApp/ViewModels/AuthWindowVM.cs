using Avalonia.Controls;
using Avalonia.Media;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MicrosoftApp.ViewModels
{
    public class AuthWindowVM : ViewModelBase
    {
      public ViewModelBase AuthContent
        {
            get => this.authContent;
            set => this.RaiseAndSetIfChanged(ref this.authContent, value);
        }
        public Window Window { get; set; }

        private ViewModelBase authContent;

        public AuthWindowVM()
        {
            this.authContent = new LoginVM(this);
        }
     
    }
}
