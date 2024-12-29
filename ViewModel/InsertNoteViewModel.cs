using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using CatalogScolarOnline.Model;
using CatalogScolarOnline.Utilities;
using CatalogScolarOnline.Views;

namespace CatalogScolarOnline.ViewModel
{
    public class InsertNoteViewModel : BaseViewModel
    {
        private string _text;
        private string _clasaID;
        private int _elevID;
        private int _predareID;
        private int _profesorID;
        private int _materieID;
        private int _nota;
        private DateTime _dataNota;

        public DateTime DataNota
        {
            get { return _dataNota; }
            set { _dataNota = value; OnPropertyChanged(nameof(DataNota)); }
        }

        public int PredareID
        {
            get { return _predareID; }
            set { _predareID = value; OnPropertyChanged(nameof(PredareID)); }
        }
        public int Nota
        {
            get { return _nota; }
            set { _nota = value; OnPropertyChanged(nameof(Nota)); }
        }
        public string ClasaID
        {
            get { return _clasaID; }
            set 
            {  _clasaID = value; OnPropertyChanged(nameof(ClasaID)); 
                ListaElevi = (new InsertNoteModel()).GetEleviList(_clasaID);
                ListaProfesori = (new InsertNoteModel()).GetProfesoriList(_clasaID);
                Text = "Profesor care predă la clasa " + _clasaID;
            }
        }

        public string Text
        {
            get { return _text; }
            set { _text = value; OnPropertyChanged(nameof(Text)); }
        }

        private List<string> _clase;
        public List<string> Clase
        {
            get { return _clase; }
            set { _clase = value; OnPropertyChanged(nameof(Clase)); }
        }


        private List<string> _listaElevi;
        public List<string> ListaElevi
        {
            get { return _listaElevi; }
            set { _listaElevi = value; OnPropertyChanged(nameof(ListaElevi)); }
        }

        private string _elevSelectat;
        public string ElevSelectat
        {
            get { return _elevSelectat; }
            set { _elevSelectat = value; OnPropertyChanged(nameof(ElevSelectat)); }
        }

        private List<string> _listaProfesori;
        public List<string> ListaProfesori
        {
            get { return _listaProfesori; }
            set { _listaProfesori = value; OnPropertyChanged(nameof(ListaProfesori)); }
        }

        private string _profesorSelectat;
        public string ProfesorSelectat
        {
            get { return _profesorSelectat; }
            set { _profesorSelectat = value; OnPropertyChanged(nameof(ProfesorSelectat)); }
        }

        public ICommand InsertNotaCommand { get; set; }

        public InsertNoteViewModel()
        {
            _clase = (new InsertNoteModel()).GetClase();
            InsertNotaCommand = new RelayCommand(InsertNota);
        }

        private void InsertNota(object parameter)
        {
            if (_profesorSelectat == null || _elevSelectat == null || _dataNota == null || _clasaID == null)
            {
                MessageBox.Show($"Nu pot exista câmpuri goale", "Eroare", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            _profesorID = (new InsertNoteModel()).GetProfID(_profesorSelectat);
            _elevID = (new InsertNoteModel()).GetElevID(_elevSelectat);
            _materieID = (new InsertNoteModel()).GetMaterieID(_profesorID, _clasaID);
            _predareID = (new InsertNoteModel()).GetPredareID(_profesorID,_materieID,_clasaID); 
            
            (new InsertNoteModel()).InsertNota(_nota,_dataNota,_elevID,_predareID,_elevSelectat);
        }

    }
}
