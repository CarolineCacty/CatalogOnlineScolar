using CatalogScolarOnline.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using CatalogScolarOnline.Utilities;

namespace CatalogScolarOnline.Model
{
    public class Orar
    {
        private readonly Online_School_CatalogEntities _context;
        public event PropertyChangedEventHandler PropertyChanged;

        public Orar()
        {
            _context = new Online_School_CatalogEntities();
        }

        public string LoadClasaElevului()
        {
            int Rol = Session.GetRol();

            string clasa="";

            if (Rol == 0)
            {
                clasa = (from elev in _context.Elevis
                                     join utilizator in _context.Utilizatoris
                                     on elev.UtilizatorID equals utilizator.UtilizatorID
                                     where utilizator.UtilizatorID == Session.UtilizatorID
                                     select elev.ClasaID).FirstOrDefault();
            }
            else if (Rol == 1)
            {
                clasa = (from parinte in _context.Parintis
                         join utilizator in _context.Utilizatoris on parinte.UtilizatorID equals utilizator.UtilizatorID
                         join elev in _context.Elevis on parinte.ParinteID equals elev.ParinteID
                         where utilizator.UtilizatorID == Session.UtilizatorID
                         select elev.ClasaID).FirstOrDefault();
            }

            if(Session.ClasaID == null)
                Session.ClasaID = clasa;

            return clasa;
        }

    }
}
