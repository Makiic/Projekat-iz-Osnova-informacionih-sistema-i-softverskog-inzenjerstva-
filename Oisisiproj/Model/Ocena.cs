using Oisisiproj.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oisisiproj.Serializer;

namespace Oisisiproj.Model
{
    public class Ocena : ISerializable
    {
        public int Id { get; set; } 
        public int Vrednost { get; set; }
        public DateTime DatumPolaganja { get; set; }    
        public int StudentId { get; set; }    
        public int  PredmetId { get; set; }

        public Ocena() { }


        public Ocena( int vrednost, DateTime datumPolaganja, int studentId, int predmetId)
        {
           
            Vrednost = vrednost;
            DatumPolaganja = datumPolaganja;
            StudentId = studentId;
            PredmetId = predmetId;
        }   

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            Vrednost=int.Parse(values[1]);
            DatumPolaganja =DateTime.Parse(values[2]);  
            StudentId= int.Parse(values[3]);
            PredmetId = int.Parse(values[4]);
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                Vrednost.ToString(),
                DatumPolaganja.ToString(),
                StudentId.ToString(),
                PredmetId.ToString()
            };

            return csvValues;
        }
    }
}
