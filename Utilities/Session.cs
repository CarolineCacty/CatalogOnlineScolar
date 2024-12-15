using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CatalogScolarOnline.Model;

namespace CatalogScolarOnline.Utilities
{
    public static class Session
    {
        public static int? UtilizatorID { get; set; }
        public static string Email { get; set; }
        public static string ClasaID { get; set; }

        private static readonly OnlineSchoolCatalogDataContext _context = new OnlineSchoolCatalogDataContext();
        public static int GetElevId()
        {
            int elevID;

            var item =
                (from u in _context.Utilizatoris
                join e in _context.Elevis on u.UtilizatorID equals e.UtilizatorID
                where e.UtilizatorID == UtilizatorID
                select e.ElevID).FirstOrDefault();

            elevID = Convert.ToInt32(item);


            return elevID;
        }
    }
}
