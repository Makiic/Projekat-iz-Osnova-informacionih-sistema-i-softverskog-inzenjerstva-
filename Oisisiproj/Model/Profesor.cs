using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oisisiproj.Serializer;

namespace Oisisiproj.Model
{
    public class Profesor: ISerializable
    {
        public int Id { get; set; }
        public string Prezime { get; set; }
        public string Ime { get; set; }
        public DateOnly DatumRodjenja { get; set; }
        public int IdAdreseStanovanja { get; set; }
        public Adresa AdresaStanovanja { get; set; }
        public string Telefon { get; set; }
        public string Email { get; set; }
        public int IdAdreseKancelarije { get; set; }
        public Adresa AdresaKancelarije { get; set; }
        public string BrojLicne { get; set; }
        public string Zvanje { get; set; }
        public int GodineStaza { get; set; }
        public List<int> IdPredmeta { get; set; }
        public List<Predmet> Predmeti { get; set; }

        public Profesor(string prezime, string ime, DateOnly datumRodjenja, Adresa adresaStanovanja, string telefon, string email, Adresa adresaKancelarije, string brojLicne, string zvanje, int godineStaza)
        {
            Prezime = prezime;
            Ime = ime;
            DatumRodjenja = datumRodjenja;
            AdresaStanovanja = adresaStanovanja;
            Telefon = telefon;
            Email = email;
            AdresaKancelarije = adresaKancelarije;
            BrojLicne = brojLicne;
            Zvanje = zvanje;
            GodineStaza = godineStaza;
            IdPredmeta = new List<int>();
            IdPredmeta.Add(0);
            Predmeti = new List<Predmet>();
        }

        public Profesor()
        {
            Predmeti = new List<Predmet>();
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
            IdAdreseKancelarije = Convert.ToInt32(values[7]);
            BrojLicne = values[8];
            Zvanje = values[9];
            GodineStaza = Convert.ToInt32(values[10]);
            List<string> stringList = values[11].Split(',').ToList();
            List<int> intList = stringList.Select(int.Parse).ToList();
            IdPredmeta = intList;
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
                IdAdreseKancelarije.ToString(),
                BrojLicne,
                Zvanje,
                GodineStaza.ToString(),
                string.Join(',', IdPredmeta)
                };
            return csvValues;
        }
    }
}
