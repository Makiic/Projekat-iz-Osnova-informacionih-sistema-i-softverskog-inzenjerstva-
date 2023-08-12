using Oisisiproj.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oisisiproj.Serializer;

namespace Oisisiproj.Model
{
    public class Adresa : ISerializable
    {
        public int Id { get; set; }
        public string Ulica { get; set; }
        public string Broj { get; set; }
        public string Grad { get; set; }
        public string Drzava { get; set; }

        public Adresa(string ulica, string broj, string grad, string drzava)
        {
            Ulica = ulica;
            Broj = broj;
            Grad = grad;
            Drzava = drzava;
        }

        public Adresa() { }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Ulica = values[1];
            Broj = values[2];
            Grad = values[3];
            Drzava = values[4];
        }

        public string[] ToCSV()
        {
            string[] csvValues = {
                Id.ToString(),
                Ulica,
                Broj,
                Grad,
                Drzava
                };
            return csvValues;
        }
    }
}