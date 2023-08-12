using Oisisiproj.Enums;
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
    /// Interaction logic for AddSubject.xaml
    /// </summary>
    public partial class AddSubject : Window, INotifyPropertyChanged
    {
        public readonly PredmetRepository _predmetRepository;

        private MainWindow _mainWindow;

        private string _sifra;
        public string Sifra
        {
            get { return _sifra; }
            set
            {
                _sifra = value;
                OnPropertyChanged();
            }
        }

        private string _naziv;
        public string Naziv
        {
            get { return _naziv; }
            set
            {
                _naziv = value;
                OnPropertyChanged();
            }
        }

        private Semestar _semestar;
        public Semestar Semestar
        {
            get { return _semestar; }
            set
            {
                _semestar = value;
                OnPropertyChanged();
            }
        }

        private int _eSPB;
        public int ESPB
        {
            get { return _eSPB; }
            set
            {
                _eSPB = value;
                OnPropertyChanged();
            }
        }

        private int _godinaStudija;
        public int GodinaStudija
        {
            get { return _godinaStudija; }
            set
            {
                _godinaStudija = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public AddSubject(MainWindow mainWindow)
        {
            InitializeComponent();
            DataContext = this;

            _mainWindow = mainWindow;

            _predmetRepository = new PredmetRepository();

            semestarCb.Items.Add("Zimski");
            semestarCb.Items.Add("Letnji");
        }

        private void PublishSubjectButton_Click(object sender, RoutedEventArgs e)
        {
            Sifra = sifraTb.Text;
            Naziv = nazivTb.Text;
            GodinaStudija = Convert.ToInt16(godStudTb.Text);
            ESPB = Convert.ToInt16(espbTb.Text);
            Semestar = (Semestar)Enum.Parse(typeof(Semestar), semestarCb.Text);

            Predmet predmet = new Predmet(Sifra, Naziv, Semestar, GodinaStudija, ESPB, 0);
            _predmetRepository.Save(predmet);

            MessageBox.Show("Profesor uspesno dodat!");
            _mainWindow.AddDataItemPredmet(predmet);
            this.Close();
        }

        private void CancelSubject_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
