using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oisisiproj.Serializer;

namespace Oisisiproj.Model
{
    public class Katedra : ISerializable
    {
        public int Id { get; set; }
        public string SifraKatedre { get; set; }
        public string NazivKatedre { get; set; }
        public int IdSefKatedre { get; set; }
        public Profesor SefKatedre { get; set; }
        public List<Profesor> Profesori { get; set; }

        public Katedra()
        {
            Profesori = new List<Profesor>();
        }

        public Katedra(int id, string sifraKatedre, string nazivKatedre, int idSefKatedre)
        {
            Id = id;
            SifraKatedre = sifraKatedre;
            NazivKatedre = nazivKatedre;
            IdSefKatedre = idSefKatedre;
            Profesori = new List<Profesor>();
        }


        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                SifraKatedre,
                NazivKatedre,
                IdSefKatedre.ToString(),
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            SifraKatedre = values[1];
            NazivKatedre = values[2];
            IdSefKatedre = int.Parse(values[3]);
        }
    }
}
