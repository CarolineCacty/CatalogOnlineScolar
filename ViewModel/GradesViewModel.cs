using CatalogScolarOnline.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using CatalogScolarOnline.Utilities;

namespace CatalogScolarOnline.ViewModel
{
    public class GradesViewModel :INotifyPropertyChanged
    {

        private string _email;
        public string Email
        {
            get { return _email; }
            set
            {
                if (_email != value)
                {
                    _email = value;
                    OnPropertyChanged();
                }
            }
        }

        private ObservableCollection<Grades> _grades;

        public Students students = new Students();

        public event PropertyChangedEventHandler PropertyChanged;

        public GradesViewModel()
        {
            int studentID = (new Grades()).GetElevID(Session.Email);
            
            _grades = (new Grades()).getGrades(studentID);
        }
        public ObservableCollection<Grades> Grades => _grades;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void SetEmail(string email)
        {
            this._email = email;
            Email = email;
        }

    }
}
