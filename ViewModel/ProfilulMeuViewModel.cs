using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using CatalogScolarOnline.Model;
using CatalogScolarOnline.Utilities;

namespace CatalogScolarOnline.ViewModel
{
    public class ProfilulMeuViewModel : BaseViewModel
    {
        private string _nume;
        private string _prenume;
        private string _clasa;
        private string _diriginte;
        private string _specializare;
        private string _email;
        private string _grad;
        private int? _vechime;
        private string _adresa;
        private string _copil;
        private string _telefon;
        public string Nume
        {
            get { return _nume; }
            set
            {
                if (_nume != value)
                {
                    _nume = value;
                    OnPropertyChanged(nameof(Nume));
                }
            }
        }

        public string Prenume
        {
            get { return _prenume; }
            set
            {
                if (_prenume != value)
                {
                    _prenume = value;
                    OnPropertyChanged(nameof(Prenume));
                }
            }
        }

        public string Clasa
        {
            get { return _clasa; }
            set
            {
                if (_clasa != value)
                {
                    _clasa = value;
                    OnPropertyChanged(nameof(Clasa));
                }
            }
        }

        public string Diriginte
        {
            get { return _diriginte; }
            set
            {
                if (_diriginte != value)
                {
                    _diriginte = value;
                    OnPropertyChanged(nameof(Diriginte));
                }
            }
        }

        public string Specializare
        {
            get { return _specializare; }
            set
            {
                if (_specializare != value)
                {
                    _specializare = value;
                    OnPropertyChanged(nameof(Specializare));
                }
            }
        }

        public string Email
        {
            get { return _email; }
            set
            {
                if (_email != value)
                {
                    _email = value;
                    OnPropertyChanged(nameof(Email));
                }
            }
        }

        public string Grad
        {
            get { return _grad; }
            set
            {
                if (_grad != value)
                {
                    _grad = value;
                    OnPropertyChanged(nameof(Grad));
                }
            }
        }

        public int? Vechime
        {
            get { return _vechime; }
            set
            {
                if (_vechime != value)
                {
                    _vechime = value;
                    OnPropertyChanged(nameof(Vechime));
                }
            }
        }

        public string Adresa
        {
            get { return _adresa; }
            set
            {
                if (_adresa != value)
                {
                    _adresa = value;
                    OnPropertyChanged(nameof(Adresa));
                }
            }
        }

        public string Copil
        {
            get { return _copil; }
            set
            {
                if (_copil != value)
                {
                    _copil = value;
                    OnPropertyChanged(nameof(Copil));
                }
            }
        }
        public string Telefon
        {
            get { return _telefon; }
            set
            {
                if (_telefon != value)
                {
                    _telefon = value;
                    OnPropertyChanged(nameof(Telefon));
                }
            }
        }


        private Visibility _clasaVisibility = Visibility.Collapsed;
        private Visibility _adresaVisibility = Visibility.Collapsed;
        private Visibility _telefonVisibility = Visibility.Collapsed;
        private Visibility _copilVisibility = Visibility.Collapsed;
        private Visibility _diriginteVisibility = Visibility.Collapsed;
        private Visibility _specializareVisibility = Visibility.Collapsed;
        private Visibility _gradVisibility = Visibility.Collapsed;
        private Visibility _vechimeVisibility = Visibility.Collapsed;

        public Visibility ClasaVisibility
        {
            get { return _clasaVisibility; }
            set
            {
                if (_clasaVisibility != value)
                {
                    _clasaVisibility = value;
                    OnPropertyChanged(nameof(ClasaVisibility));
                }
            }
        }

        public Visibility AdresaVisibility
        {
            get { return _adresaVisibility; }
            set
            {
                if (_adresaVisibility != value)
                {
                    _adresaVisibility = value;
                    OnPropertyChanged(nameof(AdresaVisibility));
                }
            }
        }

        public Visibility TelefonVisibility
        {
            get { return _telefonVisibility; }
            set
            {
                if (_telefonVisibility != value)
                {
                    _telefonVisibility = value;
                    OnPropertyChanged(nameof(TelefonVisibility));
                }
            }
        }

