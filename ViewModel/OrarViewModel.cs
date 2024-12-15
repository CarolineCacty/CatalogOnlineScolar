using CatalogScolarOnline.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using CatalogScolarOnline.ViewModel;
using CatalogScolarOnline.Utilities;

namespace CatalogScolarOnline.ViewModel
{
    public class OrarViewModel : BaseViewModel
    {

        private string _class;

        public string Clasa
        {
            get { return _class; }
            set
            {
                _class = value;
                OnPropertyChanged(nameof(Clasa));
            }
        }
        private OnlineSchoolCatalogDataContext _context = new OnlineSchoolCatalogDataContext();

        public ObservableCollection<OrarRow> OrarClasa { get; set; } = new ObservableCollection<OrarRow>();

        private void LoadOrarForClass()
        {

            if (Session.ClasaID == null)
            {
                return;
            }

            var query = from orar in _context.OrarClases
                            join interval in _context.IntervaleOres on orar.IntervalID equals interval.IntervalID
                            join predare in _context.Predares on orar.PredareID equals predare.PredareID
                            join materie in _context.Materiis on predare.MaterieID equals materie.MaterieID
                            join profesor in _context.Profesoris on predare.ProfesorID equals profesor.ProfesorID
                            where orar.ClasaID == Session.ClasaID
                        orderby interval.IntervalID, orar.Zi_saptamana
                            select new
                            {
                                IntervalOrar = $"{interval.Ora_inceput:hh\\:mm} - {interval.Ora_sfarsit:hh\\:mm}",
                                orar.Zi_saptamana,
                                MaterieProfesor = $"{materie.Nume_materie} - {profesor.Nume} {profesor.Prenume}"
                            };

            var orarGrouped = query
            .ToList()
            .GroupBy(o => o.IntervalOrar)
            .Select(g => new OrarRow
            {
                IntervalOrar = g.Key,
                Luni = g.FirstOrDefault(o => o.Zi_saptamana == "Luni")?.MaterieProfesor,
                Marti = g.FirstOrDefault(o => o.Zi_saptamana == "Marti")?.MaterieProfesor,
                Miercuri = g.FirstOrDefault(o => o.Zi_saptamana == "Miercuri")?.MaterieProfesor,
                Joi = g.FirstOrDefault(o => o.Zi_saptamana == "Joi")?.MaterieProfesor,
                Vineri = g.FirstOrDefault(o => o.Zi_saptamana == "Vineri")?.MaterieProfesor
            });

            foreach (var row in orarGrouped)
                {
                    OrarClasa.Add(row);
                }
            
        }

        public OrarViewModel()
        {
            _class = (new Orar()).LoadClasaElevului();
            _class = "ORAR " + _class;
            LoadOrarForClass();
        }

    }
}
