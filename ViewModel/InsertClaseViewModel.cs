using CatalogScolarOnline.Model;
using CatalogScolarOnline.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CatalogScolarOnline.ViewModel
{
    public class InsertClaseViewModel : BaseViewModel
    {
        private string _email;
        public string Email
        {
            get { return _email; }
            set { _email = value; OnPropertyChanged(nameof(Email)); }
        }

        private string _clasaID;
        public string ClasaID
        {
            get { return _clasaID; }
            set { _clasaID = value; OnPropertyChanged(nameof(ClasaID)); }
        }

        private string _specializare;
        public string Specializare
        {
            get { return _specializare; }
            set { _specializare = value; OnPropertyChanged(nameof(Specializare)); }
        }

        private int _anScolar;
        public int AnScolar
        {
            get { return _anScolar; }
            set { _anScolar = value; OnPropertyChanged(nameof(AnScolar)); }
        }
        public ICommand InsertClaseCommand { get; set; }
        public InsertClaseViewModel()
        {
            InsertClaseCommand = new RelayCommand(InsertClase);
        }

        private void InsertClase(object parameter)
        {
            (new InsertClaseModel()).InsertClasa(_email,_clasaID,_specializare,_anScolar);
        }
    }
}
