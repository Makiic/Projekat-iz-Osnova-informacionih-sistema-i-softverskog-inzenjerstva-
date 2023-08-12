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
    public class PredmetRepository
    {
        private const string FilePath = "../../../Resources/Data/predmeti.csv";

        private List<Predmet> _predmeti;

        private readonly Serializer<Predmet> _serializer;

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public PredmetRepository()
        {
            _serializer = new Serializer<Predmet>();
            _predmeti = _serializer.FromCSV(FilePath);

        }

        public List<Predmet> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }

        public Predmet Save(Predmet predmeti)
        {
            predmeti.Id = NextId();
            _predmeti = _serializer.FromCSV(FilePath);
            _predmeti.Add(predmeti);
            _serializer.ToCSV(FilePath, _predmeti);
            return predmeti;
        }

        public int NextId()
        {
            _predmeti = _serializer.FromCSV(FilePath);
            if (_predmeti.Count < 1)
            {
                return 1;
            }
            return _predmeti.Max(c => c.Id) + 1;
        }

        public void Delete(Predmet predmet)
        {
            if (predmet == null)
            {
                // Throw an exception, log an error, or handle the null case as needed
                return;
            }

            if (_predmeti == null)
            {
                _predmeti = _serializer.FromCSV(FilePath);
            }

            Predmet founded = _predmeti.Find(c => c.Id == predmet.Id);
            if (founded != null)
            {
                _predmeti.Remove(founded);
                _serializer.ToCSV(FilePath, _predmeti);
            }
        }

        public Predmet Update(Predmet predmeti)
        {
            _predmeti = _serializer.FromCSV(FilePath);
            Predmet current = _predmeti.Find(c => c.Id == predmeti.Id);
            int index = _predmeti.IndexOf(current);
            _predmeti.Remove(current);
            _predmeti.Insert(index, predmeti);
            _serializer.ToCSV(FilePath, _predmeti);
            return predmeti;
        }

        public int GetSujbectIdByCode(string code)
        {
            foreach (Predmet predmet in _serializer.FromCSV(FilePath))
            {
                if (predmet.Sifra == code)
                {
                    return predmet.Id;
                }
            }
            return 0;
        }
        public Predmet GetById(int id)
        {
            List<Predmet> predmeti = _serializer.FromCSV(FilePath);

            return predmeti.FirstOrDefault(predmet => predmet.Id == id);
        }

    }
}
