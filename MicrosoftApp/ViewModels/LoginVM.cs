using Avalonia.Media;
using Material.Icons;
using MicrosoftApp.UtilityClasses;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MicrosoftApp.ViewModels
{
    public class LoginVM : ViewModelBase
    {


        public ISolidColorBrush RegisterColor
        {
            get => this.registerColor;
            set => this.RaiseAndSetIfChanged(ref this.registerColor, value);
        }
        private ISolidColorBrush registerColor = Brushes.White;
        private AuthWindowVM authVM;
        private string username;
        private string password;
        private ISolidColorBrush usernameBrush;
        private ISolidColorBrush passwordBrush;
        private MaterialIconKind usernameKind;
        private MaterialIconKind passwordKind;
        public string Username { get => this.username; set => this.RaiseAndSetIfChanged(ref this.username, value); }
        public string Password { get => this.password; set => this.RaiseAndSetIfChanged(ref this.password, value); }

        public ISolidColorBrush UsernameBrush { get => this.usernameBrush; set => this.RaiseAndSetIfChanged(ref this.usernameBrush, value); }
        public ISolidColorBrush PasswordBrush { get => this.passwordBrush;set => this.RaiseAndSetIfChanged(ref this.passwordBrush , value); }
        public MaterialIconKind UsernameKind { get => this.usernameKind; set => this.RaiseAndSetIfChanged(ref this.usernameKind, value); }
        public MaterialIconKind PasswordKind { get => this.passwordKind; set => this.RaiseAndSetIfChanged(ref this.passwordKind, value); }
        public ReactiveCommand<Unit, Unit> DontHaveAccPointerEnteredCommand { get; set; }
        public ReactiveCommand<Unit, Unit> DontHaveAccPointerExitedCommand { get; set; }
        public ReactiveCommand<Unit , Unit> DontHaveAccPointerPressedCommand { get; set; }
        public ReactiveCommand<Unit , Unit> LoginCommand { get; set; }

        public LoginVM(AuthWindowVM authvm)
        {
            DontHaveAccPointerEnteredCommand = ReactiveCommand.Create(changeRegisterColorToRed);
            DontHaveAccPointerExitedCommand = ReactiveCommand.Create(changeRegisterColorToWhite);
            DontHaveAccPointerPressedCommand = ReactiveCommand.Create(goToRegister);
            LoginCommand = ReactiveCommand.Create(loginCommand);
            SetupObservables();
            this.authVM = authvm;
        }
        private void SetupObservables()
        {
            this.WhenAnyValue(x => x.Username).Subscribe((username) =>
            {
                if (FieldVerification.CanBeUsername(username))
                {
                    this.UsernameKind = MaterialIconKind.CheckCircle;
                    this.UsernameBrush = Brushes.Green;
                }
                else
                {
                    this.UsernameKind = MaterialIconKind.Cancel;
                    this.UsernameBrush = Brushes.Red;
                }
            });
            this.WhenAnyValue(x => x.Password).Subscribe((password) =>
            {
                if (FieldVerification.CanBePassword(password))
                {
                    this.PasswordKind = MaterialIconKind.CheckCircle;
                    this.PasswordBrush = Brushes.Green;
                }
                else
                {
                    this.PasswordKind = MaterialIconKind.Cancel;
                    this.PasswordBrush = Brushes.Red;
                }
            });
        }
        private void loginCommand()
        {
            throw new NotImplementedException();
        }
        private void changeRegisterColorToRed() => this.RegisterColor = Brushes.Red;

        private void changeRegisterColorToWhite() => this.RegisterColor = Brushes.White;
        private void goToRegister() => this.authVM.AuthContent = new RegisterVM(authVM);
        
    }
}
