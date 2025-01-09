using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using CatalogScolarOnline.Model;
using CatalogScolarOnline.Utilities;
using CatalogScolarOnline.Views;
using Flattinger.UI.ToastMessage;
using Flattinger.Core.MVVM;
using Flattinger.UI.ToastMessage.Controls;
using Flattinger.Core.Interface.ToastMessage;
using System.Threading;
using System;


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
        private Visibility _notificaritVisibility = Visibility.Collapsed;

        private Visibility _nrNotificariVisibility = Visibility.Collapsed;
        private Visibility _chatVisibility = Visibility.Collapsed;

        
        public Visibility ChatVisibility
        {
            get { return _chatVisibility; }
            set
            {
                if (_chatVisibility != value)
                {
                    _chatVisibility = value;
                    OnPropertyChanged(nameof(ChatVisibility));
                }
            }
        }
        public Visibility NrNotificariVisibility
        {
            get { return _nrNotificariVisibility; }
            set
            {
                if (_nrNotificariVisibility != value)
                {
                    _nrNotificariVisibility = value;
                    OnPropertyChanged(nameof(NrNotificariVisibility));
                }
            }
        }
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

        public Visibility NotificariVisibility
        {
            get { return _notificaritVisibility; }
            set
            {
                if (_notificaritVisibility != value)
                {
                    _notificaritVisibility = value;
                    OnPropertyChanged(nameof(NotificariVisibility));
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

        private  int _numarNotificari;
        public  int NumarNotificari
        {
            get => _numarNotificari;
            set
            {
                if (_numarNotificari != value)
                {
                    _numarNotificari = value;
                    OnPropertyChanged(nameof(NumarNotificari));
                }
            }
        }

        public void SetNumarNotificari(int nr)
        {
            NumarNotificari = nr;
        }
        private readonly Timer _notificariTimer;

        //private ToastProvider _toastProvider;
        public ICommand CloseCommand { get; }
        public ICommand LoginWindow { get; }
        public ICommand NavigateToNotesCommand { get; }
        public ICommand ShowOrarCommand {  get; }
        public ICommand ShowAbsenteCommand { get; }
        public ICommand ShowMyProfileCommand { get; }

        public ICommand ShowRaportEvaluareCommand { get; }
        public ICommand ShowGenerateRaportEvaluareCommand { get; }

        //public ICommand SendTestNotification { get; set; }
        public ICommand ShowNotificariCommand { get; set; }

        public ICommand ShowChatCommand { get; set; }

        private void VerificaNotificari(object state)
        {
            int notificariNoi = (new NotificariModel()).GetNrNotificariNoi();
            NumarNotificari = notificariNoi;
        }
        public MainWindowViewModel()
        {
            CloseCommand = new CatalogScolarOnline.Utilities.RelayCommand(CloseApplication);
            LoginWindow = new CatalogScolarOnline.Utilities.RelayCommand(LoginWindowShow);
            NavigateToNotesCommand = new CatalogScolarOnline.Utilities.RelayCommand(NavigateToNotes);
            ShowOrarCommand = new CatalogScolarOnline.Utilities.RelayCommand(ShowOrar);
            ShowAbsenteCommand = new CatalogScolarOnline.Utilities.RelayCommand(ShowAbsente);
            ShowMyProfileCommand = new CatalogScolarOnline.Utilities.RelayCommand(ShowMyProfile);
            ShowRaportEvaluareCommand = new CatalogScolarOnline.Utilities.RelayCommand(ShowRaportEvaluare);
            ShowGenerateRaportEvaluareCommand = new CatalogScolarOnline.Utilities.RelayCommand(ShowGenerateRaportEvaluare);
            //SendTestNotification = new Flattinger.Core.MVVM.RelayCommand(parameter => OnSend());
            ShowNotificariCommand = new CatalogScolarOnline.Utilities.RelayCommand(ShowNotificari);
            ShowChatCommand = new CatalogScolarOnline.Utilities.RelayCommand(ShowChat);
        }

        

        //public void OnSend()
        //{
        //    _toastProvider.NotificationService.AddNotification(Flattinger.Core.Enums.ToastType.WARNING, "Entry not found", "The entry not found in your Database", 5, animationConfig: new AnimationConfig { });
        //}

        
        public MainWindowViewModel(string email)
        {
            int rol = Session.GetRol(email);
            if (rol == 2)
            {
                _noteVisibility = Visibility.Collapsed;
                _absenteVisibility = Visibility.Collapsed;
                _generareRaportVisibility = Visibility.Visible;
                _chatVisibility = Visibility.Visible;
            }
            else if(rol == 0 || rol == 1)
            {
                _vizualizareRaportVisibility = Visibility.Visible;
                if(rol == 1)
                {
                    _notificaritVisibility = Visibility.Visible;
                    _numarNotificari = (new NotificariModel()).GetNrNotificariNoi();
                    NrNotificariVisibility = _numarNotificari != 0 ? Visibility.Visible : Visibility.Collapsed;
                    _chatVisibility = Visibility.Visible;
                }
                else _notificaritVisibility= Visibility.Collapsed;
            }

            //_notificariTimer = new Timer(VerificaNotificari, null, TimeSpan.Zero, TimeSpan.FromSeconds(1.5));

            //_toastProvider = new ToastProvider(notificationContainer);

            CloseCommand = new CatalogScolarOnline.Utilities.RelayCommand(CloseApplication);
            LoginWindow = new CatalogScolarOnline.Utilities.RelayCommand(LoginWindowShow);
            NavigateToNotesCommand = new CatalogScolarOnline.Utilities.RelayCommand(NavigateToNotes);
            ShowOrarCommand = new CatalogScolarOnline.Utilities.RelayCommand(ShowOrar);
            ShowAbsenteCommand = new CatalogScolarOnline.Utilities.RelayCommand(ShowAbsente);
            ShowMyProfileCommand = new CatalogScolarOnline.Utilities.RelayCommand(ShowMyProfile);
            ShowRaportEvaluareCommand = new CatalogScolarOnline.Utilities.RelayCommand(ShowRaportEvaluare);
            ShowGenerateRaportEvaluareCommand = new CatalogScolarOnline.Utilities.RelayCommand(ShowGenerateRaportEvaluare);
            ShowNotificariCommand = new CatalogScolarOnline.Utilities.RelayCommand(ShowNotificari);
            //SendTestNotification = new Flattinger.Core.MVVM.RelayCommand(parameter => OnSend());
            ShowChatCommand = new CatalogScolarOnline.Utilities.RelayCommand(ShowChat);
        }

        private void ShowChat(object parameter)
        {
            var mainWindow = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
            if (mainWindow != null)
            {
                var chatPage = new Views.Chat();
                mainWindow.MainFrame.Navigate(chatPage);
            }
        }
        private void ShowNotificari(object parameter)
        {
            var mainWindow = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
            if (mainWindow != null)
            {
                var notificariPage = new Views.Notificari(this);
                mainWindow.MainFrame.Navigate(notificariPage);
            }
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
            _numarNotificari = (new NotificariModel()).GetNrNotificariNoi();
            NrNotificariVisibility = _numarNotificari != 0 ? Visibility.Visible : Visibility.Collapsed;
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
            _numarNotificari = (new NotificariModel()).GetNrNotificariNoi();
            NrNotificariVisibility = _numarNotificari != 0 ? Visibility.Visible : Visibility.Collapsed;
        }

        private void ShowOrar(object parameter)
        {
            var mainWindow = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
            if (mainWindow != null)
            {
                var orarPage = new Views.OrarPage();
                
                mainWindow.MainFrame.Navigate(orarPage);
            }
            _numarNotificari = (new NotificariModel()).GetNrNotificariNoi();
            NrNotificariVisibility = _numarNotificari != 0 ? Visibility.Visible : Visibility.Collapsed;
        }

        private void ShowAbsente(object parameter)
        {
            var mainWindow = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
            if (mainWindow != null)
            {
                var absentePage = new Views.Absente();


                mainWindow.MainFrame.Navigate(absentePage);
            }
            _numarNotificari = (new NotificariModel()).GetNrNotificariNoi();
            NrNotificariVisibility = _numarNotificari != 0 ? Visibility.Visible : Visibility.Collapsed;
        }

        private void ShowMyProfile(object parameter)
        {
            var mainWindow = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
            if (mainWindow != null)
            {
                var myProfilePage = new Views.ProfilulMeu();
                mainWindow.MainFrame.Navigate(myProfilePage);
            }
            _numarNotificari = (new NotificariModel()).GetNrNotificariNoi();
            NrNotificariVisibility = _numarNotificari != 0 ? Visibility.Visible : Visibility.Collapsed;
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
