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
    /// Interaction logic for EditSubject.xaml
    /// </summary>
    public partial class EditSubject : Window
    {
        public readonly PredmetRepository _predmetRepository;
        public readonly ProfesorRepository _profesorRepository;

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

        public EditSubject(MainWindow mainwindow)
        {
            InitializeComponent();
            DataContext = this;

            _mainWindow = mainwindow;

            _predmetRepository = new PredmetRepository();
            _profesorRepository = new ProfesorRepository();

            semestarCb.Items.Add("Zimski");
            semestarCb.Items.Add("Letnji");

            sifraTb.Text = _mainWindow.SelectedPredmet.Sifra;
            nazivTb.Text = _mainWindow.SelectedPredmet.Naziv;
            godStudTb.Text = Convert.ToString(_mainWindow.SelectedPredmet.GodinaStudija);
            espbTb.Text = Convert.ToString(_mainWindow.SelectedPredmet.ESPB);
            semestarCb.Text = Convert.ToString(_mainWindow.SelectedPredmet.Semestar);

            if (_mainWindow.SelectedPredmet.Profesor != null)
            {
                profesorTb.Text = _mainWindow.SelectedPredmet.Profesor.Ime + " " + _mainWindow.SelectedPredmet.Profesor.Prezime;
            }
            else
            {
                profesorTb.Text = "";
            }

            if (profesorTb.Text == "")
            {
                removeProfButton.IsEnabled = false;
                addProfButton.IsEnabled = true;
            }
            else
            {
                removeProfButton.IsEnabled = true;
                addProfButton.IsEnabled = false;
            }
        }

        private void EditSubjectButton_Click(object sender, RoutedEventArgs e)
        {
            Sifra = sifraTb.Text;
            Naziv = nazivTb.Text;
            GodinaStudija = Convert.ToInt16(godStudTb.Text);
            ESPB = Convert.ToInt16(espbTb.Text);
            Semestar = (Semestar)Enum.Parse(typeof(Semestar), semestarCb.Text);

            int idProfesora = 0;

            if (profesorTb.Text != "")
            {
                foreach(Profesor profesor in _profesorRepository.GetAll())
                {
                    if(profesor.Ime + " " + profesor.Prezime == profesorTb.Text)
                    {
                        idProfesora = profesor.Id;
                    }
                }
            }

            Predmet predmet = new Predmet(Sifra, Naziv, Semestar, GodinaStudija, ESPB, idProfesora);
            predmet.Id = _predmetRepository.GetSujbectIdByCode(Sifra);
            _predmetRepository.Update(predmet);

            if(idProfesora != 0)
            {
                foreach(Profesor profesor in _profesorRepository.GetAll())
                {
                    profesor.IdPredmeta.Clear();
                    foreach(Predmet predmett in _predmetRepository.GetAll())
                    {
                        if(predmett.IdProfesora == profesor.Id)
                        {
                            profesor.IdPredmeta.Add(predmett.Id);
                        }
                    }
                    _profesorRepository.Update(profesor);
                }
            }

            MessageBox.Show("Predmet uspesno izmenjen!");
            _mainWindow.EditDataItemPredmet(predmet);
            this.Close();

            this.Close();
        }

        private void CancelSubject_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void addProfButton_Click(object sender, RoutedEventArgs e)
        {
            ChoseProfessor choseProfessor = new ChoseProfessor(this, _mainWindow);
            choseProfessor.Show();
            removeProfButton.IsEnabled = true;
            addProfButton.IsEnabled = false;
        }

        private void removeProfButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                profesorTb.Clear();
            }
            removeProfButton.IsEnabled = false;
            addProfButton.IsEnabled = true;
        }



    }
}
