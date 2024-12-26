using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
                    OnPropertyChanged(); 
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
                    OnPropertyChanged(); 
                }
            }
        }

        private Visibility _noteVisibility = Visibility.Visible;
        private Visibility _absenteVisibility = Visibility.Visible;

        private Visibility _vizualizareRaportVisibility = Visibility.Collapsed;
        private Visibility _generareRaportVisibility = Visibility.Collapsed;
        public Visibility VizualizareRaportVisibility
        {
            get { return _vizualizareRaportVisibility; }
            set
            {
                if (_vizualizareRaportVisibility != value)
                {
                    _vizualizareRaportVisibility = value;
                    OnPropertyChanged(nameof(VizualizareRaportVisibility));
                }
            }
        }

        public Visibility GenerareRaportVisibility
        {
            get { return _generareRaportVisibility; }
            set
            {
                if (_generareRaportVisibility != value)
                {
                    _generareRaportVisibility = value;
                    OnPropertyChanged(nameof(GenerareRaportVisibility));
                }
            }
        }
        public Visibility NoteVisibility
        {
            get { return _noteVisibility; }
            set
            {
                if (_noteVisibility != value)
                {
                    _noteVisibility = value;
                    OnPropertyChanged(nameof(NoteVisibility));
                }
            }
        }

        public Visibility AbsenteVisibility
        {
            get { return _absenteVisibility; }
            set
            {
                if (_absenteVisibility != value)
                {
                    _absenteVisibility = value;
                    OnPropertyChanged(nameof(AbsenteVisibility));
                }
            }
        }

        private ObservableCollection<Grades> _grades;

        private RaportEvaluare raportEvaluare;
        public RaportEvaluare RaportEvaluare
        {
            get { return raportEvaluare; }
            set
            {
                if (raportEvaluare != value)
                {
                    raportEvaluare = value;
                    OnPropertyChanged(nameof(RaportEvaluare));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand CloseCommand { get; }
        public ICommand LoginWindow { get; }
        public ICommand NavigateToNotesCommand { get; }
        public ICommand ShowOrarCommand {  get; }
        public ICommand ShowAbsenteCommand { get; }
        public ICommand ShowMyProfileCommand { get; }

        public ICommand ShowRaportEvaluareCommand { get; }
        public ICommand ShowGenerateRaportEvaluareCommand { get; }

        public MainWindowViewModel()
        {
            CloseCommand = new RelayCommand(CloseApplication);
            LoginWindow = new RelayCommand(LoginWindowShow);
            NavigateToNotesCommand = new RelayCommand(NavigateToNotes);
            ShowOrarCommand = new RelayCommand(ShowOrar);
            ShowAbsenteCommand = new RelayCommand(ShowAbsente);
            ShowMyProfileCommand = new RelayCommand(ShowMyProfile);
            ShowRaportEvaluareCommand = new RelayCommand(ShowRaportEvaluare);
            ShowGenerateRaportEvaluareCommand = new RelayCommand(ShowGenerateRaportEvaluare);
        }

        public MainWindowViewModel(string email)
        {
            int rol = Session.GetRol(email);
            if (rol == 2)
            {
                _noteVisibility = Visibility.Collapsed;
                _absenteVisibility = Visibility.Collapsed;
                _generareRaportVisibility = Visibility.Visible;
            }
            else if(rol == 0 || rol == 1)
            {
                _vizualizareRaportVisibility = Visibility.Visible;
            }

            CloseCommand = new RelayCommand(CloseApplication);
            LoginWindow = new RelayCommand(LoginWindowShow);
            NavigateToNotesCommand = new RelayCommand(NavigateToNotes);
            ShowOrarCommand = new RelayCommand(ShowOrar);
            ShowAbsenteCommand = new RelayCommand(ShowAbsente);
            ShowMyProfileCommand = new RelayCommand(ShowMyProfile);
            ShowRaportEvaluareCommand = new RelayCommand(ShowRaportEvaluare);
            ShowGenerateRaportEvaluareCommand = new RelayCommand(ShowGenerateRaportEvaluare);
        }

        private void ShowGenerateRaportEvaluare(object parameter)
        {
            var mainWindow = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
            if (mainWindow != null)
            {
                var generarePage = new Views.GenerareRaport();
                mainWindow.MainFrame.Navigate(generarePage);
            }
        }


        public void ShowRaportEvaluare(object parameter)
        {
            var mainWindow = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
            if (mainWindow != null)
            {
                raportEvaluare = (new VizualizareRaportModel()).GetRaportEvaluare(Session.GetElevId());
                var vizualizarePage = new Views.VizualizareRaport(raportEvaluare);
                mainWindow.MainFrame.Navigate(vizualizarePage);
            }
        }

        private void NavigateToNotes(object parameter)
        {
            var mainWindow = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
            if (mainWindow != null)
            {
                var notePage = new Views.Note();
                notePage.ReceiveEmail(_email);
                
                mainWindow.MainFrame.Navigate(notePage);
            }
        }

        private void ShowOrar(object parameter)
        {
            var mainWindow = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
            if (mainWindow != null)
            {
                var orarPage = new Views.OrarPage();
                
                mainWindow.MainFrame.Navigate(orarPage);
            }
        }

        private void ShowAbsente(object parameter)
        {
            var mainWindow = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
            if (mainWindow != null)
            {
                var absentePage = new Views.Absente();


                mainWindow.MainFrame.Navigate(absentePage);
            }
        }

        private void ShowMyProfile(object parameter)
        {
            var mainWindow = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
            if (mainWindow != null)
            {
                var myProfilePage = new Views.ProfilulMeu();
                mainWindow.MainFrame.Navigate(myProfilePage);
            }
        }

        public void SetEmail(string email)
        {
            this._email = email;
            Email = email;
            //Name = students.GetName(this._email);
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
