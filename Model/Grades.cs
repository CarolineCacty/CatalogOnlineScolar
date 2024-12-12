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
        public int SubjectID {  get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private readonly OnlineSchoolCatalogDataContext _context;

        public Grades()
        {
            _context = new OnlineSchoolCatalogDataContext();
        }

        public ObservableCollection<Grades> getGrades(int studentID)
        {
            ObservableCollection<Grades> grades = new ObservableCollection<Grades>();

            // Presupunem că _context.grades este colecția ce conține notele din baza de date
            var studentGrades = _context.Notes.Where(g => g.ElevID == studentID);

            foreach (var item in studentGrades)
            {
                grades.Add(new Grades
                {
                    GradeID = item.NotaID,
                    Grade = item.Nota,
                    Date = item.DataNota,
                    StudentID = item.ElevID,
                    SubjectID = item.PredareID,
                });
            }

            return grades;
        }


    }
}
