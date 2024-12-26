using CatalogScolarOnline.Model;
using CatalogScolarOnline.Utilities;
using CatalogScolarOnline.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CatalogScolarOnline.ViewModel
{
    public class GenerareRaportViewModel : BaseViewModel
    {
        private string _clasaID;
        private int _elevID;
        private int _diriginte;

        public string ClasaID
        {
            get { return _clasaID; }
            set
            {
                _clasaID = value; OnPropertyChanged(nameof(ClasaID));
            }
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

        private string _comportament;
        public string Comportament
        {
            get { return _comportament; }
            set { _comportament = value; OnPropertyChanged(nameof(Comportament)); }
        }

        private string _descriere;
        public string Descriere
        {
            get { return _descriere; }
            set { _descriere = value; OnPropertyChanged(nameof(Descriere)); }
        }

        private RaportEvaluare _raportEvaluare;
        public RaportEvaluare RaportEvaluare
        {
            get { return _raportEvaluare; }
            set { _raportEvaluare = value; OnPropertyChanged(nameof(RaportEvaluare)); }
        }


        public ICommand GenerareRaportCommand { get; set; }

        public GenerareRaportViewModel()
        {
            _diriginte = Session.GetProfesorId();
            _clasaID = (new GenerareRaportModel()).GetClasaID(_diriginte);
            ListaElevi = (new InsertNoteModel()).GetEleviList(_clasaID);
            GenerareRaportCommand = new RelayCommand(GenerateRaport);
        }

        private void GenerateRaport(object parameter)
        {
            if (_elevSelectat == null) return;
            _elevID = (new InsertNoteModel()).GetElevID(_elevSelectat);

            if(_comportament == null) return;
            _raportEvaluare = (new GenerareRaportModel()).GenerareRaport(_elevSelectat,_clasaID,_elevID,_comportament,_descriere);

            (new GenerareRaportModel()).ActualizareRaport(_raportEvaluare, _elevID);

            var mainWindow = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
            if (mainWindow != null)
            {
                var vizualizarePage = new Views.VizualizareRaport(_raportEvaluare);
                mainWindow.MainFrame.Navigate(vizualizarePage);
            }
        }
    }
}
