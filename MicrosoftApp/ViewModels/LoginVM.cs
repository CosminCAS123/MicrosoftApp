using Avalonia.Media;
using Avalonia.Threading;
using Material.Icons;
using Microsoft.Extensions.DependencyInjection;
using MicrosoftApp.Repositories;
using MicrosoftApp.Services;
using MicrosoftApp.UtilityClasses;
using MicrosoftApp.Views;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MicrosoftApp.ViewModels
{
    public class LoginVM : ViewModelBase
    {

        public ISolidColorBrush ErrorBrush { get => this.errorBrush; set => this.RaiseAndSetIfChanged(ref this.errorBrush, value); }
        public string ErrorText { get => this.errorText; set => this.RaiseAndSetIfChanged(ref this.errorText, value); }
        public ISolidColorBrush RegisterColor
        {
            get => this.registerColor;
            set => this.RaiseAndSetIfChanged(ref this.registerColor, value);
        }
        private ISolidColorBrush errorBrush;
        private string errorText;
        private ISolidColorBrush registerColor = Brushes.White;
        private AuthWindowVM authVM;
        private AuthField usernameField;
        private AuthField passwordField;
        public AuthField UsernameField { get => this.usernameField; set => this.RaiseAndSetIfChanged(ref this.usernameField, value); }
        public AuthField PasswordField { get => this.passwordField; set => this.RaiseAndSetIfChanged(ref this.passwordField, value); }
        private CancellationTokenSource _loadingCancellationTokenSource;
        public ReactiveCommand<Unit, Unit> DontHaveAccPointerEnteredCommand { get; set; }
        public ReactiveCommand<Unit, Unit> DontHaveAccPointerExitedCommand { get; set; }
        public ReactiveCommand<Unit , Unit> DontHaveAccPointerPressedCommand { get; set; }
        public ReactiveCommand<Unit , Unit> LoginCommand { get; set; }
        private IServiceProvider services;
        private IObservable<bool> canLogin;
        public LoginVM(AuthWindowVM authvm)
        {
            
          
            SetupObservables();
            this.services = App.Current.Services!;
            this.authVM = authvm;
        }
     
        private void SetupObservables()
        {
            DontHaveAccPointerEnteredCommand = ReactiveCommand.Create(changeRegisterColorToRed);
            DontHaveAccPointerExitedCommand = ReactiveCommand.Create(changeRegisterColorToWhite);
            DontHaveAccPointerPressedCommand = ReactiveCommand.Create(goToRegister);

            this.canLogin = this.WhenAnyValue(x => x.UsernameField.Text, x => x.PasswordField.Text, (username, password) =>
            {
                return FieldVerification.CanBeUsername(username) && FieldVerification.CanBePassword(password);
            });
            LoginCommand = ReactiveCommand.Create(loginCommand, canLogin);
            this.UsernameField = new AuthField { Type = AuthField.FieldType.UsernameField }; 
            
            this.PasswordField = new AuthField { Type= AuthField.FieldType.PasswordField };

            this.WhenAnyValue(x => x.UsernameField.IconKind).Subscribe((icon) =>
            {
                AuthField.SetTip(UsernameField);
            });

            this.WhenAnyValue(x => x.PasswordField.IconKind).Subscribe((icon) =>
            {
                AuthField.SetTip(PasswordField);
            });
           

            this.WhenAnyValue(x => x.UsernameField.Text).Subscribe((username) =>
            {
                if (FieldVerification.CanBeUsername(username))
                {
                    this.UsernameField.IconKind = MaterialIconKind.CheckCircle;
                    this.UsernameField.IconBrush = Brushes.Green;
                }
                else
                {
                    this.UsernameField.IconKind = MaterialIconKind.Cancel;
                    this.UsernameField.IconBrush = Brushes.Red;
                }
            });
            this.WhenAnyValue(x => x.PasswordField.Text).Subscribe((password) =>
            {
                if (FieldVerification.CanBePassword(password))
                {
                    this.PasswordField.IconKind = MaterialIconKind.CheckCircle;
                    this.PasswordField.IconBrush = Brushes.Green;
                }
                else
                {
                    this.PasswordField.IconKind = MaterialIconKind.Cancel;
                    this.PasswordField.IconBrush = Brushes.Red;
                }
            });
        }
        private async void loginCommand()
        {
            var user_service = this.services.GetService<IUserService>();
            startLoading();
            await Task.Delay(2000);
            //verify if username exists in db 
            var user = await user_service!.GetUserByUsername(this.UsernameField.Text);
           
            if (user is not null)
            {
                //check if password matches
                if (string.Equals(user.Password , this.PasswordField.Text))
                {
                    this.ErrorText = FieldVerification.Errors.Login.GoodTip;
                    this.ErrorBrush = Brushes.Green;

                    //ENTER APP//////////////
                    var mainVM = new MainWindowVM(user);
                    mainVM.FirstColumnVM = new FirstMainColumnVM();
                    var mainWindow = new MainWindow();
                    mainWindow.DataContext = mainVM;
                   
                    await Task.Delay(1000);
                    stopLoading();
                    mainWindow.Show();
                    this.authVM.Window.Close();

                    
                }
                else
                {
                    stopLoading();
                    this.ErrorText = FieldVerification.Errors.Login.PasswordNotMatching;
                    this.ErrorBrush = Brushes.Red;
                   
                }
            }
            else
            {
                //username not found
                stopLoading();
                this.ErrorText = FieldVerification.Errors.Login.UsernameNotFound;
                this.ErrorBrush = Brushes.Red;
               
            }

          

        }
        private void startLoading()
        {
            _loadingCancellationTokenSource = new CancellationTokenSource();
            var token = _loadingCancellationTokenSource.Token;
            Dispatcher.UIThread.Invoke(() => this.ErrorBrush = Brushes.White);
            Task.Run(async () =>
            {

                int dotCount = 0;
                while (!token.IsCancellationRequested)
                {
                    dotCount = (dotCount + 1) % 4;
                    string loadingText = "Loading" + new string('.', dotCount);
                    Dispatcher.UIThread.Invoke(() => this.ErrorText = loadingText);

                    await Task.Delay(333);
                }

            }, token);
        }
        private void stopLoading()
        {
            _loadingCancellationTokenSource.Cancel();
            Dispatcher.UIThread.Invoke(() =>this.ErrorText = string.Empty);
        }
        private void changeRegisterColorToRed() => this.RegisterColor = Brushes.Red;

        private void changeRegisterColorToWhite() => this.RegisterColor = Brushes.White;
        private void goToRegister() => this.authVM.AuthContent = new RegisterVM(this.authVM, services.GetRequiredService<IUserService>() , services.GetRequiredService<ITaskService>());
      
        
    }
}
