using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CatalogScolarOnline.Model;
using CatalogScolarOnline.Utilities;

namespace CatalogScolarOnline.ViewModel
{
    public class InsertMaterieViewModel : BaseViewModel
    {
        private string _numeMaterie;
        public string NumeMaterie
        {
            get { return _numeMaterie; }
            set { _numeMaterie = value; OnPropertyChanged(nameof(NumeMaterie)); }
        }

        public ICommand InsertMaterieCommand { get; set; }

        public InsertMaterieViewModel()
        {
            InsertMaterieCommand = new RelayCommand(InsertMaterie);
        }

        private void InsertMaterie(object parameter)
        {
            (new InsertMaterieModel()).InsertMaterie(NumeMaterie);
        }
    }
}
