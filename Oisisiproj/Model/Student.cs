using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oisisiproj.Enums;
using Oisisiproj.Serializer;

namespace Oisisiproj.Model
{
    public class Student : ISerializable
    {
        public int Id { get; set; }
        public string Prezime { get; set; }
        public string Ime { get; set; }
        public DateOnly DatumRodjenja { get; set; }
        public int IdAdreseStanovanja { get; set; }
        public Adresa AdresaStanovanja { get; set; }
        public string Telefon { get; set; }
        public string Email { get; set; }
        public string Indeks { get; set; }
        public int GodinaUpisa { get; set; }
        public int GodinaStudija { get; set; }
        public Status Status { get; set; }
        public float ProsecnaOcena { get; set; }
        public int BrojEspbStudenta { get; set; }
        public List<Ocena> Polozeni { get; set; }
        public List<Predmet> Nepolozeni { get; set; }

        public Student()
        {
            Polozeni= new List<Ocena>();
            Nepolozeni = new List<Predmet>();
        }

        public Student( string prezime, string ime, DateOnly datumRodjenja, Adresa adresaStanovanja, string telefon, string email, string indeks, int godinaUpisa, int godinaStudija, Status status, float prosecnaOcena,int brojEspbStudenta)
          { 
            Prezime = prezime;
            Ime = ime;
            DatumRodjenja = datumRodjenja;
            AdresaStanovanja = adresaStanovanja;
            Telefon = telefon;
            Email = email;
            Indeks = indeks;
            GodinaUpisa = godinaUpisa;
            GodinaStudija = godinaStudija;
            Status = status;
            ProsecnaOcena = prosecnaOcena;
            BrojEspbStudenta = brojEspbStudenta;
             Polozeni = new List<Ocena>();
            Nepolozeni = new List<Predmet>();

        }

        public Student(float prosecnaOcena, int brojEspbStudenta)
        {
            ProsecnaOcena = prosecnaOcena;
            BrojEspbStudenta = brojEspbStudenta;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Prezime = values[1];
            Ime = values[2];
            DatumRodjenja = DateOnly.Parse(values[3]);
            IdAdreseStanovanja = Convert.ToInt32(values[4]);
            Telefon = values[5];
            Email = values[6];
            Indeks = values[7];
            GodinaUpisa=Convert.ToInt32(values[8]); 
            GodinaStudija=Convert.ToInt32(values[9]);
            Status = (Status)Enum.Parse(typeof(Status), values[10]);
            ProsecnaOcena = float.Parse(values[11]);
        }

        public string[] ToCSV()
        {
            string[] csvValues = {
                Id.ToString(),
                Prezime,
                Ime,
                DatumRodjenja.ToString(),
                 IdAdreseStanovanja.ToString(),
                Telefon,
                Email,
                Indeks,
                GodinaUpisa.ToString(),
                GodinaStudija.ToString(),
                Status.ToString(),
                ProsecnaOcena.ToString(),
            };
            return csvValues;
        }

       

    }
}
