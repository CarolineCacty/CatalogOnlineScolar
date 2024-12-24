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

        public int? Vechime { get; set; }
        public string GradDidactic { get; set; }
        public string Adresa { get; set; }
        public string Telefon {  get; set; }
        public string Copil {  get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private readonly OnlineSchoolCatalogDataContext _context;
        public ProfilulMeuModel()
        {
            _context = new OnlineSchoolCatalogDataContext();
        }

        public ProfilulMeuModel GetDetailsForElev()
        {

            ProfilulMeuModel profilulMeuModel = new ProfilulMeuModel();

            profilulMeuModel.Email = Session.Email;
            int? userID = Session.UtilizatorID;

            if (userID == null) return null;

            profilulMeuModel.Nume = 
                (from u in _context.Utilizatoris
                join e in _context.Elevis on u.UtilizatorID equals e.UtilizatorID
                where u.UtilizatorID == userID
                select e.Nume).FirstOrDefault();

            profilulMeuModel.Prenume =
                (from u in _context.Utilizatoris
                 join e in _context.Elevis on u.UtilizatorID equals e.UtilizatorID
                 where u.UtilizatorID == userID
                 select e.Prenume).FirstOrDefault();

            profilulMeuModel.Clasa = 
                (from u in _context.Utilizatoris
                 join e in _context.Elevis on u.UtilizatorID equals e.UtilizatorID
                 where u.UtilizatorID == userID
                 select e.ClasaID).FirstOrDefault();

            profilulMeuModel.Diriginte =
                (from u in _context.Utilizatoris
                 join e in _context.Elevis on u.UtilizatorID equals e.UtilizatorID
                 join c in _context.Clases on e.ClasaID equals c.ClasaID
                 join p in _context.Profesoris on c.Diriginte equals p.ProfesorID
                 where u.UtilizatorID == userID
                 select p.Grad + " " + p.Nume + " " + p.Prenume
                ).FirstOrDefault();  

            profilulMeuModel.Specializare =
                (from u in _context.Utilizatoris
                 join e in _context.Elevis on u.UtilizatorID equals e.UtilizatorID
                 join c in _context.Clases on e.ClasaID equals c.ClasaID
                 where u.UtilizatorID == userID
                 select c.Specializare).FirstOrDefault();

            profilulMeuModel.Adresa =
                (from u in _context.Utilizatoris
                 join e in _context.Elevis on u.UtilizatorID equals e.UtilizatorID
                 where u.UtilizatorID == userID
                 select e.Adresa).FirstOrDefault();




            return profilulMeuModel;
        }

        public ProfilulMeuModel GetDetailsForProfesor()
        {
            ProfilulMeuModel profilulMeuModel = new ProfilulMeuModel();

            profilulMeuModel.Email = Session.Email;
            int? userID = Session.UtilizatorID;

            if (userID == null) return null;

            profilulMeuModel.Nume =
                (from u in _context.Utilizatoris
                 join e in _context.Profesoris on u.UtilizatorID equals e.UtilizatorID
                 where u.UtilizatorID == userID
                 select e.Nume).FirstOrDefault();

            profilulMeuModel.Prenume =
                (from u in _context.Utilizatoris
                 join e in _context.Profesoris on u.UtilizatorID equals e.UtilizatorID
                 where u.UtilizatorID == userID
                 select e.Prenume).FirstOrDefault();

            int profID =
                (from u in _context.Utilizatoris
                 join e in _context.Profesoris on u.UtilizatorID equals e.UtilizatorID
                 where u.UtilizatorID == userID
                 select e.ProfesorID).FirstOrDefault();

            profilulMeuModel.Clasa =
                (from cl in _context.Clases
                join p in _context.Profesoris on cl.Diriginte equals p.ProfesorID
                where p.ProfesorID == profID
                select cl.ClasaID).FirstOrDefault();

            profilulMeuModel.Specializare =
                (from cl in _context.Clases
                 join p in _context.Profesoris on cl.Diriginte equals p.ProfesorID
                 where p.ProfesorID == profID
                 select cl.Specializare).FirstOrDefault();

            profilulMeuModel.Diriginte = (profilulMeuModel.Clasa != null) ? profilulMeuModel.Clasa + " " + profilulMeuModel.Specializare : "NU";
                
            profilulMeuModel.GradDidactic =
                (from p in _context.Profesoris
                where p.ProfesorID == profID
                select p.Grad).FirstOrDefault();

            profilulMeuModel.Vechime =
                (from p in _context.Profesoris
                 where p.ProfesorID == profID
                 select p.Vechime).FirstOrDefault();


            return profilulMeuModel;
        }

        public ProfilulMeuModel GetDetailsForParinte()
        {
            ProfilulMeuModel profilulMeuModel = new ProfilulMeuModel();

            profilulMeuModel.Email = Session.Email;
            int? userID = Session.UtilizatorID;

            if (userID == null) return null;

            profilulMeuModel.Nume =
                (from u in _context.Utilizatoris
                 join p in _context.Parintis on u.UtilizatorID equals p.UtilizatorID
                 where u.UtilizatorID == userID
                 select p.Nume_parinte).FirstOrDefault();

            profilulMeuModel.Prenume =
                (from u in _context.Utilizatoris
                 join p in _context.Parintis on u.UtilizatorID equals p.UtilizatorID
                 where u.UtilizatorID == userID
                 select p.Prenume_parinte).FirstOrDefault();

            profilulMeuModel.Telefon =
                (from u in _context.Utilizatoris
                 join p in _context.Parintis on u.UtilizatorID equals p.UtilizatorID
                 where u.UtilizatorID == userID
                 select p.Numar_telefon).FirstOrDefault();

            int parinteID =
                (from u in _context.Utilizatoris
                 join p in _context.Parintis on u.UtilizatorID equals p.UtilizatorID
                 where u.UtilizatorID == userID
                 select p.ParinteID).FirstOrDefault();

            profilulMeuModel.Adresa =
                (from u in _context.Utilizatoris
                 join p in _context.Parintis on u.UtilizatorID equals p.UtilizatorID
                 join e in _context.Elevis on p.ParinteID equals e.ParinteID
                 where p.ParinteID == parinteID
                 select e.Adresa).FirstOrDefault();

            var list =
                (from u in _context.Utilizatoris
                 join p in _context.Parintis on u.UtilizatorID equals p.UtilizatorID
                 join e in _context.Elevis on p.ParinteID equals e.ParinteID
                 where p.ParinteID == parinteID
                 select e.Nume + " " + e.Prenume).ToList();

            profilulMeuModel.Copil = string.Join("; ", list);


            profilulMeuModel.Diriginte =
                (from u in _context.Utilizatoris
                 join par in _context.Parintis on u.UtilizatorID equals par.UtilizatorID
                 join e in _context.Elevis on par.ParinteID equals e.ParinteID
                 join c in _context.Clases on e.ClasaID equals c.ClasaID
                 join p in _context.Profesoris on c.Diriginte equals p.ProfesorID
                 where u.UtilizatorID == userID
                 select p.Grad + " " + p.Nume + " " + p.Prenume
                ).FirstOrDefault();


            return profilulMeuModel;
        }
    }
}
