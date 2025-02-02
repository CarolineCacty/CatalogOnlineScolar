using System.ComponentModel;
using System.Windows.Input;
using System.Linq;
using System.Windows;
using CatalogScolarOnline.Model;
using CatalogScolarOnline.Utilities;
using System.Net;
using System.Security.Claims;
using System;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using CatalogScolarOnline.Views;
using System.IO;

namespace CatalogScolarOnline.ViewModel
{

    
    public class RegisterViewModel : INotifyPropertyChanged
    {
        private string _lastName;
        private string _firstName;
        private string _email;
        private string _password;
        private string _selectedRole;
        private string _registrationMessage;

        //Profesor
        private string _materiePredata;
        private string _gradDidactic;
        private string _vechime;
        private string _clasa;

        public string MateriePredata
        {
            get { return _materiePredata; }
            set { _materiePredata = value; OnPropertyChanged(); }
        }

        public string GradDidactic
        {
            get { return _gradDidactic; }
            set { _gradDidactic = value; OnPropertyChanged(); }
        }

        public string Vechime
        {
            get { return _vechime; }
            set { _vechime = value; OnPropertyChanged(); }
        }

        public string Clasa
        {
            get { return _clasa; }
            set { _clasa = value; OnPropertyChanged(); }
        }


        //Parinte
        private string _telefon;

        public string Telefon
        {
            get { return _telefon; }
            set { _telefon = value; OnPropertyChanged(); }
        }

        //Elev
        private string _adresaElev;
        private string _clasaElev;
        private string _numeParinte;
        private string _prenumeParinte;
        private DateTime _dataNasterii;

        public string AdresaElev
        {
            get { return _adresaElev; }
            set { _adresaElev = value; OnPropertyChanged(); }
        }

        public string ClasaElev
        {
            get { return _clasaElev; }
            set { _clasaElev = value; OnPropertyChanged(); }
        }

        public string NumeParinte
        {
            get { return _numeParinte; }
            set { _numeParinte = value; OnPropertyChanged(); }
        }

        public string PrenumeParinte
        {
            get { return _prenumeParinte; }
            set { _prenumeParinte = value; OnPropertyChanged(); }
        }

        public DateTime DataNasterii
        {
            get { return _dataNasterii; }
            set { _dataNasterii = value; OnPropertyChanged(); }
        }

        //Comune
        private UserRepository userRepository = new UserRepository();

        public string LastName
        {
            get { return _lastName; }
            set { _lastName = value; OnPropertyChanged(); }
        }

        public string FirstName
        {
            get { return _firstName; }
            set { _firstName = value; OnPropertyChanged(); }
        }

        public string Email
        {
            get { return _email; }
            set { _email = value; OnPropertyChanged(); }
        }

        public string Password
        {
            get { return _password; }
            set { _password = value; OnPropertyChanged(); }
        }

        public string SelectedRole
        {
            get { return _selectedRole; }
            set { _selectedRole = value; OnPropertyChanged(); UpdateVisibility(); }
        }

        private Visibility _stackProfesorVisibility = Visibility.Collapsed;
        private Visibility _stackElevVisibility = Visibility.Collapsed;
        private Visibility _stackParinteVisibility = Visibility.Collapsed;  
        private Visibility _stackAdminVisibility = Visibility.Collapsed;  
        private Visibility _notAdmin = Visibility.Collapsed;

        public Visibility NotAdmin
        {
            get { return _notAdmin; }
            set
            {
                if (_notAdmin != value)
                {
                    _notAdmin = value;
                    OnPropertyChanged(nameof(NotAdmin));
                }
            }
        }

        public Visibility StackProfesorVisibility
        {
            get { return _stackProfesorVisibility; }
            set
            {
                if (_stackProfesorVisibility != value)
                {
                    _stackProfesorVisibility = value;
                    OnPropertyChanged(nameof(StackProfesorVisibility));
                }
            }
        }

        public Visibility StackElevVisibility
        {
            get { return _stackElevVisibility; }
            set
            {
                if (_stackElevVisibility != value)
                {
                    _stackElevVisibility = value;
                    OnPropertyChanged(nameof(StackElevVisibility));
                }
            }
        }

