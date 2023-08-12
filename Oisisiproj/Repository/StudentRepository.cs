using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oisisiproj.Serializer;
using Oisisiproj.Model;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Oisisiproj.Repository
{
    public class StudentRepository
    {

        private const string FilePath = "../../../Resources/Data/studenti.csv";



        private readonly Serializer<Student> _serializer;
        private List<Student> _student;


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }



        public StudentRepository()
        {
            _serializer = new Serializer<Student>();
            _student = _serializer.FromCSV(FilePath);

        }


        public List<Student> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }

        public Student Save(Student studenti)
        {
            studenti.Id = NextId();
            _student = _serializer.FromCSV(FilePath);
            _student.Add(studenti);
            _serializer.ToCSV(FilePath, _student);
            return studenti;
        }

        public int NextId()
        {
            _student = _serializer.FromCSV(FilePath);
            if (_student.Count < 1)
            {
                return 1;
            }
            return _student.Max(c => c.Id) + 1;
        }

        public void Delete(Student studenti)
        {
            _student = _serializer.FromCSV(FilePath);
            Student founded = _student.Find(c => c.Id == studenti.Id);
            _student.Remove(founded);
            _serializer.ToCSV(FilePath, _student);
        }

        public Student Update(Student studenti)
        {
            _student = _serializer.FromCSV(FilePath);
            Student current = _student.Find(c => c.Id == studenti.Id);

                int index = _student.IndexOf(current);
                _student.Remove(current);
                _student.Insert(index, studenti);
                _serializer.ToCSV(FilePath, _student);
                return studenti;
          
          
        }
        public int GetIdByIndeks(string indeks)
        {
            foreach (Student student in _serializer.FromCSV(FilePath))
            {
                if (student.Indeks == indeks)
                {
                    return student.Id;
                }
            }
            return 0;
        }
        public List<Predmet> ProveriOcene(int studentId)
        {
            List<Ocena> ocene = new List<Ocena>();
            List<Predmet> predmeti = new List<Predmet>();
            foreach (Predmet predmet in predmeti)
            {
                foreach (Ocena ocena in ocene)
                {
                    if (ocena.StudentId == studentId && ocena.Vrednost > 5 && ocena.PredmetId == predmet.Id)
                    {
                        predmeti.Add(predmet);
                    }
                }
            }


            return predmeti;
        }

        public float PostaviProsecnuOcenu(int id)
        {
            float prosOcena = 0;
            foreach (Student stud in _student)
            {
                if (stud.Id == id)
                {
                    prosOcena = stud.ProsecnaOcena;
                }
            }
            return prosOcena;
        }

        public int PostaviEspb(int id)
        {
            int espb = 0;
            foreach (Student stud in _student)
            {
                if (stud.Id == id)
                {
                    espb = stud.BrojEspbStudenta;
                }
            }
            return espb;
        }
        public void IzmeniProsOcenuIEspb(int id, float prosecnaOcena, int brojEspb)
        {
            Student student = null;
            foreach (Student stud in _student)
            {
                if (stud.Id == id)
                {
                    student = stud;
                    break;
                }
            }

            if (student != null)
            {
                student.ProsecnaOcena = prosecnaOcena;
                student.BrojEspbStudenta = brojEspb;
            }
        }

        public int TrenutnaGodStudija(Student student)
        {
            int trenGodina = 0;
            foreach (Student stud in _student)
            {
                if (stud.Id == student.Id)
                {
                    trenGodina = stud.GodinaStudija;
                }
            }
            return trenGodina;
        }
    }

}

