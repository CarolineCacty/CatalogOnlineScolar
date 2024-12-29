using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CatalogScolarOnline.Utilities;

namespace CatalogScolarOnline.Model
{
    public  class NotificariModel : BaseModel
    {
        public void InsertNotificare(DateTime data, string mesaj,int parinteID)
        {
            Notificari notificari = new Notificari
            {
                Data_Notificare = DateTime.Now,
                Mesaj = mesaj,
                ParinteID = parinteID,
                EsteCitita = false
            };
            Context.Notificaris.InsertOnSubmit(notificari);
            Context.SubmitChanges();
        }

        public int GetParinteID(int elevID)
        {
            return Context.Elevis.Where(e => e.ElevID == elevID).Select(e => e.ParinteID).FirstOrDefault();
        }

        public string GetMaterie(int predareID)
        {
            return Context.Predares
                                    .Join(Context.Materiis,
                                          predare => predare.MaterieID,
                                          materie => materie.MaterieID,
                                          (predare, materie) => new { predare, materie })
                                    .Where(p => p.predare.PredareID == predareID)
                                    .Select(p => p.materie.Nume_materie)
                                    .FirstOrDefault();
        }



        private DateTime _dataNotificare;

        private bool _esteCitita;

        private int _parinteID;

        private string _mesaj;

        public string Mesaj { get; set; }

        public DateTime DataNotificare { get; set; }
        public bool EsteCitita { get; set; }
        public int ParinteID { get; set; }

        public bool NotEsteCitita { get; set; }

        public ObservableCollection<NotificariModel> GetNotificariList(int parinteID)
        {
            ObservableCollection<NotificariModel> notificariModel = new ObservableCollection<NotificariModel>();

            var list = Context.Notificaris.Where(a => a.ParinteID == parinteID).ToList();
            if (list == null)
                throw new ArgumentNullException("Nu exista parintele respectiv!\n");

            int notificareID = Context.Notificaris.Select(n => n.NotificareID).FirstOrDefault();

            foreach ( var item in list.AsEnumerable().Reverse())
            {
                notificariModel.Add
                (
                    new NotificariModel
                    {
                        Mesaj = item.Mesaj,
                        DataNotificare = item.Data_Notificare,
                        EsteCitita = item.EsteCitita,
                        NotEsteCitita = !item.EsteCitita,
                        ParinteID = item.ParinteID,
                    }
                );
            }

            return notificariModel;
        }

        public int GetNrNotificariNoi()
        {
            int parinteID = GetParinteID(Session.GetElevId());
            return Context.Notificaris.Where(p => p.ParinteID == parinteID && p.EsteCitita == false).Count();
        }
    }
}
