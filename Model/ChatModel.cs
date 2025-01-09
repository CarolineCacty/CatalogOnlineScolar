using CatalogScolarOnline.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace CatalogScolarOnline.Model
{
    public class ChatModel : BaseModel
    {
        public int MesajID { get; set; }
        public DateTime DataTrimitere { get; set; }
        public string Continut { get; set; }
        public int ExpeditorID { get; set; }
        public int DestinatarID { get; set; }
        public bool EsteCitit { get; set; }

        public bool EsteDeLaDiriginte { get; set; }

        public string Expeditor {  get; set; }

        public List<string> GetParintiList()
        {
            List<string> parinteList = new List<string>();
            var list =
                (from e in Context.Elevis
                 where e.ClasaID == Session.GetClasaID()
                 select e.ParinteID);

            foreach (int item in list)
            {
                string itemStr =
                    (from p in Context.Parintis
                     where p.ParinteID == item
                     select p.Nume_parinte + " " + p.Prenume_parinte).FirstOrDefault();
                parinteList.Add(itemStr);
            }

            return parinteList;
        }

        public ObservableCollection<ChatModel> GetMesaje(int destinatarID,string clasaID)
        {
            ObservableCollection<ChatModel> Mesaje = new ObservableCollection<ChatModel>();

            int rol = Session.GetRol();

            List<Mesaje> list = new List<Mesaje>();

            if(rol == 2)
                list = Context.Mesajes.Where(m => (m.DestinatarID == destinatarID || m.DestinatarID == Session.UtilizatorID) && (m.ExpeditorID == Session.UtilizatorID || m.ExpeditorID == destinatarID )).ToList();
            else 
                list = Context.Mesajes.Where(m => (m.DestinatarID == GetUserIDByDiriginiteID(clasaID) || m.DestinatarID == Session.UtilizatorID) && m.ExpeditorID == Session.UtilizatorID || m.ExpeditorID == destinatarID ).ToList();

            foreach (var item in list)
            {
                bool ok = GetRolByExpeditorID(item.ExpeditorID) == 1 ? false : true;
                Mesaje.Add(
                    new ChatModel
                    {
                        MesajID = item.MesajID,
                        DataTrimitere = item.DataTrimitere,
                        Continut = item.Continut,
                        ExpeditorID = item.ExpeditorID,
                        DestinatarID = item.DestinatarID,
                        EsteCitit = item.EsteCitit,
                        EsteDeLaDiriginte = ok,
                        Expeditor = GetExpeditorName(item.ExpeditorID),
                    });
            }

            return Mesaje;  
        }

        internal int GetDestinatarIDByParinteSelectat(string parinteSelectat)
        {
            var parts = parinteSelectat.Split(' ');
            if (parts.Length < 2)
            {
                throw new ArgumentException("Stringul parinteSelectat trebuie să conțină atât numele, cât și prenumele.");
            }
            string nume = parts[0];
            string prenume = parts[1];

            int destID =
                (from p in Context.Parintis
                 where p.Nume_parinte == nume && p.Prenume_parinte == prenume
                 select p.UtilizatorID).FirstOrDefault();

            return destID;
        }

        internal string GetDiriginteByClasaID(string clasaID)
        {
            int diriginteID = GetDiriginteID(clasaID);
            return "Diriginte: " + Context.Profesoris.Where(p => p.ProfesorID == diriginteID).Select(p => p.Nume + " " + p.Prenume).FirstOrDefault().ToString();
        }

        internal int GetDiriginteID(string clasaID)
        {
            int diriginteID = Context.Clases.Where(c => c.ClasaID == clasaID).Select(c => c.Diriginte).FirstOrDefault();
            return diriginteID;
        }

        internal int GetUserIDByDiriginiteID(string clasaID)
        {
            int diriginteID = GetDiriginteID(clasaID);
            return Context.Profesoris.Where(p => p.ProfesorID == diriginteID).Select(p => p.UtilizatorID).FirstOrDefault();
        }

        public int GetRolByExpeditorID(int expeditorID)
        {
            return Context.Utilizatoris.Where(u => u.UtilizatorID == expeditorID).Select(u=>u.Rol).FirstOrDefault();
        }

        public string GetExpeditorName(int expeditorID)
        {
            int rol = GetRolByExpeditorID(expeditorID);

            if(rol == 1)
            {
                return Context.Parintis.Where(p => p.UtilizatorID == expeditorID).Select(p => p.Nume_parinte + " " + p.Prenume_parinte).FirstOrDefault().ToString();
            }
            else
            {
                return Context.Profesoris.Where(p => p.UtilizatorID == expeditorID).Select(p => p.Nume + " " + p.Prenume).FirstOrDefault().ToString();
            }
        }

    }
}
