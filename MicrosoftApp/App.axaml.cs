using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Microsoft.Extensions.DependencyInjection;
using MicrosoftApp.ViewModels;
using MicrosoftApp.Views;
using System.Threading.Tasks;
using MicrosoftApp.Extensions;
using System;

namespace MicrosoftApp;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    private async Task SetupSplashScreenAndAuthScreen(IClassicDesktopStyleApplicationLifetime desktop)
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
        authVM.Window = authWindow;
        authWindow.Show();


        splashScreen.Close();

    }
    public new static App Current => (App)Application.Current!;
    public IServiceProvider? Services { get; private set; }
    private ServiceProvider ConfigureServices()
    {
        var services = new ServiceCollection();
        services.AddCommonServices();
        return services.BuildServiceProvider();
    }
    public override async void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            this.Services = ConfigureServices();
            await SetupSplashScreenAndAuthScreen(desktop);
            
        }
        base.OnFrameworkInitializationCompleted();
    }
}
