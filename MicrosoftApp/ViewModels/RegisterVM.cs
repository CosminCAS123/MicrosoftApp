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
using System.Runtime.Serialization.Formatters;
using Material.Styles.Assists;
using MicrosoftApp.Services;
using MicrosoftApp.Data;
using System.Threading;
using Avalonia.Threading;
namespace MicrosoftApp.ViewModels
{
    public class RegisterVM : ViewModelBase
    {
        #region privateProperties
        private AuthWindowVM authWindowVM;
        private AuthField emailField;
        private AuthField passwordField;
        private AuthField confirmPasswordField;
        private AuthField usernameField;
        private AuthField fullnameField;
        private IUserService user_service;
        private ITaskService task_service;
        private ISolidColorBrush errorColor;
        private string errorText = "";
        private CancellationTokenSource _loadingCancellationTokenSource;
        #endregion

        #region PublicProperties
        public ISolidColorBrush ErrorColor
        {
            get => this.errorColor;
            set => this.RaiseAndSetIfChanged(ref this.errorColor, value);
        }
        public string ErrorText
        {
            get => this.errorText;
            set => this.RaiseAndSetIfChanged(ref this.errorText, value);
        }

        public AuthField EmailField { get => this.emailField; set => this.RaiseAndSetIfChanged(ref emailField, value); }
        public AuthField PasswordField { get => this.passwordField; set => this.RaiseAndSetIfChanged(ref passwordField, value); }
        public AuthField ConfirmPasswordField { get => this.confirmPasswordField; set => this.RaiseAndSetIfChanged(ref confirmPasswordField, value); }
        public AuthField UsernameField { get => this.usernameField; set => this.RaiseAndSetIfChanged(ref usernameField, value); }
        public AuthField FullNameField { get => this.fullnameField; set => this.RaiseAndSetIfChanged(ref fullnameField, value); }
        
        public ReactiveCommand<Unit , Unit> BackToLoginCommand { get; set; }
        public ReactiveCommand<Unit , Unit> RegisterCommand { get; set; }
       

        #endregion

