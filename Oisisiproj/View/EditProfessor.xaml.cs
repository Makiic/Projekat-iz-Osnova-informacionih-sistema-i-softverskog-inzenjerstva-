using Oisisiproj.Model;
using Oisisiproj.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for EditProfessor.xaml
    /// </summary>
    public partial class EditProfessor : Window, INotifyPropertyChanged
    {
        public readonly ProfesorRepository _profesorRepository;
        public readonly AdresaRepository _adresaRepository;
        public readonly PredmetRepository _predmetRepository;

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

        private ObservableCollection<Predmet> _predmeti;
        public ObservableCollection<Predmet> Predmeti
        {
            get { return _predmeti; }
            set
            {
                _predmeti = value;
                OnPropertyChanged();
            }
        }

        private Predmet _selectedPredmet;
        public Predmet SelectedPredmet
        {
            get { return _selectedPredmet; }
            set
            {
                _selectedPredmet = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public EditProfessor(MainWindow mainWindow)
        {
            InitializeComponent();
            DataContext = this;

            _mainWindow = mainWindow;
            _profesorRepository = new ProfesorRepository();
            _adresaRepository = new AdresaRepository();
            _predmetRepository = new PredmetRepository();

            prezimeTb.Text = _mainWindow.SelectedProfesor.Prezime;
            imeTb.Text = _mainWindow.SelectedProfesor.Ime;
            datumRodjenjaTb.Text = _mainWindow.SelectedProfesor.DatumRodjenja.ToString();
            telefonTb.Text = _mainWindow.SelectedProfesor.Telefon;
            emailTb.Text = _mainWindow.SelectedProfesor.Email;
            brojLicneTb.Text = _mainWindow.SelectedProfesor.BrojLicne;
            zvanjeTb.Text = _mainWindow.SelectedProfesor.Zvanje;
            godineStazaTb.Text = _mainWindow.SelectedProfesor.GodineStaza.ToString();
            adresaSTb.Text = _mainWindow.SelectedProfesor.AdresaStanovanja.Ulica + "," + _mainWindow.SelectedProfesor.AdresaStanovanja.Broj
                + "," + _mainWindow.SelectedProfesor.AdresaStanovanja.Grad + "," + _mainWindow.SelectedProfesor.AdresaStanovanja.Drzava;
            adresaKTb.Text = _mainWindow.SelectedProfesor.AdresaKancelarije.Ulica + "," + _mainWindow.SelectedProfesor.AdresaKancelarije.Broj
                + "," + _mainWindow.SelectedProfesor.AdresaKancelarije.Grad + "," + _mainWindow.SelectedProfesor.AdresaKancelarije.Drzava;

            Predmeti = new ObservableCollection<Predmet>();

            Profesor selectedProfesor = _mainWindow.SelectedProfesor;
            foreach(Predmet predmet in selectedProfesor.Predmeti)
            {
                Predmeti.Add(predmet);
            }
        }

        private void PublishButton_Click(object sender, RoutedEventArgs e)
        {
            AdresaStanovanja = new Adresa(_mainWindow.SelectedProfesor.AdresaStanovanja.Ulica, _mainWindow.SelectedProfesor.AdresaStanovanja.Broj,
               _mainWindow.SelectedProfesor.AdresaStanovanja.Grad, _mainWindow.SelectedProfesor.AdresaStanovanja.Drzava);
            AdresaKancelarije = new Adresa(_mainWindow.SelectedProfesor.AdresaKancelarije.Ulica, _mainWindow.SelectedProfesor.AdresaKancelarije.Broj,
                _mainWindow.SelectedProfesor.AdresaKancelarije.Grad, _mainWindow.SelectedProfesor.AdresaKancelarije.Drzava);

            AdresaStanovanja.Id = _adresaRepository.GetAdressIdByStreetAndNumber(AdresaStanovanja.Ulica, AdresaStanovanja.Broj);
            AdresaKancelarije.Id = _adresaRepository.GetAdressIdByStreetAndNumber(AdresaKancelarije.Ulica, AdresaKancelarije.Broj);

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

            AdresaStanovanja.Ulica = adresaStanovanjaSplit[0];
            AdresaStanovanja.Broj = adresaStanovanjaSplit[1];
            AdresaStanovanja.Grad = adresaStanovanjaSplit[2];
            AdresaStanovanja.Drzava = adresaStanovanjaSplit[3];
            AdresaKancelarije.Ulica = adresaKancelarijeSplit[0];
            AdresaKancelarije.Broj = adresaKancelarijeSplit[1];
            AdresaKancelarije.Grad = adresaKancelarijeSplit[2];
            AdresaKancelarije.Drzava = adresaKancelarijeSplit[3];

            _adresaRepository.Update(AdresaStanovanja);
            _adresaRepository.Update(AdresaKancelarije);

            Profesor profesor = new Profesor(Prezime, Ime, DatumRodjenja, AdresaStanovanja, Telefon, Email, AdresaKancelarije, BrojLicne, Zvanje, GodineStaza);
            profesor.IdAdreseStanovanja = AdresaStanovanja.Id;
            profesor.IdAdreseKancelarije = AdresaKancelarije.Id;
            profesor.Id = _profesorRepository.GetProfessorIdByNumberOfIdCard(profesor.BrojLicne);

            _profesorRepository.Update(profesor);

            MessageBox.Show("Profesor uspešno izmenjen!");
            _mainWindow.EditDataItem(profesor);
            this.Close();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void DodajPredmetButton_Click(object sender, RoutedEventArgs e)
        {
            AddSubjectToProfessor addSubjectToProfessor = new AddSubjectToProfessor(this, _mainWindow);
            addSubjectToProfessor.Show();
        }

        private void UkloniPredmetButton_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedPredmet != null)
            {
                MessageBoxResult result = MessageBox.Show("Are you sure?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    SelectedPredmet.IdProfesora = 0;
                    _predmetRepository.Update(SelectedPredmet);

                    _mainWindow.SelectedProfesor.IdPredmeta.Clear();

                    foreach (Predmet predmet in _predmetRepository.GetAll())
                    {
                        if (predmet.IdProfesora == _mainWindow.SelectedProfesor.Id)
                        {
                            _mainWindow.SelectedProfesor.IdPredmeta.Add(predmet.Id);
                        }
                    }

                    _mainWindow.SelectedProfesor.Predmeti.Remove(SelectedPredmet);
                    _profesorRepository.Update(_mainWindow.SelectedProfesor);

                    MessageBox.Show("Predmet uspešno obrisan sa profesora!");

                    Predmeti.Remove(SelectedPredmet);
                    predmetiGrid.Items.Refresh();
                }
            }
            else
            {
                MessageBox.Show("Izaberite predmet koji želite da obrišete sa profesora!");
            }
        }

        public void UpdatePredmetGrid(Predmet predmet)
        {
            Predmeti.Add(predmet);
            predmetiGrid.Items.Refresh();
        }
    }
}
