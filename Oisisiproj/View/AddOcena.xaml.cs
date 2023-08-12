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
    /// Interaction logic for AddOcena.xaml
    /// </summary>
    public partial class AddOcena : Window, INotifyPropertyChanged
    {
        public Window Parent { get; set; }
        private readonly StudentRepository _studentRepository;
        private readonly PredmetRepository _predmetRepository;
        private readonly OcenaRepository _ocenaRepository;
        private readonly PredmetOcenaRepository _predmetOcenaRepository;
        private readonly StudentPredmetRepository _studentPredmetRepository;

        public Student SelectedStudent;
        public Predmet SelectedPredmet;
        public Ocena Ocena;

        public ObservableCollection<string> ocene
        {
            get;
            set;
        }

        private string _sifraPredmeta;
        public string SifraPredmeta
        {
            get => _sifraPredmeta;
            set
            {
                if (value != _sifraPredmeta)
                {
                    _sifraPredmeta = value;
                    OnPropertyChanged();
                }
            }
        }
        private string _nazivPredmeta;
        public string NazivPredmeta
        {
            get => _nazivPredmeta;
            set
            {
                if (value != _nazivPredmeta)
                {
                    _nazivPredmeta = value;
                    OnPropertyChanged();
                }
            }
        }




        private int _ocenaVrednost;

        public int Vrednost
        {
            get => _ocenaVrednost;
            set
            {
                if (value != _ocenaVrednost)
                {
                    _ocenaVrednost = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _datumIspita;

        public string DatumIspita
        {
            get => _datumIspita;
            set
            {
                if (value != _datumIspita)
                {
                    _datumIspita = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public AddOcena(Student selectedStudent, Predmet selectedPredmet)
        {
            InitializeComponent();
            DataContext = this;
            _studentRepository = new StudentRepository();
            _studentPredmetRepository = new StudentPredmetRepository();
            _predmetRepository = new PredmetRepository();
            _predmetOcenaRepository = new PredmetOcenaRepository();
            _ocenaRepository = new OcenaRepository();

            SelectedStudent = selectedStudent;
            SelectedPredmet = selectedPredmet;

            SifraPredmeta = SelectedPredmet.Sifra;
            NazivPredmeta = SelectedPredmet.Naziv;
            ocene = new ObservableCollection<string>();
            ocene.Add("6");
            ocene.Add("7");
            ocene.Add("8");
            ocene.Add("9");
            ocene.Add("10");

        }



        public void CreateGrade_Click(object sender, RoutedEventArgs e)
        {


            string vrednost = vrednostOceneCombo.Text;
            if (vrednost.Equals("6"))
            {
                Vrednost = 6;
            }
            else if (vrednost.Equals("7"))
            {
                Vrednost = 7;
            }
            else if (vrednost.Equals("8"))
            {
                Vrednost = 8;
            }
            else if (vrednost.Equals("9"))
            {
                Vrednost = 9;
            }
            else
            {
                Vrednost = 10;
            }


            DateTime datIspita = DateTime.Parse(DatumIspita);
            Ocena ocena = new Ocena(Vrednost, datIspita, SelectedStudent.Id, SelectedPredmet.Id);
            EditStudent editStudent = (EditStudent)this.Parent;

            PredmetOcena predmetOcena = new PredmetOcena(SelectedStudent.Id, SelectedPredmet.Id, SelectedPredmet.Sifra, SelectedPredmet.Naziv, SelectedPredmet.ESPB, Vrednost, datIspita);
            _predmetOcenaRepository.Save(predmetOcena);

            List<PredmetOcena> PolozeniPredmeti;
            PolozeniPredmeti = _predmetOcenaRepository.NadjiPolozenePredmete(SelectedStudent.Id);
            float ProsecnaOcena = 0;
            double suma = 0;
            int brojac = 0;
            foreach (PredmetOcena polozenIspit in PolozeniPredmeti)
            {
                suma += polozenIspit.BrojcanaVrednost;
                ++brojac;

            }

            ProsecnaOcena = brojac == 0 ? 0f : (float)Math.Round(suma / brojac, 2);


            editStudent.ProsecnaOcena = ProsecnaOcena;
            SelectedStudent.ProsecnaOcena = ProsecnaOcena;


            int sumaEspb = 0;
            foreach (PredmetOcena polozenIspit in PolozeniPredmeti)
            {
                sumaEspb += polozenIspit.BrojESPB;

            }
            editStudent.BrojESPB = sumaEspb;
            SelectedStudent.BrojEspbStudenta = sumaEspb;
           


            _studentRepository.IzmeniProsOcenuIEspb(SelectedStudent.Id, ProsecnaOcena, sumaEspb);

            StudentPredmet studSubj = _studentPredmetRepository.FindStudentPredmet(SelectedStudent.Id, SelectedPredmet.Id);
            _studentPredmetRepository.Delete(studSubj);
            editStudent.NepolozeniPredmeti.Remove(SelectedPredmet);


            editStudent.PolozeniIspiti.Add(predmetOcena);

            Close();

        }

        public void CancelCreateGrade_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
