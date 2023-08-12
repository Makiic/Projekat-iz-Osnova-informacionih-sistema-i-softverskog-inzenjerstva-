using Oisisiproj.Model;
using Oisisiproj.Repository;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace Oisisiproj.View
{
    /// <summary>
    /// Interaction logic for AddStudentToSubject.xaml
    /// </summary>
    public partial class AddStudentToSubject : Window, INotifyPropertyChanged
    {
        public EditStudent editStudent;
        public Window Parent { get; set; }
        private readonly StudentRepository _studentRepository;
        private readonly PredmetRepository _predmetRepository;
        private readonly OcenaRepository _ocenaRepository;
        private readonly StudentPredmetRepository _studentPredmetRepository;

        public Student SelectedStudent;
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
        public ObservableCollection<Predmet> listaNepolozenihPredmeta;
        public List<Predmet> Zadovoljava { get; set; }

        public AddStudentToSubject(Student selectedStudent, Predmet selectedPredmet,Window parent)
        {
            InitializeComponent();
            DataContext = this;
            _studentRepository = new StudentRepository();
            _studentPredmetRepository = new StudentPredmetRepository();
            _predmetRepository = new PredmetRepository();
            _ocenaRepository = new OcenaRepository();

            SelectedStudent = selectedStudent;
            SelectedPredmet = selectedPredmet;
            Parent = parent;

            int TrenutnaGodinaStudenta = _studentRepository.TrenutnaGodStudija(SelectedStudent);
            List<int> IdNepolozenihPredmeta = _studentPredmetRepository.NadjiIdNepolozenihPredmeta(SelectedStudent.Id);
            List<int> IdPolozenihPredmeta = _ocenaRepository.NadjiIdNepolozenihPredmeta(SelectedStudent.Id);

            Zadovoljava = new List<Predmet>();
            foreach (Predmet predUslov in _predmetRepository.GetAll())
            {
                if (!IdNepolozenihPredmeta.Contains(predUslov.Id) && !IdPolozenihPredmeta.Contains(predUslov.Id) && TrenutnaGodinaStudenta >= predUslov.GodinaStudija)
                {
                    Zadovoljava.Add(predUslov);
                }
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private void AddSubject_Click(object sender, RoutedEventArgs e)
        {
            var selectedSubj = NepolozeniPredmeti.SelectedItem as Predmet;
            EditStudent editStudent = (EditStudent)this.Parent;
          
              StudentPredmet studPred =new StudentPredmet(SelectedStudent.Id,SelectedPredmet.Id);
            _studentPredmetRepository.Save(studPred);
                // Add the subject to the NepolozeniPredmeti collection
                editStudent.NepolozeniPredmeti.Add(SelectedPredmet);

                // Refresh the UI control to display the added subject
                editStudent.sdatagrid.Items.Refresh();
            
            Close();
        }



        private void CancelAddSubject_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
