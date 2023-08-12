using Oisisiproj.Enums;
using Oisisiproj.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oisisiproj.Model
{
    public class Predmet : ISerializable
    {
        public int Id { get; set; }
        public string Sifra { get; set; }
        public string Naziv { get; set; }
        public Semestar Semestar { get; set; }
        public int GodinaStudija { get; set; }
        public int ESPB { get; set; }
        public int IdProfesora { get; set; }
        public Profesor Profesor { get; set; }
        public string ImePrezimeProfesora { get; set; }
        public List<Student> Polozili { get; set; }
        public List<Student> NisuPolozili { get; set; }

        public Predmet(string sifra, string naziv, Semestar semestar, int godinaStudija, int eSPB, int idProfesora)
        {
            Sifra = sifra;
            Naziv = naziv;
            Semestar = semestar;
            GodinaStudija = godinaStudija;
            ESPB = eSPB;
            Polozili = new List<Student>();
            NisuPolozili = new List<Student>();
            IdProfesora = idProfesora;
        }

        public Predmet() { }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Sifra = values[1];
            Naziv = values[2];
            Semestar = (Semestar)Enum.Parse(typeof(Semestar), values[3]);
            GodinaStudija = Convert.ToInt32(values[4]);
            ESPB = Convert.ToInt32(values[5]);
            IdProfesora = Convert.ToInt32(values[6]);
        }

        public string[] ToCSV()
        {
            string[] csvValues = {
                Id.ToString(),
                Sifra,
                Naziv,
                Semestar.ToString(),
                GodinaStudija.ToString(),
                ESPB.ToString(),
                IdProfesora.ToString()
                };
            return csvValues;
        }
    }
}
