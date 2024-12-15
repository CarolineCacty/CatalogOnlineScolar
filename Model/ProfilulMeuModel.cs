using CatalogScolarOnline.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CatalogScolarOnline.Utilities;

namespace CatalogScolarOnline.Model
{
    public class ProfilulMeuModel
    {
        public string Nume { get; set; }
        public string Prenume { get; set; }
        public string Clasa { get; set; }
        public string Diriginte { get; set; }
        public string Specializare { get; set; }
        public string Email { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private readonly OnlineSchoolCatalogDataContext _context;
        public ProfilulMeuModel()
        {
            _context = new OnlineSchoolCatalogDataContext();
        }

        public ProfilulMeuModel GetDetails()
        {

            ProfilulMeuModel profilulMeuModel = new ProfilulMeuModel();

            profilulMeuModel.Email = Session.Email;
            int? userID = Session.UtilizatorID;

            if (userID == null) return null;

            profilulMeuModel.Nume = 
                (from u in _context.Utilizatoris
                join e in _context.Elevis on u.UtilizatorID equals e.UtilizatorID
                where u.UtilizatorID == userID
                select e.Nume).First();

            profilulMeuModel.Prenume =
                (from u in _context.Utilizatoris
                 join e in _context.Elevis on u.UtilizatorID equals e.UtilizatorID
                 where u.UtilizatorID == userID
                 select e.Prenume).First();

            profilulMeuModel.Clasa = 
                (from u in _context.Utilizatoris
                 join e in _context.Elevis on u.UtilizatorID equals e.UtilizatorID
                 where u.UtilizatorID == userID
                 select e.ClasaID).First();

            profilulMeuModel.Diriginte =
                (from u in _context.Utilizatoris
                 join e in _context.Elevis on u.UtilizatorID equals e.UtilizatorID
                 join c in _context.Clases on e.ClasaID equals c.ClasaID
                 join p in _context.Profesoris on c.Diriginte equals p.ProfesorID
                 where u.UtilizatorID == userID
                 select p.Grad + " " + p.Nume + " " + p.Prenume
                ).First();  

            profilulMeuModel.Specializare =
                (from u in _context.Utilizatoris
                 join e in _context.Elevis on u.UtilizatorID equals e.UtilizatorID
                 join c in _context.Clases on e.ClasaID equals c.ClasaID
                 where u.UtilizatorID == userID
                 select c.Specializare).First();


            return profilulMeuModel;
        }
    }
}
