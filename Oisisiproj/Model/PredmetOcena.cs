using System;
using System.Collections.Generic;
using System.Text;
using Oisisiproj.Serializer;

namespace Oisisiproj.Model
{
    public class PredmetOcena : ISerializable
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int PredmetId { get; set; }
        public string SifraPredmeta { get; set; }
        public string NazivPredmeta { get; set; }

        public int BrojESPB { get; set; }
        public int BrojcanaVrednost { get; set; }
        public DateTime DatumPolaganja { get; set; }

        public PredmetOcena() { }
        public PredmetOcena(int studentId, int predmetId, string sifraPredmeta, string nazivPredmeta, int brojESPB, int brojcanaVrednost, DateTime datum)
        {
            StudentId = studentId;
            PredmetId = predmetId;
            SifraPredmeta = sifraPredmeta;
            NazivPredmeta = nazivPredmeta;
            BrojESPB = brojESPB;
            BrojcanaVrednost = brojcanaVrednost;
            DatumPolaganja = datum;
        }

  
        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                StudentId.ToString(),
                PredmetId.ToString(),
                SifraPredmeta,
                NazivPredmeta,
                BrojESPB.ToString(),
                BrojcanaVrednost.ToString(),
                DatumPolaganja.ToString("dd/MM/yyyy")
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            StudentId = int.Parse(values[1]);
            PredmetId = int.Parse(values[2]);
            SifraPredmeta = values[3];
            NazivPredmeta = values[4];
            BrojESPB = int.Parse(values[5]);
            BrojcanaVrednost = int.Parse(values[6]);
            DatumPolaganja = DateTime.Parse(values[7]);
        }
    }
}
