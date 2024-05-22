using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;
using MicrosoftApp.UtilityClasses;
using Material.Icons.Avalonia;
using Material.Icons;
using Avalonia.Media;
using System.Xml.Serialization;
using System.Runtime.CompilerServices;
using System.Diagnostics;
namespace MicrosoftApp.ViewModels
{
    public class RegisterVM : ViewModelBase
    {
        #region privateProperties
        private AuthWindowVM authWindowVM;
        private MaterialIconKind emailKind;
        private ISolidColorBrush emailBrush;
        private MaterialIconKind passwordKind;
        private ISolidColorBrush passwordBrush;
        private MaterialIconKind confirmPasswordKind;
        private ISolidColorBrush confirmPasswordBrush;
        private MaterialIconKind usernameKind;
        private ISolidColorBrush usernameBrush;
        private MaterialIconKind fullnameKind;
        private ISolidColorBrush fullnameBrush;
        private string email;
        private string password;
        private string confirmpassword;
        private string username;
        private string fullname;
        #endregion

        #region PublicProperties
        public string Email { get => this.email; set => this.RaiseAndSetIfChanged(ref this.email, value); }
       
        public MaterialIconKind EmailKind {get => this.emailKind; set => this.RaiseAndSetIfChanged(ref this.emailKind, value); }
        public ISolidColorBrush EmailBrush { get => this.emailBrush; set => this.RaiseAndSetIfChanged(ref this.emailBrush, value); }
        public string Password { get => this.password; set => this.RaiseAndSetIfChanged(ref this.password , value); }
        public MaterialIconKind PasswordKind { get => this.passwordKind; set => this.RaiseAndSetIfChanged(ref this.passwordKind, value); }
        public ISolidColorBrush PasswordBrush { get => this.passwordBrush; set => this.RaiseAndSetIfChanged(ref this.passwordBrush, value); }
        public string ConfirmPassword { get => this.confirmpassword; set => this.RaiseAndSetIfChanged(ref this.confirmpassword , value); }
        public MaterialIconKind ConfirmPasswordKind { get => this.confirmPasswordKind; set => this.RaiseAndSetIfChanged(ref this.confirmPasswordKind, value); }
        public ISolidColorBrush ConfirmPasswordBrush { get => this.confirmPasswordBrush; set => this.RaiseAndSetIfChanged(ref this.confirmPasswordBrush, value); }
        public string Username { get => this.username; set => this.RaiseAndSetIfChanged(ref this.username, value); }
        public MaterialIconKind UsernameKind { get => this.usernameKind; set => this.RaiseAndSetIfChanged(ref this.usernameKind, value); }
        public ISolidColorBrush UsernameBrush { get => this.usernameBrush; set => this.RaiseAndSetIfChanged(ref this.usernameBrush, value); }

        public string FullName { get => this.fullname; set => this.RaiseAndSetIfChanged(ref this.fullname , value); }
        public MaterialIconKind FullNameKind { get => this.fullnameKind; set => this.RaiseAndSetIfChanged(ref this.fullnameKind, value); }
        public ISolidColorBrush FullNameBrush { get => this.fullnameBrush; set => this.RaiseAndSetIfChanged(ref this.fullnameBrush, value); }
        public ReactiveCommand<Unit , Unit> BackToLoginCommand { get; set; }
        public ReactiveCommand<Unit , Unit> RegisterCommand { get; set; }
       

        #endregion

        public RegisterVM(AuthWindowVM authvm)
        {
            this.authWindowVM = authvm;
            BackToLoginCommand = ReactiveCommand.Create(onBackToLogin);
            //observe the fields
            SetFieldObservables();
             var canRegister = this.WhenAnyValue(x => x.EmailBrush, x => x.PasswordBrush, x => x.ConfirmPasswordBrush, x => x.UsernameBrush, x => x.FullNameBrush,
             (brush1, brush2, brush3, brush4, brush5) => areBrushesGreen(brush1 , brush2 , brush3 , brush4 , brush5));


            RegisterCommand = ReactiveCommand.Create(registerCommand, canRegister);
            
        }
        private bool areBrushesGreen(ISolidColorBrush brush1 , ISolidColorBrush brush2 , ISolidColorBrush brush3 , ISolidColorBrush brush4 , ISolidColorBrush brush5)
        {
            var green = Brushes.Green.ToString();
            var one = brush1.Color.ToString();
            var two = brush2.Color.ToString();
            var three = brush3.Color.ToString();
            var four = brush4.Color.ToString();
            var five = brush5.Color.ToString();
            return green.Equals(one) && green.Equals(two) && green.Equals(three) && green.Equals(four) && green.Equals(five);
        }
        private void registerCommand()
        {
            ////
            throw new NotImplementedException();
        }
        private void SetFieldObservables()
        {
            //email
            this.WhenAnyValue(x => x.Email).Subscribe(email =>
            {
                if (FieldVerification.CanBeEmailAdress(email))
                {
                    this.EmailKind = MaterialIconKind.CheckCircle;
                    this.EmailBrush = Brushes.Green;
                }
                else
                {
                    this.EmailKind = MaterialIconKind.Cancel;
                    this.EmailBrush = Brushes.Red;
                }
            });

            //password
            this.WhenAnyValue(x => x.Password).Subscribe(password =>
            {

            if (FieldVerification.CanBePassword(password))
            {
                this.PasswordKind = MaterialIconKind.CheckCircle;
                this.PasswordBrush = Brushes.Green;
                if (this.ConfirmPassword == this.Password)
                    {
                        this.ConfirmPasswordBrush = Brushes.Green;
                        this.ConfirmPasswordKind = MaterialIconKind.CheckCircle; 
                    }

            }
            else
            {
                this.PasswordKind = MaterialIconKind.Cancel;
                this.PasswordBrush = Brushes.Red;
            }
            if (this.Password is not null && this.Password != this.ConfirmPassword) { this.ConfirmPasswordBrush = Brushes.Red; this.ConfirmPasswordKind = MaterialIconKind.Cancel; }
            
                
                });

                //confirm password
                this.WhenAnyValue(x => x.ConfirmPassword).Subscribe(password =>
                {
                    if (FieldVerification.AreMatching(this.Password, password))
                    {
                        this.ConfirmPasswordKind = MaterialIconKind.CheckCircle;
                        this.ConfirmPasswordBrush = Brushes.Green;
                    }
                    else
                    {
                        this.ConfirmPasswordKind = MaterialIconKind.Cancel;
                        this.ConfirmPasswordBrush = Brushes.Red;
                    }
                });

                //full name
                this.WhenAnyValue(x => x.FullName).Subscribe(fullName =>
                {
                    if (FieldVerification.CanBeFullName(fullName))
                    {
                        this.FullNameKind = MaterialIconKind.CheckCircle;
                        this.FullNameBrush = Brushes.Green;
                    }
                    else
                    {
                        this.FullNameKind = MaterialIconKind.Cancel;
                        this.FullNameBrush = Brushes.Red;
                    }

                });

                //username
                this.WhenAnyValue(x => x.Username).Subscribe(username =>
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

            }
        

        private void onBackToLogin() => this.authWindowVM.AuthContent = new LoginVM(authWindowVM);
        


    }
}
