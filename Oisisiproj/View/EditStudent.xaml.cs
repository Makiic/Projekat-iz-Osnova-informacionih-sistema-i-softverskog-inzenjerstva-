using Oisisiproj.Enums;
using Oisisiproj.Model;
using Oisisiproj.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for EditStudent.xaml
    /// </summary>
    public partial class EditStudent : Window,IDataErrorInfo,INotifyPropertyChanged
    {

        private MainWindow _mainWindow;
        private readonly StudentPredmetRepository _studentPredmetRepository;
        private readonly StudentRepository _studentRepository;
        private readonly AdresaRepository _adresaRepository;
        private readonly PredmetOcenaRepository _predmetOcenaRepository;
        private readonly PredmetRepository _predmetRepository;
        public List<Predmet> Predmeti { get; set; }
       
        public Student Student { get; set; }
        public ObservableCollection<PredmetOcena> PolozeniIspiti { get; set; }
        public ObservableCollection<Predmet> NepolozeniPredmeti { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private PredmetOcena _selectedPo;
        public PredmetOcena SelectedPo
        {
            get { return _selectedPo; }
            set
            {
                _selectedPo = value;
                OnPropertyChanged(nameof(SelectedPo));
            }
        }
        private Student _SelectedStudent;
        public Student SelectedStudent
        {
            get { return _SelectedStudent; }
            set
            {
                _SelectedStudent = value;
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


        private string _ime;
        public string Ime
        {
            get => _ime;
            set
            {
                if (value != _ime)
                {
                    _ime = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _prezime;

        public string Prezime
        {
            get => _prezime;
            set
            {
                if (value != _prezime)
                {
                    _prezime = value;
                    OnPropertyChanged();
                }
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
        private Adresa _adresaStanovanja;
        public Adresa AdresaStanovanja
        {
            get { return _adresaStanovanja; }
            set
            {
                _adresaStanovanja = value;
                OnPropertyChanged(nameof(AdresaStanovanja));
                FormatAdresaStanovanja();

            }
        }
        private string _adresa;


        public string Adresa
        {
            get => _adresa;
            set
            {
                if (value != _adresa)
                {
                    _adresa = value;
                    OnPropertyChanged();
                }
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
            get => _email;
            set
            {
                if (value != _email)
                {
                    _email = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _brindeksa;

        public string Indeks
        {
            get => _brindeksa;
            set
            {
                if (value != _brindeksa)
                {
                    _brindeksa = value;
                    OnPropertyChanged();
                }
            }
        }

        private int _trengodina;

        public int GodinaStudija
        {
            get => _trengodina;
            set
            {
                if (value != _trengodina)
                {
                    _trengodina = value;
                    OnPropertyChanged();
                }
            }
        }
        private int _godupisa;

        public int GodinaUpisa
        {
            get => _godupisa;
            set
            {
                if (value != _godupisa)
                {
                    _godupisa = value;
                    OnPropertyChanged();
                }
            }
        }
        private Status _status;

        public Status Status
        {
            get => _status;
            set
            {
                if (value != _status)
                {
                    _status = value;
                    OnPropertyChanged();
                }
            }
        }

        private float _prosecnaOcena;
        public float ProsecnaOcena
        {
            get => _prosecnaOcena;
            set
            {
                if (value != _prosecnaOcena)
                {
                    _prosecnaOcena = value;
                    OnPropertyChanged();
                }
            }
        }

        private int _brojESPB;
        public int BrojESPB
        {
            get => _brojESPB;
            set
            {
                if (value != _brojESPB)
                {
                    _brojESPB = value;
                    OnPropertyChanged();
                }
            }
        }
        private ObservableCollection<PredmetOcena> _pocena;
        public ObservableCollection<PredmetOcena> PredOcena
        {
            get { return _pocena; }
            set
            {
                _pocena = value;
                OnPropertyChanged();
            }
        }
      


        public EditStudent(MainWindow mainWindow ,Student selected,PredmetOcena selectedPo)
        {
            InitializeComponent();
            DataContext = this;
            _mainWindow = mainWindow;
            SelectedPo = selectedPo;
            _studentRepository = new StudentRepository();
            _adresaRepository = new AdresaRepository();
            _studentPredmetRepository = new StudentPredmetRepository();
            _predmetOcenaRepository = new PredmetOcenaRepository();
            _predmetRepository = new PredmetRepository();
            Predmeti = new List<Predmet>();

            List<Predmet> predmeti = new List<Predmet>();
            List<Ocena> ocene = new List<Ocena>();

           SelectedStudent = selected;

            StatusCb.Items.Add("S");
            StatusCb.Items.Add("B");

            Student = _mainWindow.SelectedStudent;

            Prezime = Student.Prezime;
            Ime = Student.Ime;
            DatumRodjenja = Student.DatumRodjenja;
            Telefon = Student.Telefon;
            Email = Student.Email;
            Indeks = Student.Indeks;
            GodinaUpisa = Student.GodinaUpisa;
            GodinaStudija = Student.GodinaStudija;
            AdresaStanovanja = Student.AdresaStanovanja;
            Status = Student.Status;
            ProsecnaOcena = Student.ProsecnaOcena;
            BrojESPB = Student.BrojEspbStudenta;

           List <PredmetOcena> PolozeniPredmeti;

            PolozeniPredmeti = _predmetOcenaRepository.NadjiPolozenePredmete(SelectedStudent.Id);


            PolozeniIspiti = new ObservableCollection<PredmetOcena>();
            foreach (PredmetOcena polozenIspit in PolozeniPredmeti)
            {
                PolozeniIspiti.Add(polozenIspit);
            }

            List<int> NepolozeniIspiti = new List<int>();
            NepolozeniIspiti= _studentPredmetRepository.NadjiIdNepolozenihPredmeta(SelectedStudent.Id);

            NepolozeniPredmeti = new ObservableCollection<Predmet>();

            foreach (Predmet predmet in _predmetRepository.GetAll())
            {
                if (NepolozeniIspiti.Contains(predmet.Id))
                {
                    NepolozeniPredmeti.Add(predmet);
                }
            }

            ProsecnaOcena = _predmetOcenaRepository.IzracunajProsecnu(SelectedStudent.Id);
            BrojESPB = _predmetOcenaRepository.IzracunajESPB(SelectedStudent.Id);




        }
        private void FormatAdresaStanovanja()
        {
            if (AdresaStanovanja != null)
            {
                Adresa = $"{AdresaStanovanja.Ulica}, {AdresaStanovanja.Broj}, {AdresaStanovanja.Grad}, {AdresaStanovanja.Drzava}";
            }
            else
            {
                Adresa = string.Empty;
            }
        }


        private Regex _IndexRegex = new Regex("[A-Z]{2} [0-9]{1,3}/[0-9]{4}");

        private Regex _EmailRegex = new Regex("[a-z0-9.a-z0-9]*@mailinator.com");

        private Regex _DateRegex = new Regex("[0-9]{2}/[0-1][0-9]/[1-2][0-9]{3}");

        private Regex _AddressRegex = new Regex("[a-zA-z]*,[0-9]{3},[a-zA-z]*,[a-zA-z]*");

        private Regex _PhoneRegex = new Regex("[0-9]{3}/[0-9]{3}-[0-9]{3}");

        public string Error => null;

        public string this[string columnName]
        {
            get
            {
                if (columnName == "Indeks")
                {
                    if (string.IsNullOrEmpty(Indeks))
                        return "Indeks je obavezan";

                    Match match = _IndexRegex.Match(Indeks);
                    if (!match.Success)
                        return "Indeks mora biti u formatu: XY 123/YYYY";
                }
                else if (columnName == "Ime")
                {
                    if (string.IsNullOrEmpty(Ime))
                        return "Ime je obavezno";
                }
                else if (columnName == "Prezime")
                {
                    if (string.IsNullOrEmpty(Prezime))
                        return "Prezime je obavezno";
                }
                else if (columnName == "DatumRodjenja")
                {
                    if (string.IsNullOrEmpty((DatumRodjenja).ToString()))
                        return "Datum je obavezan";

                    Match match = _DateRegex.Match((DatumRodjenja).ToString());
                    if (!match.Success)
                        return "Datum mora biti u formatu: dd/MM/YYYY";
                }
                else if (columnName == "Adresa")
                {
                    if (string.IsNullOrEmpty(Adresa))
                        return "Adresa je obavezna";

                    Match match = _AddressRegex.Match(Adresa);
                    if (!match.Success)
                        return "Adresa mora biti u formatu: ulica, broj, grad, drzava";
                }

                else if (columnName == "Telefon")
                {
                    if (string.IsNullOrEmpty(Telefon))
                        return "Broj telefona je obavezan";

                    Match match = _PhoneRegex.Match(Telefon);
                    if (!match.Success)
                        return "Telefon mora biti u formatu: XXX/YYY-ZZZ";
                }
                else if (columnName == "Email")
                {
                    if (string.IsNullOrEmpty(Email))
                        return "Email je obavezan";

                    Match match = _EmailRegex.Match(Email);
                    if (!match.Success)
                        return "Email mora biti u formatu: username@mailinator.com";
                }
                else if (columnName == "GodinaUpisa")
                {
                    if (string.IsNullOrEmpty((GodinaUpisa).ToString()))
                        return "Godina upisa je obavezna";
                }
                else if (columnName == "GodinaStudija")
                {
                    if (string.IsNullOrEmpty((GodinaStudija).ToString()))
                        return "Trenutna godina je obavezna";
                }
                else if (columnName == "statusi")
                {
                    if (string.IsNullOrEmpty((Status).ToString()))
                        return "Nacin finansiranja je obavezan";
                }

                return null;
            }
        }

        private readonly string[] _validatedProperties = { "Ime", "Prezime", /*"DatRodj", "Adresa", "BrTel",*/ "Email", "BrIndeksa"/*, "GodUpisa", "TrenGodina"*/ };

        public bool isValid
        {
            get
            {
                foreach (var property in _validatedProperties)
                {
                    if (this[property] != null)
                        return false;
                }

                return true;
            }
        }
        private void PublishButton_Click(object sender, RoutedEventArgs e)
        {
            AdresaStanovanja = new Adresa(_mainWindow.SelectedStudent.AdresaStanovanja.Ulica, _mainWindow.SelectedStudent.AdresaStanovanja.Broj,
               _mainWindow.SelectedStudent.AdresaStanovanja.Grad, _mainWindow.SelectedStudent.AdresaStanovanja.Drzava);
       
            AdresaStanovanja.Id = _adresaRepository.GetAdressIdByStreetAndNumber(AdresaStanovanja.Ulica, AdresaStanovanja.Broj);


            Prezime = prezimeTb.Text;
            Ime = imeTb.Text;
            DatumRodjenja = DateOnly.Parse(datumRodjenjaTb.Text);
            Telefon = telefonTb.Text;
            Email = emailTb.Text;
            Indeks = indeksTb.Text;
            GodinaUpisa = Convert.ToInt16(GodUpTb.Text);
            GodinaStudija = int.Parse(GodStudTb.Text);
            Status = (Status)Enum.Parse(typeof(Status), StatusCb.Text);
            string[] adresaStanovanjaSplit = adresaSTb.Text.Split(',');
            ProsecnaOcena = 0;
            BrojESPB = 0;

            AdresaStanovanja.Ulica = adresaStanovanjaSplit[0];
            AdresaStanovanja.Broj = adresaStanovanjaSplit[1];
            AdresaStanovanja.Grad = adresaStanovanjaSplit[2];
            AdresaStanovanja.Drzava = adresaStanovanjaSplit[3];
            
            _adresaRepository.Update(AdresaStanovanja);

            Student student = new Student(Prezime, Ime, DatumRodjenja, AdresaStanovanja, Telefon, Email, Indeks, GodinaUpisa, GodinaStudija, Status, ProsecnaOcena,BrojESPB);
            student.IdAdreseStanovanja = AdresaStanovanja.Id;

            student.Id = _studentRepository.GetIdByIndeks(student.Indeks);
            _studentRepository.Update(student);

            MessageBox.Show("Student uspesno izmenjen!");
            _mainWindow.EditStudentItem(student);
            this.Close();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Ponisti(object sender, RoutedEventArgs e)
        {
           

            if (SelectedPo == null)
            {
                MessageBox.Show("Izaberite ocenu koju želite da poništite!");
                return;
            }

            MessageBoxResult result = MessageBox.Show("Da li ste sigurni?", "Potvrda", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                if (SelectedPo != null)
                {
                    int selectedPredmetId = SelectedPo.PredmetId;
                    Predmet selectedPredmet = _predmetRepository.GetById(selectedPredmetId);

                    _predmetOcenaRepository.Delete(SelectedPo);
                    PolozeniIspiti.Remove(SelectedPo);
                  

                    NepolozeniPredmeti.Add(selectedPredmet); // Dodajte samo odabrani predmet
                   

                    // Osvježite prikaz u DataGrid-u
                    sdatagrid.Items.Refresh();

                    MessageBox.Show("Ocena uspešno poništena!");

                    datagrid.Items.Refresh();
                }


            }
        }
        private void UkloniPredmet(object sender, RoutedEventArgs e)
        {
            if (SelectedPredmet == null)
            {
                MessageBox.Show("Izaberite predmet koji želite da uklonite!");
                return;
            }

            MessageBoxResult result = MessageBox.Show("Da li ste sigurni?", "Potvrda", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                if (SelectedPredmet != null)
                {
                    // Uklonite izabrani predmet iz tabele nepoloženih predmeta
                    NepolozeniPredmeti.Remove(SelectedPredmet);
                    _predmetRepository.Delete(SelectedPredmet);
                    sdatagrid.Items.Refresh();
                    MessageBox.Show("Predmet uspešno uklonjen!");
                }
            }
        }

        public void ShowAddGrade_Window(object sender, RoutedEventArgs e)
        {
            if (SelectedPredmet != null)
            {
                AddOcena addOcena = new AddOcena(SelectedStudent, SelectedPredmet);
                addOcena.Parent = this;
                addOcena.ShowDialog();
                _studentRepository.Update(SelectedStudent);
                sdatagrid.Items.Refresh();

            }
            else
            {
                MessageBox.Show("Odaberite predmet za koji želite da upišete ocenu.");
            }
        }




        public void ShowAddSubject_Window(object sender, RoutedEventArgs e)
        {

            Dispatcher.Invoke(() =>
            {
                AddStudentToSubject addSubjectWindow = new AddStudentToSubject(SelectedStudent, SelectedPredmet, this);
                addSubjectWindow.ShowDialog();
                sdatagrid.Items.Refresh();
            });
            // Nakon zatvaranja prozora možete pristupiti ažuriranoj vrednosti SelectedPredmet
        }


    }


}



