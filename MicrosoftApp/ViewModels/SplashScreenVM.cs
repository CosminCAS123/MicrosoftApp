using Avalonia.Controls.Templates;
using Avalonia.Media;
using Avalonia.Threading;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicrosoftApp.ViewModels
{
    public class SplashScreenVM : ViewModelBase
    {

        /* private IBrush progressBrush;
         public IBrush ProgressBrush
         {
             get => this.progressBrush;
             set => this.RaiseAndSetIfChanged(ref this.progressBrush, value);
         }*/
        private string startupMessage = string.Empty;
        public string StartupMessage
        {
            get => this.startupMessage;
            set => this.RaiseAndSetIfChanged(ref this.startupMessage, value);
        }
        public int ProgressValue
        {
            get => this.progressValue;
            set => this.RaiseAndSetIfChanged(ref this.progressValue, value);
        }
        private int progressValue = 0;
        private async Task LoadProgressBar()
        {
            //  3000/100 = 30 milliseconds.
            for (ProgressValue = 0; ProgressValue < 100; ProgressValue++)
               await Task.Delay(30);
            
        }

        public SplashScreenVM()
        {
            this.StartupMessage = "Microsoft To Do App";
            LoadProgressBar();            
        }


    }
}
