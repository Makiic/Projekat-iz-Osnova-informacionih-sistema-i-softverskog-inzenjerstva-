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
    public partial class ChoseProfessor : Window, INotifyPropertyChanged
    {
        private EditSubject _editSubject;
        private MainWindow _mainWindow;

        public readonly ProfesorRepository _profesorRepository;
        public readonly PredmetRepository _predmetRepository;

        private ObservableCollection<Profesor> _profesori;
        public ObservableCollection<Profesor> Profesori
        {
            get { return _profesori; }
            set
            {
                _profesori = value;
                OnPropertyChanged();
            }
        }

        private Profesor _selectedProfesor;
        public Profesor SelectedProfesor
        {
            get { return _selectedProfesor; }
            set
            {
                _selectedProfesor = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ChoseProfessor(EditSubject editSubject, MainWindow mainWindow)
        {
            InitializeComponent();
            DataContext = this;

            _profesorRepository = new ProfesorRepository();
            _predmetRepository = new PredmetRepository();

            _mainWindow = mainWindow;
            _editSubject = editSubject;

            Profesori = new ObservableCollection<Profesor>();

            foreach (Profesor profesor in _profesorRepository.GetAll())
            {
                Profesori.Add(profesor);
            }
        }

        private void DodajProfesora_Click(object sender, RoutedEventArgs e)
        {
            _editSubject.profesorTb.Text = SelectedProfesor.Ime + " " + SelectedProfesor.Prezime;
            this.Close();
        }

        private void Odustani_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
