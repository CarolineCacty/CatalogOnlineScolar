using CatalogScolarOnline.Model;
using CatalogScolarOnline.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CatalogScolarOnline.ViewModel
{
    public class InsertOrarViewModel : BaseViewModel
    {
        private string _clasaID;
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
                ListaProfesori = (new InsertNoteModel()).GetProfesoriList(_clasaID);
            }
        }


        private List<string> _clase;
        public List<string> Clase
        {
            get { return _clase; }
            set { _clase = value; OnPropertyChanged(nameof(Clase)); }
        }


        private string _ziSaptamana;
        public string ZiSaptamana
        {
            get { return _ziSaptamana; }
            set { _ziSaptamana = value; OnPropertyChanged(nameof(ZiSaptamana)); }
        }

        private string _oraSelectata;
        public string OraSelectata
        {
            get { return _oraSelectata; }
            set { _oraSelectata = value; OnPropertyChanged(nameof(OraSelectata)); }
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

        public ICommand InsertOrarCommand { get; set; }

        public InsertOrarViewModel()
        {
            _clase = (new InsertNoteModel()).GetClase();
            InsertOrarCommand = new RelayCommand(InsertOrar);
        }

        private void InsertOrar(object parameter)
        {
            if (_profesorSelectat == null || _clasaID == null || _ziSaptamana == null || _oraSelectata == null)
            {
                MessageBox.Show($"Nu pot exista câmpuri goale", "Eroare", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            _profesorID = (new InsertNoteModel()).GetProfID(_profesorSelectat);
            _materieID = (new InsertNoteModel()).GetMaterieID(_profesorID, _clasaID);
            _predareID = (new InsertNoteModel()).GetPredareID(_profesorID, _materieID, _clasaID);

            (new InsertOrarModel()).InsertOrar(_ziSaptamana,_oraSelectata,_predareID,_clasaID);
        }
    }
}
