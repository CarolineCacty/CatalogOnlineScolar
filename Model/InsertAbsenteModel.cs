using CatalogScolarOnline.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CatalogScolarOnline.Model
{
    public class InsertAbsenteModel : BaseModel
    {
        public void InsertAbsenta(int motivata,DateTime data,int elevID,int predareID)
        {
            try
            {
                Absente absenta = new Absente
                {
                    Motivata = motivata == 0 ? false : true,
                    Data_absenta = data,
                    ElevID = elevID,
                    PredareID = predareID
                };
                Context.Absentes.InsertOnSubmit(absenta);
                Context.SubmitChanges();
                string mesaj = "Insertie Absenta " + " a fost realizată cu succes!";
                MessageBox.Show(mesaj, "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"A apărut o eroare la inserare absenta : {ex.Message}", "Eroare", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public int GetPredareID(int profID, int materieID, string clasaID)
        {
            int predareID =
                (from p in Context.Predares
                 where p.ProfesorID == profID && p.ClasaID == clasaID && p.MaterieID == materieID
                 select p.PredareID).FirstOrDefault();
            return predareID;
        }

        public List<string> GetEleviList(string clasaID)
        {
            List<string> eleviList = new List<string>();
            var list =
                (from e in Context.Elevis
                 where e.ClasaID == clasaID
                 select e.Nume + " " + e.Prenume);

            foreach (string item in list)
            {
                eleviList.Add(item);
            }

            return eleviList;
        }

        public List<string> GetProfesoriList(string clasaID)
        {
            List<string> profList = new List<string>();
            var list =
                (from p in Context.Predares
                 where p.ClasaID == clasaID
                 select p.ProfesorID);

            foreach (int item in list)
            {
                string itemStr =
                    (from p in Context.Profesoris
                     where p.ProfesorID == item
                     select p.Nume + " " + p.Prenume).FirstOrDefault();
                profList.Add(itemStr);
            }

            return profList;
        }

        public List<string> GetClase()
        {
            List<string> clase = new List<string>();
            var list =
                (from c in Context.Clases
                 select c.ClasaID);

            foreach (string item in list)
            {
                clase.Add(item);
            }

            return clase;
        }

        public int GetElevID(string elev)
        {
            var parts = elev.Split(' ');
            if (parts.Length < 2)
            {
                throw new ArgumentException("Stringul elev trebuie să conțină atât numele, cât și prenumele.");
            }
            string nume = parts[0];
            string prenume = parts[1];


            int elevID =
                (from e in Context.Elevis
                 where e.Nume == nume && e.Prenume == prenume
                 select e.ElevID).FirstOrDefault();
            return elevID;
        }

        public int GetProfID(string profesor)
        {
            var parts = profesor.Split(' ');
            if (parts.Length < 2)
            {
                throw new ArgumentException("Stringul profesor trebuie să conțină atât numele, cât și prenumele.");
            }
            string nume = parts[0];
            string prenume = parts[1];

            int profID =
                (from p in Context.Profesoris
                 where p.Nume == nume && p.Prenume == prenume
                 select p.ProfesorID).FirstOrDefault();

            return profID;
        }

        public int GetMaterieID(int profID, string clasaID)
        {
            int matID =
                (from p in Context.Predares
                 where p.ProfesorID == profID && p.ClasaID == clasaID
                 select p.MaterieID).FirstOrDefault();
            return matID;
        }
    }
}
