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
    public class OcenaRepository
    {


        private const string FilePath = "../../../Resources/Data/ocene.csv";

        private readonly Serializer<Ocena> _serializer;
        private List<Ocena> _ocene;


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }



        public OcenaRepository()
        {
            _serializer = new Serializer<Ocena>();
            _ocene = _serializer.FromCSV(FilePath);

        }


        public List<Ocena> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }

        public Ocena Save(Ocena ocene)
        {
            ocene.Id = NextId();
            _ocene = _serializer.FromCSV(FilePath);
            _ocene.Add(ocene);
            _serializer.ToCSV(FilePath, _ocene);
            return ocene;
        }

        public int NextId()
        {
            _ocene = _serializer.FromCSV(FilePath);
            if (_ocene.Count < 1)
            {
                return 1;
            }
            return _ocene.Max(c => c.Id) + 1;
        }

        public void Delete(Ocena ocene)
        {
            _ocene = _serializer.FromCSV(FilePath);
            Ocena founded = _ocene.Find(c => c.Id == ocene.Id);
            _ocene.Remove(founded);
            _serializer.ToCSV(FilePath, _ocene);
        }

        public Ocena Update(Ocena ocene)
        {
            _ocene = _serializer.FromCSV(FilePath);
            Ocena current = _ocene.Find(c => c.Id == ocene.Id);
            int index = _ocene.IndexOf(current);
            _ocene.Remove(current);
            _ocene.Insert(index, ocene);
            _serializer.ToCSV(FilePath, _ocene);
            return ocene;
        }
        public List<int> NadjiIdNepolozenihPredmeta(int studId)
        {
            List<int> predmetiId = new List<int>();
            foreach (Ocena ocena in _ocene)
            {
                if (ocena.StudentId == studId)
                {
                    predmetiId.Add(ocena.PredmetId);
                }
            }
            return predmetiId;
        }
    }
}

