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
    /// Interaction logic for AddSubjectToProfessor.xaml
    /// </summary>
    public partial class AddSubjectToProfessor : Window, INotifyPropertyChanged
    {
        public readonly ProfesorRepository _profesorRepository;
        public readonly PredmetRepository _predmetRepository;

        private EditProfessor _editprofessor;
        private MainWindow _mainWindow;

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

        public AddSubjectToProfessor(EditProfessor editProfessor, MainWindow mainWindow)
        {
            InitializeComponent();
            DataContext = this;

            _editprofessor = editProfessor;
            _mainWindow = mainWindow;

            _profesorRepository = new ProfesorRepository();
            _predmetRepository = new PredmetRepository();

            Predmeti = new ObservableCollection<Predmet>();

            foreach (Predmet predmet in _predmetRepository.GetAll())
            {
                if(predmet.IdProfesora == 0)
                {
                    Predmeti.Add(predmet);
                }
            }
        }

        private void DodajPredmet_Click(object sender, RoutedEventArgs e)
        {
            if(SelectedPredmet != null)
            {
                _mainWindow.SelectedProfesor.Predmeti.Add(SelectedPredmet);
                _mainWindow.SelectedProfesor.IdPredmeta.Add(SelectedPredmet.Id);
                _profesorRepository.Update(_mainWindow.SelectedProfesor);

                SelectedPredmet.IdProfesora = _mainWindow.SelectedProfesor.Id;
                _predmetRepository.Update(SelectedPredmet);

                SelectedPredmet.ImePrezimeProfesora = _mainWindow.SelectedProfesor.Ime + " " + _mainWindow.SelectedProfesor.Prezime;
                _mainWindow.predmetDataGrid.Items.Refresh();

                MessageBox.Show("Predmet uspešno dodat profesoru!");
                _editprofessor.UpdatePredmetGrid(SelectedPredmet);
                this.Close();
            }
            else
            {
                MessageBox.Show("Izaberite predmet koji želite da dodelite profesoru!");
            }    
        }

        private void Odustani_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
