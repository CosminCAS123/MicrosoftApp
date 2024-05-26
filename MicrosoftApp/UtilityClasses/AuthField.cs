using Avalonia.Media;
using Material.Icons;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicrosoftApp.UtilityClasses
{
    public class AuthField : ReactiveObject
    {
        
        public enum FieldType
        {
            UsernameField,
            PasswordField,
            ConfirmPasswordField,
            EmailField,
            FullNameField
        }
        private ISolidColorBrush iconBrush;
        private MaterialIconKind iconKind;
        private string tip;
        private string text = string.Empty;

        public FieldType Type { get; set; }
        public ISolidColorBrush IconBrush { get => this.iconBrush; set => this.RaiseAndSetIfChanged(ref iconBrush , value); }
        public MaterialIconKind IconKind { get => this.iconKind; set => this.RaiseAndSetIfChanged(ref iconKind, value); }
        public string Tip { get => this.tip; set => this.RaiseAndSetIfChanged(ref this.tip, value); }
        public string Text { get => this.text; set => this.RaiseAndSetIfChanged(ref text, value); }

        public static void SetTip(AuthField field)
        {
            if (field.IconKind == MaterialIconKind.CheckCircle)//good
            {
                field.Tip = FieldVerification.Tips.GoodTip;
            }
            else //bad
            {
                switch (field.Type)
                {
                    case FieldType.UsernameField:
                        field.Tip = FieldVerification.Tips.UsernameTip;
                        break;
                    case FieldType.PasswordField:
                        field.Tip = FieldVerification.Tips.PasswordTip;
                        break;
                    case FieldType.ConfirmPasswordField:
                        field.Tip = FieldVerification.Tips.ConfirmPasswordTip;
                        break;
                    case FieldType.FullNameField:
                        field.Tip += FieldVerification.Tips.FullNameTip;
                        break;
                    case FieldType.EmailField:
                        field.Tip = FieldVerification.Tips.EmailTip;
                        break;
                }

            }
        }


        }
}
