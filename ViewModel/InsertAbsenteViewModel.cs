using CatalogScolarOnline.Model;
using CatalogScolarOnline.Utilities;
using CatalogScolarOnline.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CatalogScolarOnline.ViewModel
{
    public class InsertAbsenteViewModel : BaseViewModel
    {
        private DateTime _dataAbsenta;

        public DateTime DataAbsenta
        {
            get { return _dataAbsenta; }
            set { _dataAbsenta = value; OnPropertyChanged(nameof(DataAbsenta)); }
        }

        private int _motivata;
        public int Motivata
        {
            get { return _motivata; }
            set { _motivata = value; OnPropertyChanged(nameof(Motivata)); }
        }

        private string _text;
        private string _clasaID;
        private int _elevID;
        private int _predareID;
        private int _profesorID;
        private int _materieID;

        public int PredareID
        {
            get { return _predareID; }
            set { _predareID = value; OnPropertyChanged(nameof(PredareID)); }
        }

        public string ClasaID
        {
            get { return _clasaID; }
            set
            {
                _clasaID = value; OnPropertyChanged(nameof(ClasaID));
                ListaElevi = (new InsertNoteModel()).GetEleviList(_clasaID);
                ListaProfesori = (new InsertNoteModel()).GetProfesoriList(_clasaID);
                Text = "Profesor care predă la clasa " + _clasaID;
            }
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

        public string Text
        {
            get { return _text; }
            set { _text = value; OnPropertyChanged(nameof(Text)); }
        }

        public ICommand InsertAbsentaCommand { get; set; }

        public InsertAbsenteViewModel()
        {
            _clase = (new InsertNoteModel()).GetClase();
            InsertAbsentaCommand = new RelayCommand(InsertAbsenta);
        }

        private void InsertAbsenta(object parameter)
        {
            _profesorID = (new InsertNoteModel()).GetProfID(_profesorSelectat);
            _elevID = (new InsertNoteModel()).GetElevID(_elevSelectat);
            _materieID = (new InsertNoteModel()).GetMaterieID(_profesorID, _clasaID);
            _predareID = (new InsertNoteModel()).GetPredareID(_profesorID, _materieID, _clasaID);

            (new InsertAbsenteModel()).InsertAbsenta(_motivata,_dataAbsenta,_elevID,_predareID);
        }
    }
}
