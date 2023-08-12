using Oisisiproj.Model;
using Oisisiproj.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Oisisiproj.View
{
    /// <summary>
    /// Interaction logic for AddProfessor.xaml
    /// </summary>
    public partial class AddProfessor : Window, INotifyPropertyChanged
    {
        public readonly ProfesorRepository _profesorRepository;
        public readonly AdresaRepository _adresaRepository;

        private MainWindow _mainWindow;

        private string _prezime;
        public string Prezime
        {
            get { return _prezime; }
            set
            {
                _prezime = value;
                OnPropertyChanged();
            }
        }

        private string _ime;
        public string Ime
        {
            get { return _ime; }
            set
            {
                _ime = value;
                OnPropertyChanged();
            }
        }

        private DateOnly _datumRodjenja;
        public DateOnly DatumRodjenja
        {
            get { return _datumRodjenja; }
            set
            {
                _datumRodjenja = value;
                OnPropertyChanged();
            }
        }

        private string _telefon;
        public string Telefon
        {
            get { return _telefon; }
            set
            {
                _telefon = value;
                OnPropertyChanged();
            }
        }

        private string _email;
        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
                OnPropertyChanged();
            }
        }

        private Adresa _adresaStanovanja;
        public Adresa AdresaStanovanja
        {
            get { return _adresaStanovanja; }
            set
            {
                _adresaStanovanja = value;
                OnPropertyChanged();
            }
        }

        private Adresa _adresaKancelarije;
        public Adresa AdresaKancelarije
        {
            get { return _adresaKancelarije; }
            set
            {
                _adresaKancelarije = value;
                OnPropertyChanged();
            }
        }

        private string _brojLicne;
        public string BrojLicne
        {
            get { return _brojLicne; }
            set
            {
                _brojLicne = value;
                OnPropertyChanged();
            }
        }

        private string _zvanje;
        public string Zvanje
        {
            get { return _zvanje; }
            set
            {
                _zvanje = value;
                OnPropertyChanged();
            }
        }

        private int _godineStaza;
        public int GodineStaza
        {
            get { return _godineStaza; }
            set
            {
                _godineStaza = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public AddProfessor(MainWindow mainWindow)
        {
            InitializeComponent();
            DataContext = this;

            _profesorRepository = new ProfesorRepository();
            _adresaRepository = new AdresaRepository();
            _mainWindow = mainWindow;
        }

        private void PublishButton_Click(object sender, RoutedEventArgs e)
        {
            Prezime = prezimeTb.Text;
            Ime = imeTb.Text;
            DatumRodjenja = DateOnly.Parse(datumRodjenjaTb.Text);
            Telefon = telefonTb.Text;
            Email = emailTb.Text;
            BrojLicne = brojLicneTb.Text;
            Zvanje = zvanjeTb.Text;
            GodineStaza = int.Parse(godineStazaTb.Text);
            string[] adresaStanovanjaSplit = adresaSTb.Text.Split(',');
            string[] adresaKancelarijeSplit = adresaKTb.Text.Split(',');
            AdresaStanovanja = new Adresa(adresaStanovanjaSplit[0], adresaStanovanjaSplit[1], adresaStanovanjaSplit[2], adresaStanovanjaSplit[3]);
            AdresaKancelarije = new Adresa(adresaKancelarijeSplit[0], adresaKancelarijeSplit[1], adresaKancelarijeSplit[2], adresaKancelarijeSplit[3]);

            AdresaStanovanja.Id = _adresaRepository.NextId();
            _adresaRepository.Save(AdresaStanovanja);
            AdresaKancelarije.Id = _adresaRepository.NextId();
            _adresaRepository.Save(AdresaKancelarije);

            Profesor profesor = new Profesor(Prezime, Ime, DatumRodjenja, AdresaStanovanja, Telefon, Email, AdresaKancelarije, BrojLicne, Zvanje, GodineStaza);
            profesor.IdAdreseStanovanja = AdresaStanovanja.Id;
            profesor.IdAdreseKancelarije = AdresaKancelarije.Id;
            _profesorRepository.Save(profesor);

            MessageBox.Show("Profesor uspesno dodat!");
            _mainWindow.AddDataItem(profesor);
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
