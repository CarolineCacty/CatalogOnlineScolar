using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using CatalogScolarOnline.Utilities;

namespace CatalogScolarOnline.Model
{
    public class InsertClaseModel
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private readonly OnlineSchoolCatalogDataContext _context;
        public InsertClaseModel()
        {
            _context = new OnlineSchoolCatalogDataContext();
        }

        public void InsertClasa(string email,string clasaID,string specializare,int anScolar)
        {
            try
            {
                int diriginte =
                (from u in _context.Utilizatoris
                 join p in _context.Profesoris on u.UtilizatorID equals p.UtilizatorID
                 where u.Email == email
                 select p.ProfesorID).FirstOrDefault();

                Clase clase = new Clase
                {
                    ClasaID = clasaID,
                    An_scolar = anScolar,
                    Specializare = specializare,
                    Diriginte = diriginte,
                    
                };

                _context.Clases.InsertOnSubmit(clase);
                _context.SubmitChanges();
                string mesaj = "Insertia Clasei " + clasaID + " a fost realizata cu succes!";
                MessageBox.Show(mesaj, "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            
            catch (Exception ex)
            {
                MessageBox.Show($"A apărut o eroare la inserarea clasei {clasaID}: {ex.Message}", "Eroare", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
    }
}
