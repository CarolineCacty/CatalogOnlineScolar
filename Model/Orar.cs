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
        private readonly OnlineSchoolCatalogDataContext _context;
        public event PropertyChangedEventHandler PropertyChanged;

        public Orar()
        {
            _context = new OnlineSchoolCatalogDataContext();
        }

        public string LoadClasaElevului()
        {
            var clasaElevului = (from elev in _context.Elevis
                                 join utilizator in _context.Utilizatoris
                                 on elev.UtilizatorID equals utilizator.UtilizatorID
                                 where utilizator.UtilizatorID == Session.UtilizatorID
                                 select elev.ClasaID).FirstOrDefault();

            if(Session.ClasaID == null)
                Session.ClasaID = clasaElevului;

            return clasaElevului;
        }

    }
}
