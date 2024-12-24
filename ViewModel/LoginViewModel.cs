using System.ComponentModel;
using System.Windows.Input;
using System.Windows;
using CatalogScolarOnline.Utilities;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Linq;
using CatalogScolarOnline.Model;
using CatalogScolarOnline.Views;

namespace CatalogScolarOnline.ViewModel
{

    public class LoginViewModel : INotifyPropertyChanged
    {
        private string _email;
        private string _password;
        private string _emailError;
        private string _loginError;


        private UserRepository userRepository = new UserRepository();
        public string Email
        {
            get { return _email; }
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
            get { return _emailError; }

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
                _loginError = value;
                OnPropertyChanged(nameof(LoginError));
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
            if (string.IsNullOrEmpty(_email) || string.IsNullOrEmpty(_password))
            {
                LoginError = "Email and password are required.";
                return;
            }

            bool isValidUser = userRepository.ValidateUser(_email, _password);
            if (isValidUser == false)
            {
                LoginError = "Invalid email or password.";
            }
            else
            {
                LoginError = string.Empty;
                var currentWindow = Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.IsActive);

                using (OnlineSchoolCatalogDataContext context = new OnlineSchoolCatalogDataContext())
                {
                    var user = context.Utilizatoris.FirstOrDefault(u => u.Email == Email && u.Parola == Password);
                    Session.UtilizatorID = user.UtilizatorID;
                    Session.Email = user.Email;
                }

                int rol = Session.GetRol();
                if(rol != 3)
                {
                    MainWindow mainWindow = new MainWindow(_email);
                    mainWindow.ReceiveEmail(_email);
                    mainWindow.Show();
                }
                else
                {
                    AdminWindow adminWindow = new AdminWindow();
                    adminWindow.Show();
                }

                if (currentWindow != null)
                {
                    currentWindow.Close();
                }
            }
        }   
        private void CloseApplication(object parameter)
        {
            var currentWindow = Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.IsActive);
            if (currentWindow != null)
            {
                currentWindow.Close();
            }
        }

        private void OpenRegister(object parameter)
        {
            var currentWindow = Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.IsActive);

            Register registerWindow = new Register();
            registerWindow.Show();

            if (currentWindow != null)
            {
                currentWindow.Close();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}