using Oisisiproj.Model;
using Oisisiproj.Serializer;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Oisisiproj.Repository
{
    public class ProfesorRepository
    {
        private const string FilePath = "../../../Resources/Data/profesori.csv";

        private List<Profesor> _profesori;
        private readonly Serializer<Profesor> _serializer;
        public readonly KatedraRepository _katedraRepository;

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ProfesorRepository()
        {
            _serializer = new Serializer<Profesor>();
            _profesori = _serializer.FromCSV(FilePath);
            _katedraRepository = new KatedraRepository();

        }

        public List<Profesor> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }

        public Profesor Save(Profesor profesori)
        {
            profesori.Id = NextId();
            _profesori = _serializer.FromCSV(FilePath);
            _profesori.Add(profesori);
            _serializer.ToCSV(FilePath, _profesori);
            return profesori;
        }

        public int NextId()
        {
            _profesori = _serializer.FromCSV(FilePath);
            if (_profesori.Count < 1)
            {
                return 1;
            }
            return _profesori.Max(c => c.Id) + 1;
        }

        public void Delete(Profesor profesori)
        {
            _profesori = _serializer.FromCSV(FilePath);
            Profesor founded = _profesori.Find(c => c.Id == profesori.Id);
            _profesori.Remove(founded);
            _serializer.ToCSV(FilePath, _profesori);
        }

        public Profesor Update(Profesor profesori)
        {
            _profesori = _serializer.FromCSV(FilePath);
            Profesor current = _profesori.Find(c => c.Id == profesori.Id);
            int index = _profesori.IndexOf(current);
            _profesori.Remove(current);
            _profesori.Insert(index, profesori);
            _serializer.ToCSV(FilePath, _profesori);
            return profesori;
        }

        public int GetProfessorIdByNumberOfIdCard(string idCardNumber)
        {
            foreach (Profesor profesor in _serializer.FromCSV(FilePath))
            {
                if (profesor.BrojLicne == idCardNumber)
                {
                    return profesor.Id;
                }
            }
            return 0;
        }
        public Profesor GetById(int profesorId)
        {
            foreach (Profesor profesor in _profesori)
            {
                if (profesor.Id == profesorId)
                {
                    return profesor;
                }
            }

            return null;
        }

        public Profesor FindProfessorByDepartmentId()
        {
            List<Katedra> katedre = new List<Katedra>();
            foreach (Profesor profesor in GetAll())
            {
                foreach (Katedra katedra in _katedraRepository.GetAll())
                {


                    if (katedra.IdSefKatedre == profesor.Id)
                    {
                        return profesor;
                    }
                }
            }

            return null;
        }



    }
}
