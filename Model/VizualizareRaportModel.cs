using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;

namespace CatalogScolarOnline.Model
{
    public class VizualizareRaportModel : BaseModel
    {
        public RaportEvaluare GetRaportEvaluare(int elevID)
        {
            RapoarteEvaluare rapoarteEvaluare =
                (from r in Context.RapoarteEvaluares
                where r.ElevID == elevID
                select r).FirstOrDefault();

            RaportEvaluare raportEvaluare = new RaportEvaluare()
            {
                Nume = Context.Elevis.Where(e => e.ElevID == rapoarteEvaluare.ElevID).Select(e => e.Nume + " " + e.Prenume).FirstOrDefault(),
                MediaGenerala = rapoarteEvaluare.MediaGenerala,
                AbsenteMotivate = rapoarteEvaluare.AbsenteMotivate,
                AbsenteNemotivate = rapoarteEvaluare.AbsenteNemotivate,
                Comportament = rapoarteEvaluare.Comportament,
                Descriere = rapoarteEvaluare.Descriere,
                DataGenerare = rapoarteEvaluare.DataGenerare
            };

            return raportEvaluare;
        }
    }
}
