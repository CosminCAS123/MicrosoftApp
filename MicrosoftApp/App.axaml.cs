using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using MicrosoftApp.ViewModels;
using MicrosoftApp.Views;
using System.Threading.Tasks;

namespace MicrosoftApp;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override async void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            var splashScreenVM = new SplashScreenVM();
            var splashScreen = new SplashScreen
            {
                DataContext = splashScreenVM
            };
            desktop.MainWindow = splashScreen;
            splashScreen.Show();

            await Task.Delay(3000);
            var authVM = new AuthWindowVM();
            var authWindow = new AuthWindow();
            authWindow.DataContext = authVM;
            authWindow.Show();
          
            
            splashScreen.Close();
            
        }
       
        

        base.OnFrameworkInitializationCompleted();
    }
}
