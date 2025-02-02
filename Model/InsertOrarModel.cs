using CatalogScolarOnline.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;
using System.Windows;

namespace CatalogScolarOnline.Model
{
    public class InsertOrarModel : BaseModel
    {
        internal void InsertOrar(string ziSaptamana, string oraSelectata, int predareID,string clasaID)
        {
            int intervalID = 0;
            switch (oraSelectata)
            {
                case "System.Windows.Controls.ComboBoxItem: 8:00-8:50" :
                    intervalID = 1;
                    break;
                case "System.Windows.Controls.ComboBoxItem: 9:00-9:50":
                    intervalID = 2;
                    break;
                case "System.Windows.Controls.ComboBoxItem: 10:00-10:50":
                    intervalID = 3;
                    break;
                case "System.Windows.Controls.ComboBoxItem: 11:00-11:50":
                    intervalID = 4;
                    break;
                case "System.Windows.Controls.ComboBoxItem: 12:00-12:50":
                    intervalID = 5;
                    break;
                case "System.Windows.Controls.ComboBoxItem: 13:00-13:50":
                    intervalID = 6;
                    break;
                default :
                    intervalID = 0;
                    break;
            }

            switch (ziSaptamana)
            {
                case "System.Windows.Controls.ComboBoxItem: Luni":
                    ziSaptamana = "Luni";
                    break;
                case "System.Windows.Controls.ComboBoxItem: Marti":
                    ziSaptamana = "Marti";
                    break;
                case "System.Windows.Controls.ComboBoxItem: Miercuri":
                    ziSaptamana = "Miercuri";
                    break;
                case "System.Windows.Controls.ComboBoxItem: Joi":
                    ziSaptamana = "Joi";
                    break;
                case "System.Windows.Controls.ComboBoxItem: Vineri":
                    ziSaptamana = "Vineri";
                    break;
            }

            try
            {
                OrarClase orarClase = new OrarClase
                {
                    PredareID = predareID,
                    Zi_saptamana = ziSaptamana,
                    ClasaID = clasaID,
                    IntervalID = intervalID
                };
                Context.OrarClases.Add(orarClase);
                Context.SaveChanges();
                string mesaj = "Insertia înregistrării orar " + " a fost realizată cu succes!";
                MessageBox.Show(mesaj, "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"A apărut o eroare la inserarea înregistrării orar: {ex.Message}", "Eroare", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