        public Visibility StackParinteVisibility
        {
            get { return _stackParinteVisibility; }
            set
            {
                if (_stackParinteVisibility != value)
                {
                    _stackParinteVisibility = value;
                    OnPropertyChanged(nameof(StackParinteVisibility));
                }
            }
        }
        public Visibility StackAdminVisibility
        {
            get { return _stackAdminVisibility; }
            set
            {
                if (_stackAdminVisibility != value)
                {
                    _stackAdminVisibility = value;
                    OnPropertyChanged(nameof(StackAdminVisibility));
                }
            }
        }


        private bool _isProfesorVisible;
        public bool IsProfesorVisible
        {
            get => _isProfesorVisible;
            set
            {
                _isProfesorVisible = value;
                OnPropertyChanged();
            }
        }

        private bool _isElevVisible;
        public bool IsElevVisible
        {
            get => _isElevVisible;
            set
            {
                _isElevVisible = value;
                OnPropertyChanged();
            }
        }

        private bool _isParinteVisible;
        public bool IsParinteVisible
        {
            get => _isParinteVisible;
            set
            {
                _isParinteVisible = value;
                OnPropertyChanged();
            }
        }

        private bool _isAdminVisible;
        public bool IsAdminVisible
        {
            get => _isAdminVisible;
            set
            {
                _isAdminVisible = value;
                OnPropertyChanged();
            }
        }

        private void UpdateVisibility()
        {
            IsProfesorVisible = SelectedRole == "System.Windows.Controls.ComboBoxItem: Profesor";
            IsElevVisible = SelectedRole == "System.Windows.Controls.ComboBoxItem: Elev";
            IsParinteVisible = SelectedRole == "System.Windows.Controls.ComboBoxItem: Părinte";
            IsAdminVisible = SelectedRole == "System.Windows.Controls.ComboBoxItem: Admin";

            if (IsProfesorVisible) { NotAdmin = Visibility.Visible; StackAdminVisibility = Visibility.Collapsed; StackProfesorVisibility = Visibility.Visible; StackElevVisibility = Visibility.Collapsed; StackParinteVisibility = Visibility.Collapsed; }
            if (IsElevVisible) { NotAdmin = Visibility.Visible; StackAdminVisibility = Visibility.Collapsed; StackElevVisibility = Visibility.Visible; StackProfesorVisibility = Visibility.Collapsed; StackParinteVisibility = Visibility.Collapsed;  }
            if (IsParinteVisible){ NotAdmin = Visibility.Visible; StackAdminVisibility = Visibility.Collapsed; StackParinteVisibility = Visibility.Visible; StackProfesorVisibility = Visibility.Collapsed; StackElevVisibility = Visibility.Collapsed; }
            if (IsAdminVisible) { NotAdmin = Visibility.Collapsed; StackAdminVisibility = Visibility.Visible; StackParinteVisibility = Visibility.Collapsed; StackProfesorVisibility = Visibility.Collapsed; StackElevVisibility = Visibility.Collapsed; }
        }

        public string RegistrationMessage
        {
            get { return _registrationMessage; }
            set { _registrationMessage = value; OnPropertyChanged(); }
        }


        private Visibility _gridVisibility = Visibility.Collapsed;
        public Visibility GridVisibility
        {
            get { return _gridVisibility; }
            set
            {
                if (_gridVisibility != value)
                {
                    _gridVisibility = value;
                    OnPropertyChanged(nameof(GridVisibility));
                }
            }
        }


        public ICommand RegisterCommand { get; }
        public ICommand CancelCommand { get; }
        public ICommand CloseCommand { get; }
        public ICommand MinimizeCommand { get; }
        public ICommand ShowFields {  get; }

        public RegisterViewModel()
        {
            RegisterCommand = new RelayCommand(ExecuteRegister);
            CancelCommand = new RelayCommand(ExecuteCancel);
            CloseCommand = new RelayCommand(ExecuteClose);
            MinimizeCommand = new RelayCommand(ExecuteMinimize);
            ShowFields = new RelayCommand(ExecuteShowFields);
        }

      

