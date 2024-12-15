using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CatalogScolarOnline.Model;

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
            _profilulMeu = (new ProfilulMeuModel()).GetDetails();
            _email = _profilulMeu.Email;
            _diriginte = _profilulMeu.Diriginte;
            _prenume = _profilulMeu.Prenume;
            _nume = _profilulMeu.Nume;
            _specializare = _profilulMeu.Specializare;
            _clasa = _profilulMeu.Clasa;
        }
    }
}
