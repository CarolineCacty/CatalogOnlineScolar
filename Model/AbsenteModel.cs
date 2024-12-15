using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CatalogScolarOnline.Utilities;

namespace CatalogScolarOnline.Model
{
    public class AbsenteModel
    {
        private int _AbsentaID;

        private DateTime _Data_absenta;

        private bool _Motivata;

        private int _ElevID;

        private int _PredareID;

        private string _materie;

        public string Materie { get; set; } 

        public int AbsentaID { get; set; }
        public DateTime Data_absenta { get; set; }
        public bool Motivata { get; set; }
        public int ElevID { get; set; }
        public int PredareID { get; set; }

        private string _motivataAsString;

        public string MotivataAsString
        {
            get { return _motivataAsString; }
            set
            {
                if (_motivataAsString != value)
                {
                    _motivataAsString = value;
                }
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        private readonly OnlineSchoolCatalogDataContext _context;

        public AbsenteModel()
        {
            _context = new OnlineSchoolCatalogDataContext();
        }

        public ObservableCollection<AbsenteModel> GetAbsenteList()
        {
            ObservableCollection<AbsenteModel> Absente = new ObservableCollection<AbsenteModel>();

            foreach(var item in _context.Absentes)
            {
                Absente.Add(
                    new AbsenteModel 
                    {
                        AbsentaID = item.AbsentaID,
                        Data_absenta = item.Data_absenta,
                        Motivata = item.Motivata,
                        PredareID = item.PredareID,
                        ElevID = item.ElevID
                    });
            }

            return Absente; 
        }

        public ObservableCollection<AbsenteModel> GetAbsenteListByElevID(int elevId)
        {
            ObservableCollection<AbsenteModel> Absente = new ObservableCollection<AbsenteModel>();

            var list = _context.Absentes.Where(a => a.ElevID == elevId);
            if (list == null)
                throw new ArgumentNullException("Nu exista elevul respectiv!\n");

            foreach (var item in list)
            {

                string element =
                    (from a in _context.Absentes
                     join pr in _context.Predares on a.PredareID equals pr.PredareID
                     join m in _context.Materiis on pr.MaterieID equals m.MaterieID
                     select m.Nume_materie).FirstOrDefault(); 

                Absente.Add(
                    new AbsenteModel
                    {
                        AbsentaID = item.AbsentaID,
                        Data_absenta = item.Data_absenta,
                        Motivata = item.Motivata,
                        PredareID = item.PredareID,
                        ElevID = item.ElevID,
                        Materie = element,
                        _motivataAsString = Motivata == true ? "DA" : "NU"
                    });
            }

            return Absente;
        }

    }
}