        private void ExecuteShowFields(object obj)
        {
            GridVisibility = GridVisibility == Visibility.Collapsed ? Visibility.Visible : Visibility.Collapsed;
        }

       

        private void ClearFields()
        {
            LastName = string.Empty;
            FirstName = string.Empty;
            Email = string.Empty;
            Password = string.Empty;
            MateriePredata = string.Empty;
            GradDidactic = string.Empty;
            Vechime = string.Empty;
            Clasa = string.Empty;
            ClasaElev = string.Empty;
            AdresaElev = string.Empty;
            NumeParinte = string.Empty;
            PrenumeParinte = string.Empty;
            Telefon = string.Empty;
        }

        private void ExecuteCancel(object parameter)
        {
            Login loginWindow = new Login();
            loginWindow.Show();
            Application.Current.Windows[0]?.Close();
            ClearFields();
        }

        private void ExecuteClose(object parameter)
        {
            Application.Current.Shutdown();
        }

        private void ExecuteMinimize(object parameter)
        {
            Application.Current.MainWindow.WindowState = WindowState.Minimized;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void ExecuteRegister(object parameter)
        {
            int roleCode = SelectedRole == "System.Windows.Controls.ComboBoxItem: Profesor" ? 2 : SelectedRole == "System.Windows.Controls.ComboBoxItem: Elev" ? 0 : SelectedRole == "System.Windows.Controls.ComboBoxItem: Părinte" ? 1 : 3;
            bool userRegistered = userRepository.RegisterUser(Email, Password, roleCode);
            if (!userRegistered)
            {
                RegistrationMessage = $"Utilizatorul cu emailul {Email} există deja.";
                return;
            }

            bool okRegistration = true;
            using (var context = new CatalogScolarOnline.Model.Online_School_CatalogEntities())
            {

                var newUser = context.Utilizatoris.FirstOrDefault(u => u.Email == Email);

                    if (SelectedRole == "System.Windows.Controls.ComboBoxItem: Profesor")
                    {
                        userRepository.InsertProfesor(context, newUser.UtilizatorID, LastName, FirstName, GradDidactic);
                    }
                    else if (SelectedRole == "System.Windows.Controls.ComboBoxItem: Elev")
                    {
                        if(!userRepository.InsertElev(context, newUser.UtilizatorID, ClasaElev, NumeParinte, PrenumeParinte, LastName, FirstName, AdresaElev, DataNasterii))
                        {
                            using (var newContext = new CatalogScolarOnline.Model.Online_School_CatalogEntities())
                            {
                                var userToDelete = newContext.Utilizatoris.FirstOrDefault(u => u.Email == Email);
                                if (userToDelete != null)
                                {
                                    newContext.Utilizatoris.Remove(userToDelete);
                                    newContext.SaveChanges();
                                    RegistrationMessage = "A aparut o eroare!";
                            }
                            }
                            okRegistration = false;
                        }
                    }
                    else if (SelectedRole == "System.Windows.Controls.ComboBoxItem: Părinte")
                    {
                        if(!userRepository.InsertParinte(context, newUser.UtilizatorID, LastName, FirstName, Telefon))
                        {
                            using (var newContext = new CatalogScolarOnline.Model.Online_School_CatalogEntities())
                            {
                                var userToDelete = newContext.Utilizatoris.FirstOrDefault(u => u.Email == Email);
                                if (userToDelete != null)
                                {
                                    newContext.Utilizatoris.Remove(userToDelete);
                                    newContext.SaveChanges();
                                    RegistrationMessage = "A aparut o eroare!";
                                }
                            }
                            okRegistration = false;
                        }
                    }
            }

            if(okRegistration) RegistrationMessage = "Înregistrarea a avut succes!";
            ClearFields();
            MessageBox.Show(RegistrationMessage, "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            Login loginWindow = new Login();
            loginWindow.Show();
            Application.Current.Windows[0]?.Close();
        }
    }
}
