using CatalogScolarOnline.Model;
using CatalogScolarOnline.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Syncfusion.DocIO.DLS;
using System.ComponentModel;
using System.Windows;
using Syncfusion.Licensing;

namespace CatalogScolarOnline.ViewModel
{
    public class VizualizareRaportViewModel : BaseViewModel
    {
        private string _nume;
        private double _mediaGenerala;
        private int _absenteMotivate;
        private int _absenteNemotivate;
        private string _descriere;
        private string _comportament;
        private DateTime _dataGenerare;

        public DateTime DataGenerare
        {
            get { return _dataGenerare; }
            set
            {
                _dataGenerare = value; OnPropertyChanged(nameof(DataGenerare));
            }
        }
        public double MediaGenerala
        {
            get { return _mediaGenerala; }
            set
            {
                _mediaGenerala = value; OnPropertyChanged(nameof(MediaGenerala));
            }
        }
        public int AbsenteMotivate
        {
            get { return _absenteMotivate; }
            set
            {
                _absenteMotivate = value; OnPropertyChanged(nameof(AbsenteMotivate));
            }
        }
        public int AbsenteNemotivate
        {
            get { return _absenteMotivate; }
            set
            {
                _absenteNemotivate = value; OnPropertyChanged(nameof(AbsenteNemotivate));
            }
        }

        public string Nume
        {
            get { return _nume; }
            set
            {
                _nume = value; OnPropertyChanged(nameof(Nume));
            }
        }
        public string Descriere
        {
            get { return _descriere; }
            set
            {
                _descriere = value; OnPropertyChanged(nameof(Descriere));
            }
        }

        public string Comportament
        {
            get { return _comportament; }
            set
            {
                _comportament = value; OnPropertyChanged(nameof(Comportament));
            }
        }

        public void InitRaport(RaportEvaluare raport)
        {
            _nume = raport.Nume;
            _mediaGenerala = raport.MediaGenerala;
            _absenteMotivate = raport.AbsenteMotivate;
            _absenteNemotivate = raport.AbsenteNemotivate;
            _comportament = raport.Comportament;
            _descriere = raport.Descriere;
            _dataGenerare = raport.DataGenerare;
        }

        public ICommand ExportCommand { get; set; }


        public VizualizareRaportViewModel(RaportEvaluare raport)
        {
            ExportCommand = new RelayCommand(Export);
            InitRaport(raport);
        }

        private void Export(object parameter)
        {
            using (WordDocument document = new WordDocument())
            {
                Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Ngo9BigBOggjHTQxAR8/V1NMaF5cXmBCf0x3WmFZfVtgc19HZ1ZQQmY/P1ZhSXxXd0dhXH5XcXNUQGddUUQ=");

                //Adds a section and a paragraph to the document
                document.EnsureMinimal();

                // Adaugă un titlu la începutul documentului (cu stil de titlu, de exemplu Heading1)
                WParagraph titleParagraph = document.LastParagraph;
                titleParagraph.AppendText("Raport Evaluare");


                // Adaugă un paragraf cu text
                document.AddSection(); // Adaugă o secțiune nouă pentru următorul paragraf
                document.LastParagraph.AppendText("Nume: " + _nume);

                // Adaugă mai multe paragrafe
                document.AddSection();
                document.LastParagraph.AppendText("Media Generala: " + _mediaGenerala.ToString());

                // Poți adăuga mai multe paragrafe după cum este necesar
                document.AddSection();
                document.LastParagraph.AppendText("Comportament: " + _comportament);

                // Adaugă un paragraf cu text dintr-o variabilă (_nume)
                document.AddSection();
                document.LastParagraph.AppendText("Descriere "+ _descriere);

                //Saves the Word document
                document.Save("Result.docx");
            }
        }

        public VizualizareRaportViewModel()
        {
        }
    }
        
}
