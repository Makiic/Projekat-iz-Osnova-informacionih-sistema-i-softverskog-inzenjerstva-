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
    public class AdresaRepository
    {

        private const string FilePath = "../../../Resources/Data/adrese.csv";

        private readonly Serializer<Adresa> _serializer;
        private List<Adresa> _adrese;


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }



        public AdresaRepository()
        {
            _serializer = new Serializer<Adresa>();
            _adrese = _serializer.FromCSV(FilePath);

        }


        public List<Adresa> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }

        public Adresa Save(Adresa adrese)
        {
            adrese.Id = NextId();
            _adrese = _serializer.FromCSV(FilePath);
            _adrese.Add(adrese);
            _serializer.ToCSV(FilePath, _adrese);
            return adrese;
        }

        public int NextId()
        {
            _adrese = _serializer.FromCSV(FilePath);
            if (_adrese.Count < 1)
            {
                return 1;
            }
            return _adrese.Max(c => c.Id) + 1;
        }

        public void Delete(Adresa adrese)
        {
            _adrese = _serializer.FromCSV(FilePath);
            Adresa founded = _adrese.Find(c => c.Id == adrese.Id);
            _adrese.Remove(founded);
            _serializer.ToCSV(FilePath, _adrese);
        }

        public Adresa Update(Adresa adrese)
        {
            _adrese = _serializer.FromCSV(FilePath);
            Adresa current = _adrese.Find(c => c.Id == adrese.Id);
            int index = _adrese.IndexOf(current);
            _adrese.Remove(current);
            _adrese.Insert(index, adrese);
            _serializer.ToCSV(FilePath, _adrese);
            return adrese;
        }

        public int GetAdressIdByStreetAndNumber(string ulica, string broj)
        {
            foreach (Adresa adresa in _serializer.FromCSV(FilePath))
            {
                if (adresa.Ulica == ulica && adresa.Broj == broj)
                {
                    return adresa.Id;
                }
            }
            return 0;
        }
    }
}
