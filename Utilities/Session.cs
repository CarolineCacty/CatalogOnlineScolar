using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CatalogScolarOnline.Model;
using Flattinger.Core.Interface.ToastMessage;

namespace CatalogScolarOnline.Utilities
{
    public static class Session
    {
        public static int? UtilizatorID { get; set; }
        public static string Email { get; set; }
        public static string ClasaID { get; set; }

        public static int? Rol {  get; set; }

        private static readonly Online_School_CatalogEntities _context = new Online_School_CatalogEntities();
        public static int GetElevId()
        {
            int elevID = 0;
            if (Session.GetRol() == 0)
            {
                    elevID =
                (from user in _context.Utilizatoris
                 join e in _context.Elevis on user.UtilizatorID equals e.UtilizatorID
                 where user.Email == Session.Email
                 select e.ElevID).FirstOrDefault();


                return elevID;
            }
            else if (Session.GetRol() == 1)
            {
                elevID =
            (from user in _context.Utilizatoris
             join p in _context.Parintis on user.UtilizatorID equals p.UtilizatorID
             join e in _context.Elevis on p.ParinteID equals e.ParinteID
             where user.Email == Session.Email
             select e.ElevID).FirstOrDefault();

                return elevID;
            }
            return elevID;
        }

        public static int GetRol(string email)
        {
            int rol;

            var item =
                (from u in _context.Utilizatoris
                 join c in _context.Conturis on u.Email equals c.Email
                 where c.Email == email
                 select c.Rol).FirstOrDefault();

            rol = Convert.ToInt32(item);


            return rol;
        }

        public static int GetRol()
        {
            int rol;

            string email = Session.Email;

            var item =
                (from u in _context.Utilizatoris
                 join c in _context.Conturis on u.Email equals c.Email
                 where c.Email == email
                 select c.Rol).FirstOrDefault();

            rol = Convert.ToInt32(item);


            return rol;
        }

        

        public static int GetProfesorId()
        {
            int profesorId =
                (from user in _context.Utilizatoris
                 join p in _context.Profesoris on user.UtilizatorID equals p.UtilizatorID
                 where user.Email == Session.Email
                 select p.ProfesorID).FirstOrDefault();

            return profesorId;
        }

        public static string GetClasaID()
        {
            string clasaID;
            int parinteID = GetParinteID(); // Obținem ParinteID înainte de interogare
            int profesorID = GetProfesorId(); // Obținem ProfesorID înainte de interogare

            if (Session.GetRol(Email) == 2)
            {
                clasaID = _context.Clases
                    .Where(c => c.Diriginte == profesorID)
                    .Select(c => c.ClasaID)
                    .FirstOrDefault();
            }
            else
            {
                clasaID = _context.Elevis
                    .Where(e => e.ParinteID == parinteID)
                    .Select(e => e.ClasaID)
                    .FirstOrDefault();
            }

            return clasaID;
        }



        public static int GetParinteID()
        {
            return _context.Parintis
                .Where(p => p.UtilizatorID == Session.UtilizatorID)
                .Select(p => p.ParinteID)
                .FirstOrDefault();
        }

    }
}
