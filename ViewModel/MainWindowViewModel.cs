using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        private ObservableCollection<Grades> _grades;

        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand CloseCommand { get; }
        public ICommand LoginWindow { get; }

        public ICommand NavigateToNotesCommand { get; }
        public MainWindowViewModel()
        {
            CloseCommand = new RelayCommand(CloseApplication);
            LoginWindow = new RelayCommand(LoginWindowShow);
            NavigateToNotesCommand = new RelayCommand(NavigateToNotes);

        }
        private void NavigateToNotes(object parameter)
        {
            var mainWindow = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
            if (mainWindow != null)
            {
                // Navighează la pagina Note
                var notePage = new Views.Note();
                notePage.ReceiveEmail(_email);
                
                mainWindow.MainFrame.Navigate(notePage);
            }
        }
        public void SetEmail(string email)
        {
            this._email = email;
            Email = email;
            Name = students.GetName(this._email);
        }
        private void CloseApplication(object parameter)
        {
            var currentWindow = Application.Current.Windows.OfType<Window>().FirstOrDefault(w => w.IsActive);
            currentWindow?.Close();
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
