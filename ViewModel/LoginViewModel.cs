using System.ComponentModel;
using System.Windows.Input;
using System.Windows;
using CatalogScolarOnline.Utilities;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using Microsoft.Xaml.Behaviors.Media;
using System.Security;
using System.Linq;

namespace CatalogScolarOnline.ViewModel
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        private string _email;
        private string _password;
        private string _emailError;
        private string _loginError;

        public string Email
        {
            get { return _email;  }
            set
            {
                if (_email != value)
                {
                    _email = value;
                    OnPropertyChanged(nameof(Email));
                    ValidateEmail();
                }
            }
        }
        public string EmailError
        {
            get { return _emailError;  }

            set
            {
                _emailError = value;
                OnPropertyChanged(nameof(EmailError));
            }
        }

        public string LoginError
        {
            get { return _loginError; }

            set
            {
                //if (_loginError != value)
                //{
                    _loginError = value;
                    OnPropertyChanged(nameof(LoginError));
                //}
            }
        }
        private void ValidateEmail()
        {
            if (string.IsNullOrEmpty(_email))
            {
                EmailError = string.Empty; 
            }
            else if (!IsValidEmail(_email))
            {
                EmailError = "Invalid email!";
            }
            else
            {
                EmailError = string.Empty; 
            }
        }
        public string Password
        {
            get
            {
                return _password;
            }
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        private bool IsValidEmail(string email)
        {
            var regex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
            return regex.IsMatch(email);
        }

        public ICommand CloseCommand { get; }
        public ICommand OpenRegisterWindow { get; }
        public ICommand SignInCommand { get; }

        public LoginViewModel()
        {
            CloseCommand = new RelayCommand(CloseApplication);
            OpenRegisterWindow = new RelayCommand(OpenRegister);
            SignInCommand = new RelayCommand(ExecuteSignIn);
    
        }

        private void ExecuteSignIn(object parameter)
        {
            using (OnlineSchoolCatalogDataContext context = new OnlineSchoolCatalogDataContext())
            {
                var user = context.Utilizatoris.FirstOrDefault(u => u.Email == Email && u.Parola == Password);

                if (user == null)
                {
                    LoginError = "Invalid email or password.";
                }
                else
                {
                    LoginError = string.Empty;
                    Application.Current.Shutdown();
                }
            }
        }

        private void CloseApplication()
        {
            Application.Current.Shutdown();
        }

        private void OpenRegister()
        {
            Application.Current.Shutdown();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}