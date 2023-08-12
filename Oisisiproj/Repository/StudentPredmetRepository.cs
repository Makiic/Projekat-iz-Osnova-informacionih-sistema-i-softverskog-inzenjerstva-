using Oisisiproj.Model;
using Oisisiproj.Serializer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Oisisiproj.Repository
{
    public class StudentPredmetRepository
    {
        private const string FilePath = "../../../Resources/Data/studentpredmet.csv";

        private List<StudentPredmet> _studentpredmet;

        private readonly Serializer<StudentPredmet> _serializer;

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public StudentPredmetRepository()
        {
            _serializer = new Serializer<StudentPredmet>();
            _studentpredmet = _serializer.FromCSV(FilePath);

        }

        public List<StudentPredmet> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }

        public StudentPredmet Save(StudentPredmet studentpredmet)
        {
            studentpredmet.Id = NextId();
            _studentpredmet = _serializer.FromCSV(FilePath);
            _studentpredmet.Add(studentpredmet);
            _serializer.ToCSV(FilePath, _studentpredmet);
            return studentpredmet;
        }

        public int NextId()
        {
            _studentpredmet = _serializer.FromCSV(FilePath);
            if (_studentpredmet.Count < 1)
            {
                return 1;
            }
            return _studentpredmet.Max(c => c.Id) + 1;
        }

        public void Delete(StudentPredmet studentpredmet)
        {
            _studentpredmet = _serializer.FromCSV(FilePath);
            StudentPredmet founded = _studentpredmet.Find(c => c.Id == studentpredmet.Id);
            _studentpredmet.Remove(founded);
            _serializer.ToCSV(FilePath, _studentpredmet);
        }

        public StudentPredmet Update(StudentPredmet studentpredmet)
        {
            _studentpredmet = _serializer.FromCSV(FilePath);
            StudentPredmet current = _studentpredmet.Find(c => c.Id == studentpredmet.Id);
            int index = _studentpredmet.IndexOf(current);
            _studentpredmet.Remove(current);
            _studentpredmet.Insert(index, studentpredmet);
            _serializer.ToCSV(FilePath, _studentpredmet);
            return studentpredmet;
        }


        public List<int> NadjiIdNepolozenihPredmeta(int studId)
        {
            List<int> predmetiId = new List<int>();
            foreach (StudentPredmet studentpredmet in _studentpredmet)
            {
                if (studentpredmet.StudentId == studId)
                {
                    predmetiId.Add(studentpredmet.PredmetId);
                }
            }
            return predmetiId;
        }

        public bool StudentImaPredmet(int studentId, int predmetId)
        {
            foreach (StudentPredmet studentPredmet in _studentpredmet)
            {
                if (studentPredmet.StudentId == studentId && studentPredmet.PredmetId == predmetId)
                {
                    return true; // Student ima predmet
                }
            }

            return false; // Student nema predmet
        }
        public StudentPredmet FindStudentPredmet(int studentId, int predmetId)
        {
            StudentPredmet studPred = null;
            foreach (StudentPredmet studentPredmet in _studentpredmet)
            {
                if (studentPredmet.StudentId == studentId && studentPredmet.PredmetId == predmetId)
                {
                    studPred = studentPredmet;
                }
            }
            return studPred;
        }
    }
}