        public RegisterVM(AuthWindowVM authvm, IUserService userService , ITaskService taskService)
        {
            this.user_service = userService;
            this.task_service = taskService;
            this.authWindowVM = authvm;
            BackToLoginCommand = ReactiveCommand.Create(onBackToLogin);
            
            //observe the fields
            SetFieldObservables();
             var canRegister = this.WhenAnyValue(x => x.EmailField.IconBrush, x => x.PasswordField.IconBrush , x => x.ConfirmPasswordField.IconBrush, x => x.UsernameField.IconBrush, x => x.FullNameField.IconBrush,
             (brush1, brush2, brush3, brush4, brush5) => areBrushesGreen(brush1 , brush2 , brush3 , brush4 , brush5));


            RegisterCommand = ReactiveCommand.CreateFromTask(registerCommand, canRegister);
            
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
        private void startLoading()
        {
            _loadingCancellationTokenSource = new CancellationTokenSource();
            var token = _loadingCancellationTokenSource.Token;
            Dispatcher.UIThread.Invoke(() => this.ErrorColor = Brushes.White);
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
            this.ErrorText = string.Empty;
        }
        private async Task registerCommand()
        {
            
            startLoading();
            await Task.Delay(2000);
            bool exists_username = await user_service.ExistsByUsernameAsync(this.UsernameField.Text);
            bool exists_email = await user_service.ExistsByEmailAdressAsync(this.EmailField.Text);
            stopLoading();
            if (exists_username)//account with username exists
            {
                this.ErrorText = FieldVerification.Errors.Register.UsernameAlreadyExists;
                this.ErrorColor = Brushes.Red;
                return;
            }
            if (exists_email)//acount with email adress exists
            {
                this.errorText = FieldVerification.Errors.Register.EmailAlreadyExists;
                this.ErrorColor = Brushes.Red;
                return;
            }
           
            //account can be created
            var user = new   User(this.EmailField.Text, this.PasswordField.Text, this.UsernameField.Text, this.FullNameField.Text);
            await user_service.AddUserAsync(user);
            //add default tasks
            var important_tasks = new TaskList { Name = "Important", UserID = user.ID, Tasks = new List<UserTask>() };
            var all_tasks = new TaskList { Name = "Tasks", UserID = user.ID, Tasks = new List<UserTask>() };
            var today_tasks = new TaskList { Name = "My Day", UserID = user.ID, Tasks = new List<UserTask>() };
            await task_service.AddTaskListAsync(important_tasks);
            await task_service.AddTaskListAsync(all_tasks);
            await task_service.AddTaskListAsync(today_tasks);

            this.ErrorText = FieldVerification.Errors.Register.RegisteredSuccessfully;
            this.ErrorColor = Brushes.Green;
            
        }
        private void SetFieldObservables()
        {
            this.EmailField = new AuthField { Type = AuthField.FieldType.EmailField }; this.PasswordField = new AuthField { Type = AuthField.FieldType.PasswordField  }; 
            this.ConfirmPasswordField = new AuthField { Type = AuthField.FieldType.ConfirmPasswordField}; this.UsernameField = new AuthField { Type = AuthField.FieldType.UsernameField}; 
            this.FullNameField = new AuthField { Type = AuthField.FieldType.FullNameField };

            this.WhenAnyValue(x => x.UsernameField.IconKind).Subscribe((icon) => { AuthField.SetTip(this.UsernameField); });
            this.WhenAnyValue(x => x.PasswordField.IconKind).Subscribe((icon) => { AuthField.SetTip(this.PasswordField); });
            this.WhenAnyValue(x => x.ConfirmPasswordField.IconKind).Subscribe((icon) => { AuthField.SetTip(this.ConfirmPasswordField); });
            this.WhenAnyValue(x => x.EmailField.IconKind).Subscribe((icon) => { AuthField.SetTip(this.EmailField); });
            this.WhenAnyValue(x => x.FullNameField.IconKind).Subscribe((icon) => { AuthField.SetTip(this.FullNameField); });
            //email text
            this.WhenAnyValue(x => x.EmailField.Text).Subscribe(email =>
            {
                if (FieldVerification.CanBeEmailAdress(email))
                {
                    this.EmailField.IconKind = MaterialIconKind.CheckCircle;
                    this.EmailField.IconBrush = Brushes.Green;
                }
                else
                {
                    this.EmailField.IconKind = MaterialIconKind.Cancel;
                    this.EmailField.IconBrush = Brushes.Red;
                }
            });

            //password text
            this.WhenAnyValue(x => x.PasswordField.Text).Subscribe(password =>
            {

            if (FieldVerification.CanBePassword(password))
            {
                this.PasswordField.IconKind = MaterialIconKind.CheckCircle;
                this.PasswordField.IconBrush = Brushes.Green;
                if (this.ConfirmPasswordField.Text == this.PasswordField.Text)
                    {
                        this.ConfirmPasswordField.IconBrush = Brushes.Green;
                        this.ConfirmPasswordField.IconKind = MaterialIconKind.CheckCircle; 
                    }

            }
            else
            {
                this.PasswordField.IconKind = MaterialIconKind.Cancel;
                this.PasswordField.IconBrush = Brushes.Red;
            }
            if (this.PasswordField.Text is not null && this.PasswordField.Text != this.ConfirmPasswordField.Text) { this.ConfirmPasswordField.IconBrush = Brushes.Red; this.ConfirmPasswordField.IconKind = MaterialIconKind.Cancel; }
            
                
                }); 

                //confirm password text
                this.WhenAnyValue(x => x.ConfirmPasswordField.Text).Subscribe(password =>
                {
                    if (FieldVerification.AreMatching(this.PasswordField.Text, password))
                    {
                        this.ConfirmPasswordField.IconKind = MaterialIconKind.CheckCircle;
                        this.ConfirmPasswordField.IconBrush = Brushes.Green;
                    }
                    else
                    {
                        this.ConfirmPasswordField.IconKind = MaterialIconKind.Cancel;
                        this.ConfirmPasswordField.IconBrush = Brushes.Red;
                    }
                });
           
                //full name text
                this.WhenAnyValue(x => x.FullNameField.Text).Subscribe(fullName =>
                {
                    if (FieldVerification.CanBeFullName(fullName))
                    {
                        this.FullNameField.IconKind = MaterialIconKind.CheckCircle;
                        this.FullNameField.IconBrush = Brushes.Green;
                    }
                    else
                    {
                        this.FullNameField.IconKind = MaterialIconKind.Cancel;
                        this.FullNameField.IconBrush = Brushes.Red;
                    }

                });

                //username text
                this.WhenAnyValue(x => x.UsernameField.Text).Subscribe(username =>
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

            }
        

        private void onBackToLogin() => this.authWindowVM.AuthContent = new LoginVM(authWindowVM);
        


    }
}
