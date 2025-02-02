using CatalogScolarOnline.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogScolarOnline.Model
{
    public class Grades : INotifyPropertyChanged
    {   
        public int GradeID { get; set; }
        public decimal Grade {  get; set; }
        public DateTime Date { get; set; }
        public int StudentID { get; set; }
        public string Subject {  get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private readonly Online_School_CatalogEntities _context;

        public Grades()
        {
            _context = new Online_School_CatalogEntities();
        }

        public ObservableCollection<Grades> getGrades(int studentID)
        {
            ObservableCollection<Grades> grades = new ObservableCollection<Grades>();

            var studentGrades = _context.Notes.Where(g => g.ElevID == studentID);

            foreach (var item in studentGrades.AsEnumerable().Reverse())
            {
                string materie =
                    (from n in _context.Notes
                    join p in _context.Predares on n.PredareID equals p.PredareID
                    join m in _context.Materiis on p.MaterieID equals m.MaterieID
                     where p.PredareID == item.PredareID
                     select m.Nume_materie).FirstOrDefault();


                grades.Add(new Grades
                {
                    GradeID = item.NotaID,
                    Grade = item.Nota,
                    Date = item.DataNota,
                    StudentID = item.ElevID,
                    Subject = materie
                });
            }

            return grades;
        }

        public int GetElevID(string email)
        {
            if(Session.GetRol() == 0)
            {
                var elevID =
                (from user in _context.Utilizatoris
                 join e in _context.Elevis on user.UtilizatorID equals e.UtilizatorID
                 where user.Email == email
                 select e.ElevID).FirstOrDefault();

                StudentID = Convert.ToInt32(elevID);
                return StudentID;
            }
            else if(Session.GetRol() == 1)
            {
                var elevID =
                (from user in _context.Utilizatoris
                 join p in _context.Parintis on user.UtilizatorID equals p.UtilizatorID
                 join e in _context.Elevis on p.ParinteID equals e.ParinteID
                 where user.Email == email
                 select e.ElevID).FirstOrDefault();

                StudentID = Convert.ToInt32(elevID);
                return StudentID;
            }
            return StudentID;
        }
    }
}
