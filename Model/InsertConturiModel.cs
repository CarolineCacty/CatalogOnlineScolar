using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CatalogScolarOnline.Model
{
    public class InsertConturiModel
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private readonly Online_School_CatalogEntities _context;
        public InsertConturiModel()
        {
            _context = new Online_School_CatalogEntities();
        }
        public void InsertCont(string email,int rol)
        {
            try
            {
                Conturi conturi = new Conturi()
                {
                    Email = email,
                    Rol = rol
                };
                _context.Conturis.Add(conturi);
                _context.SaveChanges();
                MessageBox.Show("Insertia Contului a fost realizata cu succes!", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex) 
            {
                MessageBox.Show($"A apărut o eroare la inserare cont: {ex.Message}", "Eroare", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }
    }
}