        public Visibility CopilVisibility
        {
            get { return _copilVisibility; }
            set
            {
                if (_copilVisibility != value)
                {
                    _copilVisibility = value;
                    OnPropertyChanged(nameof(CopilVisibility));
                }
            }
        }

        public Visibility DiriginteVisibility
        {
            get { return _diriginteVisibility; }
            set
            {
                if (_diriginteVisibility != value)
                {
                    _diriginteVisibility = value;
                    OnPropertyChanged(nameof(DiriginteVisibility));
                }
            }
        }

        public Visibility SpecializareVisibility
        {
            get { return _specializareVisibility; }
            set
            {
                if (_specializareVisibility != value)
                {
                    _specializareVisibility = value;
                    OnPropertyChanged(nameof(SpecializareVisibility));
                }
            }
        }

        public Visibility GradVisibility
        {
            get { return _gradVisibility; }
            set
            {
                if (_gradVisibility != value)
                {
                    _gradVisibility = value;
                    OnPropertyChanged(nameof(GradVisibility));
                }
            }
        }
        public Visibility VechimeVisibility
        {
            get { return _vechimeVisibility; }
            set
            {
                if (_vechimeVisibility != value)
                {
                    _vechimeVisibility = value;
                    OnPropertyChanged(nameof(VechimeVisibility));
                }
            }
        }

        private readonly OnlineSchoolCatalogDataContext _context;

        private ProfilulMeuModel _profilulMeu;
        public ProfilulMeuModel ProfilulMeu
        {
            get { return _profilulMeu; }
            set
            {
                if (_profilulMeu != value)
                {
                    _profilulMeu = value;
                    OnPropertyChanged(nameof(ProfilulMeu));
                }
            }
        }
        public ProfilulMeuViewModel()
        {
            _context = new OnlineSchoolCatalogDataContext();
            int Rol = Session.GetRol();
            switch (Rol)
            {
                case 0:
                    DetailsForElev();
                    break;
                case 1:
                    DetailsForParinte();
                    break;
                case 2:
                    DetailsForProfesor();
                    break;
                default:
                    MessageBox.Show("Eroare la Rol in ProfilulMeuViewModel", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                    break;
            }

        }

        private void DetailsForElev()
        {
            _profilulMeu = (new ProfilulMeuModel()).GetDetailsForElev();
            _email = _profilulMeu.Email;
            _diriginte = _profilulMeu.Diriginte;
            _prenume = _profilulMeu.Prenume;
            _nume = _profilulMeu.Nume;
            _specializare = _profilulMeu.Specializare;
            _clasa = _profilulMeu.Clasa;
            _adresa = _profilulMeu.Adresa;
            _clasaVisibility = Visibility.Visible;
            _specializareVisibility = Visibility.Visible;
            _diriginteVisibility = Visibility.Visible;
            _adresaVisibility = Visibility.Visible;
        }

        private void DetailsForProfesor()
        {
            _profilulMeu = (new ProfilulMeuModel()).GetDetailsForProfesor();
            _email = _profilulMeu.Email;
            _diriginte = _profilulMeu.Diriginte;
            _prenume = _profilulMeu.Prenume;
            _nume = _profilulMeu.Nume;
            _vechime = _profilulMeu.Vechime;
            _grad = _profilulMeu.GradDidactic;
            _diriginteVisibility = Visibility.Visible;
            _gradVisibility = Visibility.Visible;
            _vechimeVisibility = Visibility.Visible;
        }

        private void DetailsForParinte()
        {
            _profilulMeu = (new ProfilulMeuModel()).GetDetailsForParinte();
            _email = _profilulMeu.Email;
            _prenume = _profilulMeu.Prenume;
            _nume = _profilulMeu.Nume;
            _adresa = _profilulMeu.Adresa;
            _copil = _profilulMeu.Copil;
            _telefon = _profilulMeu.Telefon;
            _diriginte = _profilulMeu.Diriginte;
            _copilVisibility = Visibility.Visible;
            _telefonVisibility = Visibility.Visible;
            _adresaVisibility = Visibility.Visible;
            _diriginteVisibility = Visibility.Visible;
        }
    }
}
