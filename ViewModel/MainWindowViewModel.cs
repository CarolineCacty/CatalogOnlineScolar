using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using CatalogScolarOnline.Model;
using CatalogScolarOnline.Utilities;
using CatalogScolarOnline.Views;

namespace CatalogScolarOnline.ViewModel
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        public string _email;

        public Students students= new Students();
        public string Email {
            get { return _email; }
            set
            {
                if (_email != value)
                {
                    _email = value;
                    OnPropertyChanged(); // Notifică UI-ul că proprietatea s-a schimbat.
                }
            }
         }
        public string _name { get; set; }
       
        public string Name
        {
            get { return _name; }
            set
            {
                if (_name != value)
                {
                    _name = value;
                    OnPropertyChanged(); // Notifică UI-ul că proprietatea s-a schimbat.
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand CloseCommand { get; }
        public ICommand LoginWindow { get; }
        public MainWindowViewModel()
        {
            CloseCommand = new RelayCommand(CloseApplication);
            LoginWindow = new RelayCommand(LoginWindowShow);
        }
        public void SetEmail(string email)
        {
            this._email = email;
            Email = email;
            Name = students.GetName(this._email);
        }
        private void CloseApplication(object parameter)
        {
            var currentWindow = Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.IsActive);
            if (currentWindow != null)
            {
                currentWindow.Close();
            }
        }

        private void LoginWindowShow(object parameter)
        {
            var currentWindow = Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.IsActive);

            Login login = new Login();
            login.Show();

            if (currentWindow != null)
            {
                currentWindow.Close();
            }
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
