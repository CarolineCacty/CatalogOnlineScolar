using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace CatalogScolarOnline.Model
{
    public class Students : INotifyPropertyChanged
    {
        public int StudentID  { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Birthday { get; set; }
        public string Adress {  get; set; }
        public int ClassID {  get; set; }
        public int ParentID {  get; set; }
        public int UserID { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private readonly OnlineSchoolCatalogDataContext _context;

        private Users users= new Users();
        public Students()
        {
            _context = new OnlineSchoolCatalogDataContext();
        }
        public string GetName(string _email)
        {
            var userRol = users.getUserRol(_email);
            string nume = "";

            if (userRol == 0)
            {
                var userID = users.getUserID(_email);

                var user = _context.Elevis.FirstOrDefault(u => u.UtilizatorID == userID);

                if (user == null)
                    throw new InvalidOperationException("Utilizatorul nu a fost găsit.");
                nume= user.Nume + " " + user.Prenume;
            }

            return nume;
        }

    }
}
