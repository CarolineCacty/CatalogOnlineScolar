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
    public class InsertPredareViewModel : BaseViewModel
    {
        private string _numeMaterie;
        public string NumeMaterie
        {
            get { return _numeMaterie; }
            set { _numeMaterie = value; OnPropertyChanged(nameof(NumeMaterie)); }
        }

        private string _email;
        public string Email
        {
            get { return _email; }
            set { _email = value; OnPropertyChanged(nameof(Email)); }
        }

        private string _clasaId;
        public string ClasaID
        {
            get { return _clasaId; }
            set { _clasaId = value; OnPropertyChanged(nameof(ClasaID)); }
        }

        public ICommand InsertPredareCommand { get; set; }

        public InsertPredareViewModel()
        {
            InsertPredareCommand = new RelayCommand(InsertPredarea);
        }

        private void InsertPredarea(object parameter)
        {
            (new InsertPredareModel()).InsertPredaree(Email, ClasaID, NumeMaterie);
        }

    }
}
