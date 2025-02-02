using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CatalogScolarOnline.Model
{
    public class InsertMaterieModel : BaseModel
    {
        public void InsertMaterie(string numeMaterie)
        {
            try
            {
                Materii materie = new Materii
                {
                    Nume_materie = numeMaterie
            
                };
                Context.Materiis.Add(materie);
                Context.SaveChanges();
                string mesaj = "Insertia Materiei " + numeMaterie + " a fost realizata cu succes!";
                MessageBox.Show(mesaj, "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"A apărut o eroare la inserarea materiei {numeMaterie}: {ex.Message}", "Eroare", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }
    }
}
