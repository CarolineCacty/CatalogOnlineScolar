using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogScolarOnline.Model
{
    public class Accounts : INotifyPropertyChanged
    {
        public int AccountID { get; set; }

        public string Enail {  get; set; }
        public int Rol {  get; set; }

        private readonly Online_School_CatalogEntities _context;

        public event PropertyChangedEventHandler PropertyChanged;
        public Accounts()
        {
            _context = new Online_School_CatalogEntities();
        }

        public int getID(string _email)
        {
            if (string.IsNullOrWhiteSpace(_email))
                throw new ArgumentException("Email-ul nu poate fi gol.");

            var user = _context.Conturis.First(u => u.Email == _email);

            if (user == null)
                throw new InvalidOperationException("Utilizatorul nu a fost găsit.");

            return user.ContID;
        }

        public int getRol(string _email)
        {
            if (string.IsNullOrWhiteSpace(_email))
                throw new ArgumentException("Email-ul nu poate fi gol.");

            var user = _context.Conturis.First(u => u.Email == _email);

            if (user == null)
                throw new InvalidOperationException("Utilizatorul nu a fost găsit.");

            return user.Rol;
        }
    }
}
