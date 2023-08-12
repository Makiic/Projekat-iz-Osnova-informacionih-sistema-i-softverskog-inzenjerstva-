using Oisisiproj.Enums;
using Oisisiproj.Model;
using Oisisiproj.Repository;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for AddStudent.xaml
    /// </summary>
    public partial class AddStudent : Window, INotifyPropertyChanged,IDataErrorInfo
    {
        private MainWindow _mainWindow;

        private readonly StudentRepository _studentRepository;
        private readonly AdresaRepository _adresaRepository;
        public Student Student { get; set; }


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
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
                OnPropertyChanged();

                // Add validation for "Adresa" property
                Adresa = value?.ToString();
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
        private float ProsOcena;

        public AddStudent(MainWindow mainWindow)
        {
            InitializeComponent();
            DataContext = this;
            _mainWindow = mainWindow;

            _studentRepository=new StudentRepository();
            _adresaRepository = new AdresaRepository();

            StatusCb.Items.Add("S");
            StatusCb.Items.Add("B");






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


        
        private void CreateStudent_Click(object sender, RoutedEventArgs e)
        {


            Prezime = prezimeTb.Text;
            Ime = imeTb.Text;
            DatumRodjenja = DateOnly.Parse(datumRodjenjaTb.Text);
            Telefon = telefonTb.Text;
            Email = emailTb.Text;
            Indeks = indeksTb.Text;
            GodinaUpisa = Convert.ToInt16(godupTb.Text);
            GodinaStudija = int.Parse(godinaStudTb.Text);
            Status = (Status)Enum.Parse(typeof(Status), StatusCb.Text);
            string[] adresaStanovanjaSplit = adresaSTb.Text.Split(',');
            
            AdresaStanovanja = new Adresa(adresaStanovanjaSplit[0], adresaStanovanjaSplit[1], adresaStanovanjaSplit[2], adresaStanovanjaSplit[3]);
            ProsOcena = 0;
            BrojESPB = 0;


            AdresaStanovanja.Id = _adresaRepository.NextId();
            _adresaRepository.Save(AdresaStanovanja);


            Student student = new Student(Prezime, Ime, DatumRodjenja, AdresaStanovanja, Telefon, Email, Indeks, GodinaUpisa, GodinaStudija,Status,ProsOcena,BrojESPB);
            student.IdAdreseStanovanja = AdresaStanovanja.Id;
          
            _studentRepository.Save(student);

            MessageBox.Show("Profesor uspesno dodat!");
            _mainWindow.AddStudentItem(student);
            this.Close();

        }

        private void OdustaniButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
    }

