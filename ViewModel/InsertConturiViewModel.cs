using CatalogScolarOnline.Utilities;
using System.Windows.Input;
using CatalogScolarOnline.Model;

namespace CatalogScolarOnline.ViewModel
{
    public class InsertConturiViewModel : BaseViewModel
    {
        private string _email;
        private int _rol;
        private string _rolString;

        public string RolString
        {
            get { return _rolString; }
            set { _rolString = value; OnPropertyChanged(nameof(RolString)); }
        }
        
        public string Email
        { 
            get { return _email; } 
            set { _email = value; OnPropertyChanged(nameof(Email)); }
        }
        public int Rol
        {
            get { return _rol; }
            set { _rol = value; OnPropertyChanged(nameof(Rol)); }
        }
        public ICommand InsertContCommand { get; }
        
        public InsertConturiViewModel()
        {
            InsertContCommand = new RelayCommand(InsertCont);
            
        }
        private void InsertCont(object parameter)
        {
            _rol = _rolString == "System.Windows.Controls.ComboBoxItem: Profesor" ? 2 : _rolString == "System.Windows.Controls.ComboBoxItem: Elev" ? 0 : 1;
            (new InsertConturiModel()).InsertCont(_email, _rol);
        }
    }
}
