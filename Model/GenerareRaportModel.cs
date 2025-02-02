using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using CatalogScolarOnline.Utilities;

namespace CatalogScolarOnline.Model
{
    public class GenerareRaportModel : BaseModel
    {
        public string GetClasaID(int diriginte)
        {
            var clasa = Context.Clases.FirstOrDefault(c => c.Diriginte == diriginte);

            if (clasa == null)
            {
                MessageBox.Show("Dirigintele nu are o clasă asociată!", "Eroare", MessageBoxButton.OK, MessageBoxImage.Error);
                return null; 
            }

            return clasa.ClasaID;
        }


        public RaportEvaluare GenerareRaport(string elevSelectat, string clasaID, int elevID, string comportament, string descriere)
        {
            int nrAbsenteMotivate = 0, nrAbsenteNemotivate = 0;
            double medie = 0.0;

            nrAbsenteMotivate = Context.Absentes.Where(a => a.Motivata == true && a.ElevID == elevID).Count();
            nrAbsenteNemotivate = Context.Absentes.Where(a => a.Motivata == false && a.ElevID == elevID).Count();

            var list = 
                (from n in Context.Notes
                where n.ElevID == elevID
                select n.Nota).ToList();

            double suma = 0.0;
            foreach (double item in list)
            {
                suma += item;
            }

            medie = suma / (double)Context.Notes.Where(n => n.ElevID == elevID).Count();

            switch (comportament)
            {
                case "System.Windows.Controls.ComboBoxItem: Exemplar":
                    comportament = "Excelent";
                    break;
                case "System.Windows.Controls.ComboBoxItem: Foarte Bun":
                    comportament = "Foarte Bun";
                    break;
                case "System.Windows.Controls.ComboBoxItem: Bun":
                    comportament = "Bun";
                    break;
                case "System.Windows.Controls.ComboBoxItem: Rău":
                    comportament = "Rău";
                    break;
                case "System.Windows.Controls.ComboBoxItem: Foarte Rău":
                    comportament = "Foarte Rău";
                    break;
            }


            RaportEvaluare raport = new RaportEvaluare
            { 
                AbsenteMotivate = nrAbsenteMotivate,
                AbsenteNemotivate = nrAbsenteNemotivate,
                Nume = elevSelectat,
                MediaGenerala = medie,
                Comportament = comportament,
                Descriere = descriere,
                DataGenerare = DateTime.Today
            };

            return raport;

        }

        public void ActualizareRaport(RaportEvaluare raportEvaluare, int elevID)
        {
            try
            {
                var raportExistent = Context.RapoarteEvaluares.FirstOrDefault(r => r.ElevID == elevID);

                if (raportExistent != null)
                {
                    raportExistent.ElevID = elevID;
                    raportExistent.MediaGenerala = raportEvaluare.MediaGenerala;
                    raportExistent.AbsenteMotivate = raportEvaluare.AbsenteMotivate;
                    raportExistent.AbsenteNemotivate = raportEvaluare.AbsenteNemotivate;
                    raportExistent.Comportament = raportEvaluare.Comportament;
                    raportExistent.Descriere = raportEvaluare.Descriere;
                    raportExistent.DataGenerare = raportEvaluare.DataGenerare;

                    Context.SaveChanges();

                    MessageBox.Show("Raportul a fost actualizat cu succes!", "Succes", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    RapoarteEvaluare raportNou = new RapoarteEvaluare
                    {
                        ElevID = elevID,
                        MediaGenerala = raportEvaluare.MediaGenerala,
                        AbsenteMotivate = raportEvaluare.AbsenteMotivate,
                        AbsenteNemotivate = raportEvaluare.AbsenteNemotivate,
                        Comportament = raportEvaluare.Comportament,
                        Descriere = raportEvaluare.Descriere,
                        DataGenerare = raportEvaluare.DataGenerare
                    };

                    Context.RapoarteEvaluares.Add(raportNou);
                    Context.SaveChanges();

                    MessageBox.Show("Raportul a fost creat cu succes!", "Succes", MessageBoxButton.OK, MessageBoxImage.Information);
                }

                // adaug notificare cu privire la raportul de evaluare nou

                string Mesaj = $"Raport de evaluare NOU!";
                (new NotificariModel()).InsertNotificare(DateTime.Now, Mesaj, (new NotificariModel()).GetParinteID(elevID));
            }
            catch (Exception ex)
            {
                MessageBox.Show($"A apărut o eroare la actualizarea/inserarea raportului: {ex.Message}", "Eroare", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }

    public class RaportEvaluare : BaseModel
    {
        public string Nume {  get; set; }
        public double MediaGenerala { get; set; }
        public int AbsenteMotivate { get; set; }
        public int AbsenteNemotivate { get; set; }
        public string Comportament {  get; set; }
        public string Descriere {  get; set; }
        public DateTime DataGenerare { get; set; }  
    }

}
