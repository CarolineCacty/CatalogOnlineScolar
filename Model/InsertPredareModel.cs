using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CatalogScolarOnline.Model
{
    public class InsertPredareModel : BaseModel
    {
        private readonly Online_School_CatalogEntities _context;
        public InsertPredareModel()
        {
            _context = new Online_School_CatalogEntities();
        }

       
        public void InsertPredaree(string email,string clasaID,string numeMaterie)
        {
            try
            {
                int diriginte =
                (from u in Context.Utilizatoris
                 join p in Context.Profesoris on u.UtilizatorID equals p.UtilizatorID
                 where u.Email == email
                 select p.ProfesorID).First();

                int materieID =
                    (from m in Context.Materiis
                     where m.Nume_materie == numeMaterie
                     select m.MaterieID).First();

                Predare pr = new Predare
                {
                    ProfesorID = diriginte,
                    MaterieID = materieID,
                    ClasaID = clasaID,
                };
                _context.Predares.Add(pr);

                _context.SaveChanges();
                    
                string mesaj = "Insertia Predarii" + " a fost realizata cu succes!";
                MessageBox.Show(mesaj, "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"A apărut o eroare la inserarea predării: {ex.Message}", "Eroare", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
    }
}
