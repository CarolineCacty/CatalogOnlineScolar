using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogScolarOnline.Model
{
    public class Users : INotifyPropertyChanged
    {
        public int UserID { get; set; }
        public string Email { get; set; }

        public int Rol {  get; set; } 

        public event PropertyChangedEventHandler PropertyChanged;

        private readonly OnlineSchoolCatalogDataContext _context;

        public Users()
        {
            _context = new OnlineSchoolCatalogDataContext();
        }
        public int getUserID(string _email)
        {
             var user = _context.Utilizatoris.First(u => u.Email==_email );

            if (user == null)
                    throw new InvalidOperationException("Utilizatorul nu a fost găsit.");
            return user.UtilizatorID;
        }

        public int getUserRol(string _email)
        {
            var user = _context.Utilizatoris.First(u => u.Email == _email);

            if (user == null)
                throw new InvalidOperationException("Utilizatorul nu a fost găsit.");
            return user.Rol;
        }
    }
}
