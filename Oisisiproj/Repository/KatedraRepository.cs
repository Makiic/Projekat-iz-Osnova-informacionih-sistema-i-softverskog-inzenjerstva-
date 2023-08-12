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
    public class KatedraRepository
    {
        private const string FilePath = "../../../Resources/Data/katedre.csv";

        private List<Katedra> _katedre;
        private readonly Serializer<Katedra> _serializer;
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public KatedraRepository()
        {
            _katedre = new List<Katedra>();
            _serializer = new Serializer<Katedra>();
        }

        public List<Katedra> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }

        public Katedra Save(Katedra katedra)
        {
            katedra.Id = NextId();
            _katedre = _serializer.FromCSV(FilePath);
            _katedre.Add(katedra);
            _serializer.ToCSV(FilePath, _katedre);
            return katedra;
        }

        public int NextId()
        {
            _katedre = _serializer.FromCSV(FilePath);
            if (_katedre.Count < 1)
            {
                return 1;
            }
            return _katedre.Max(c => c.Id) + 1;
        }

        public void Delete(Katedra katedra)
        {
            _katedre = _serializer.FromCSV(FilePath);
            Katedra founded = _katedre.Find(c => c.Id == katedra.Id);
            _katedre.Remove(founded);
            _serializer.ToCSV(FilePath, _katedre);
        }

        public Katedra Update(Katedra katedra)
        {
            _katedre = _serializer.FromCSV(FilePath);
            Katedra current = _katedre.Find(c => c.Id == katedra.Id);
            int index = _katedre.IndexOf(current);
            _katedre.Remove(current);
            _katedre.Insert(index, katedra);
            _serializer.ToCSV(FilePath, _katedre);
            return katedra;
        }

        public int NadjiIdSefaKatedre(int katId)
        {
            int sefId = 0;
            foreach (Katedra katedra in _katedre)
            {
                if (katedra.Id == katId)
                {
                    sefId = katedra.IdSefKatedre;
                }
            }
            return sefId;
        }

        public Katedra FindByName(string name)
        {
            return _katedre.FirstOrDefault(katedra => string.Equals(katedra.NazivKatedre, name, StringComparison.OrdinalIgnoreCase));
        }

        public void ChangeSefKatedre(int katId, int professorId)
        {
            Katedra katedra = _katedre.FirstOrDefault(c => c.Id == katId);
            if (katedra != null)
            {
                katedra.IdSefKatedre = professorId;
                _serializer.ToCSV(FilePath, _katedre);
            }
        }
    }
}
