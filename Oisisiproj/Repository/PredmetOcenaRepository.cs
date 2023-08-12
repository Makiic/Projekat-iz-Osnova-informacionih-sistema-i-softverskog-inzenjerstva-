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
    public class PredmetOcenaRepository
    {
        private const string FilePath = "../../../Resources/Data/predmetOcena.csv";

        private readonly Serializer<PredmetOcena> _serializer;
        private List<PredmetOcena> _predmetOcena;


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }



        public PredmetOcenaRepository()
        {
            _serializer = new Serializer<PredmetOcena>();
            _predmetOcena = _serializer.FromCSV(FilePath);

        }


        public List<PredmetOcena> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }

        public PredmetOcena Save(PredmetOcena predmetOcena)
        {
            predmetOcena.Id = NextId();
            _predmetOcena = _serializer.FromCSV(FilePath);
            _predmetOcena.Add(predmetOcena);
            _serializer.ToCSV(FilePath, _predmetOcena);
            return predmetOcena;
        }

        public int NextId()
        {
            _predmetOcena = _serializer.FromCSV(FilePath);
            if (_predmetOcena.Count < 1)
            {
                return 1;
            }
            return _predmetOcena.Max(c => c.Id) + 1;
        }

        public void Delete(PredmetOcena predmetOcena)
        {
            if (_predmetOcena == null)
            {
                _predmetOcena = _serializer.FromCSV(FilePath);
            }
            PredmetOcena founded = _predmetOcena.Find(c => c.Id == predmetOcena.Id);
            _predmetOcena.Remove(founded);
            _serializer.ToCSV(FilePath, _predmetOcena);
        }


        public PredmetOcena Update(PredmetOcena predmetOcena)
        {
            _predmetOcena = _serializer.FromCSV(FilePath);
            PredmetOcena current = _predmetOcena.Find(c => c.Id == predmetOcena.Id);
            int index = _predmetOcena.IndexOf(current);
            _predmetOcena.Remove(current);
            _predmetOcena.Insert(index, predmetOcena);
            _serializer.ToCSV(FilePath, _predmetOcena);
            return predmetOcena;
        }
        

        public List<PredmetOcena> NadjiPolozenePredmete(int studId)
        {
            List<PredmetOcena> predmeti = new List<PredmetOcena>();
            foreach (PredmetOcena predOcena in _predmetOcena)
            {
                if (predOcena.StudentId == studId)
                {
                    predmeti.Add(predOcena);
                }
            }
            return predmeti;
        }
        public float IzracunajProsecnu(int studID)
        {
            float sum = 0;
            int count = 0;

            foreach (PredmetOcena predOcena in _predmetOcena)
            {
                if (predOcena.StudentId == studID)
                {
                    sum += predOcena.BrojcanaVrednost;
                    count++;
                }
            }

            float average = 0;
            if (count > 0)
                average = sum / count;

            return average;
        }

        public int IzracunajESPB(int studID)
        {
            int sum = 0;


            List<PredmetOcena> predmeti = new List<PredmetOcena>();
            foreach (PredmetOcena predOcena in _predmetOcena)
            {
                if (predOcena.StudentId == studID)
                {
                    sum += predOcena.BrojESPB;
                }


            }
          
            return sum;
        }
        public float IzracunajProsecna()
        {
            float sum = 0;
            int count = 0;
            List<Student> studenti = new List<Student>();
            foreach (Student student in studenti)
            {
                foreach (PredmetOcena predOcena in _predmetOcena)
                {
                    if (predOcena.StudentId == student.Id)
                    {
                        sum += predOcena.BrojcanaVrednost;
                        count++;
                    }
                }
            }

            float average = 0;
            if (count > 0)
                average = sum / count;

            return average;
        }

    }
}
