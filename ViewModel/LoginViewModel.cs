using System.ComponentModel;
using System.Windows.Input;
using System.Windows;
using CatalogScolarOnline.Utilities;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace CatalogScolarOnline.ViewModel
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        private string _email;
        private bool _isEmailVisible = true;
        private bool _isErrorVisible = false;

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
        public bool IsErrorVisible
        {
            get { return _isErrorVisible;  }
            set
            {
                if (_isErrorVisible != value)
                {
                    _isErrorVisible = value;
                    OnPropertyChanged(nameof(IsErrorVisible));
                }
            }
        }

        private void ValidateEmail()
        {
            if (!string.IsNullOrEmpty(_email))
            {
                IsErrorVisible = !IsValidEmail(Email);
            }
        }

        // Validarea sintaxei emailului
        private bool IsValidEmail(string email)
        {
            var regex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
            return regex.IsMatch(email);
        }


        public ICommand CloseCommand { get; }
        public ICommand OpenRegisterWindow { get; }

        public LoginViewModel()
        {
            CloseCommand = new RelayCommand(CloseApplication);
            OpenRegisterWindow = new RelayCommand(OpenRegister);
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
