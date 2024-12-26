using CatalogScolarOnline.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CatalogScolarOnline.Utilities;

namespace CatalogScolarOnline.ViewModel
{
    public class AbsenteViewModel : BaseViewModel
    {
        private bool _motivata;
        public bool Motivata
        {
            get { return _motivata; }
            set
            {
                if (_motivata != value)
                {
                    _motivata = value;
                    OnPropertyChanged(nameof(Motivata));
                }
            }
        }

        private DateTime _date;

        public DateTime Date
        {
            get { return _date; }
            set
            {
                if (_date != value)
                {
                    _date = value;
                    OnPropertyChanged(nameof(Date));
                }
            }
        }

        private string _motivataAsString;

        public string MotivataAsString
        {
            get { return _motivataAsString; }
            set
            {
                if (_motivataAsString != value)
                {
                    _motivataAsString = value;
                    OnPropertyChanged(nameof(MotivataAsString));
                }
            }
        }


        private ObservableCollection<AbsenteModel> _absente_list;
        public ObservableCollection<AbsenteModel> absente_list
        {
            get { return _absente_list; }
            set
            {
                if (_absente_list != value)
                {
                    _absente_list = value;
                    OnPropertyChanged(nameof(absente_list));
                }
            }
        }

        public AbsenteViewModel()
        {
            absente_list = (new AbsenteModel()).GetAbsenteListByElevID(Session.GetElevId());
        }
    }
}
