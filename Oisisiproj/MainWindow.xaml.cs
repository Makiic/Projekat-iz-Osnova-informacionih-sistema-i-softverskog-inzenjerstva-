using Oisisiproj.Model;
using Oisisiproj.Repository;
using Oisisiproj.View;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Runtime.CompilerServices;
using Oisisiproj.View;

namespace Oisisiproj
{
    public partial class MainWindow : Window,INotifyPropertyChanged
    {
        private App app;

        public readonly StudentPredmetRepository _studentPredmetRepository;
        public readonly StudentRepository _studentRepository;
        public readonly AdresaRepository _adresaRepository;
        public readonly ProfesorRepository _profesorRepository;
        public readonly PredmetRepository _predmetRepository;
        public readonly OcenaRepository _ocenaRepository;
        private readonly PredmetOcenaRepository _predmetOcenaRepository;


        private readonly KatedraRepository _katedraRepository;
        public Katedra SelectedDepartment { get; set; }
        public ObservableCollection<Katedra> Katedre { get; set; }

        private int _studentId;
        public int StudentId
        {
            get { return _studentId; }
            set
            {
                _studentId = value;
                OnPropertyChanged();
            }
        }
        private ObservableCollection<Student> _studenti;
        public ObservableCollection<Student> Studenti
        {
            get { return _studenti; }
            set
            {
                _studenti = value;
                OnPropertyChanged();
            }
        }
        private PredmetOcena _selpocena;
        public PredmetOcena SelectedPO
        {
            get { return _selpocena; }
            set
            {
                _selpocena = value;
                OnPropertyChanged();
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
        private Student _student;
        public Student Student
        {
            get { return _student; }
            set
            {
                _student = value;
                OnPropertyChanged();
            }
        }

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
        private ObservableCollection<Predmet> _polozeniPredmeti;
        public ObservableCollection<Predmet> PolozeniPredmeti
        {
            get { return _polozeniPredmeti; }
            set
            {
                _polozeniPredmeti = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Predmet> _nepolozeniPredmeti;
        public ObservableCollection<Predmet> NepolozeniPredmeti
        {
            get { return _nepolozeniPredmeti; }
            set
            {
                _nepolozeniPredmeti = value;
                OnPropertyChanged();
            }
        }
        private ObservableCollection<Ocena> _ocene;
        public ObservableCollection<Ocena> Ocene
        {
            get { return _ocene; }
            set
            {
                _ocene = value;
                OnPropertyChanged();
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
        private const string SRB = "sr-Latn-RS";
        private const string ENG = "en-US";
        public List<Adresa> Adrese;

        private DispatcherTimer timer;
        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public MainWindow()
        {

            InitializeComponent();
            DataContext = this;

            _profesorRepository = new ProfesorRepository();
            Profesori = new ObservableCollection<Profesor>(_profesorRepository.GetAll());

            _adresaRepository = new AdresaRepository();
            Adrese = new List<Adresa>(_adresaRepository.GetAll());

            _predmetRepository = new PredmetRepository();
            Predmeti = new ObservableCollection<Predmet>(_predmetRepository.GetAll());

            _studentRepository = new StudentRepository();
            Studenti = new ObservableCollection<Student>(_studentRepository.GetAll());

            _ocenaRepository = new OcenaRepository();
            Ocene = new ObservableCollection<Ocena>(_ocenaRepository.GetAll());
            _predmetOcenaRepository = new PredmetOcenaRepository();

            _katedraRepository =new KatedraRepository();
            Katedre = new ObservableCollection<Katedra>(_katedraRepository.GetAll());

            foreach (Predmet predmet in Predmeti)
            {
                foreach (Profesor profesor in Profesori)
                {
                    if (predmet.IdProfesora == profesor.Id)
                    {
                        predmet.Profesor = profesor;
                        predmet.ImePrezimeProfesora = profesor.Ime + " " + profesor.Prezime;
                        profesor.Predmeti.Add(predmet);
                    }
                }
            }

            foreach (Profesor profesor in Profesori)
            {
                foreach (Adresa adresa in Adrese)
                {
                    if (adresa.Id == profesor.IdAdreseStanovanja)
                    {
                        profesor.AdresaStanovanja = adresa;
                    }
                    else if (adresa.Id == profesor.IdAdreseKancelarije)
                    {
                        profesor.AdresaKancelarije = adresa;
                    }
                }
            }

            foreach (Student student in Studenti)
            {
                foreach (Adresa adresa in Adrese)
                {
                    if (adresa.Id == student.IdAdreseStanovanja)
                    {
                        student.AdresaStanovanja = adresa;
                    }

                }
            }

            app = (App)Application.Current;
            app.ChangeLanguage(SRB);

            Loaded += MainWindow_Loaded;
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            timer.Start();
            
           
        }
        private void MenuItem_Click_Serbian(object sender, RoutedEventArgs e)
        {
            app.ChangeLanguage(SRB);
        }
        private void MenuItem_Click_English(object sender, RoutedEventArgs e)
        {
            app.ChangeLanguage(ENG);
        }
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            DateTimeTextBlock.Text = DateTime.Now.ToString("HH:mm:ss dd.MM.yyyy");
        }

        public void AddButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            TabItem selectedTab = (TabItem)MainTabControl.SelectedItem;
            if (selectedTab.Header.ToString() == "Profesori")
            {
                AddProfessor addProfessor = new AddProfessor(this);
                addProfessor.Show();
            }
            else if (selectedTab.Header.ToString() == "Predmeti")
            {
                AddSubject addSubject = new AddSubject(this);
                addSubject.Show();
            }
            else if (selectedTab.Header.ToString() == "Studenti")
            {
                AddStudent addStudent = new AddStudent(this);
                addStudent.Show();
            }
        }
        
        public void AddDataItem(Profesor newDataItem)
        {
            Profesori.Add(newDataItem);
            profesorDataGrid.Items.Refresh();
        }
        
        public void EditButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Adrese = new List<Adresa>(_adresaRepository.GetAll());
            
            foreach (Profesor profesor in Profesori)
            {
                foreach (Adresa adresa in Adrese)
                {
                    if (adresa.Id == profesor.IdAdreseStanovanja)
                    {
                        profesor.AdresaStanovanja = adresa;
                    }
                    else if (adresa.Id == profesor.IdAdreseKancelarije)
                    {
                        profesor.AdresaKancelarije = adresa;
                    }
                }
            }
            
            foreach (Student student in Studenti)
            {
                foreach (Adresa adresa in Adrese)
                {
                    if (adresa.Id == student.IdAdreseStanovanja)
                    {
                        student.AdresaStanovanja = adresa;
                    }
                   
                }
            }
            
            TabItem selectedTab = (TabItem)MainTabControl.SelectedItem;
            if (selectedTab.Header.ToString() == "Profesori")
            {
                if (profesorDataGrid.SelectedItem == null)
                {
                    MessageBox.Show("Izaberite profesora kog želite da menjate!");
                    return;
                }
                EditProfessor editProfessor = new EditProfessor(this);
                editProfessor.Show();
            }
            else if (selectedTab.Header.ToString() == "Predmeti")
            {
                if (predmetDataGrid.SelectedItem == null)
                {
                    MessageBox.Show("Izaberite predmet koji želite da menjate!");
                    return;
                }
                EditSubject editSubject = new EditSubject(this);
                editSubject.Show();
            }
            else if (selectedTab.Header.ToString() == "Studenti")
            {
                if (studentDataGrid.SelectedItem == null)
                {
                    MessageBox.Show("Izaberite studenta kog želite da menjate!");
                    return;
                }
                EditStudent editStudent = new EditStudent(this,SelectedStudent,SelectedPO);
               
                editStudent.Show();
            }
            else if (selectedTab.Header.ToString() == "Katedra")
            {
                if (katedraDatagrid.SelectedItem == null)
                {
                    MessageBox.Show("Izaberite katedru koju želite da menjate!");
                    return;
                }
                UpdateKatedru updatesef = new UpdateKatedru(this,SelectedDepartment);
                updatesef.Show();
            }
        }
        
        internal void AddStudentItem(Student student)
        {
           Studenti.Add(student);
            studentDataGrid.Items.Refresh();
        }

        public void EditStudentItem(Student studentt)
        {
            foreach (Student student in Studenti.Reverse<Student>())
            {
                Studenti.Remove(student);
            }
            foreach (Student studenti in _studentRepository.GetAll())
            {
                Studenti.Add(studenti);
            }
            studentDataGrid.Items.Refresh();
        }
        
        public void DeleteButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            TabItem selectedTab = (TabItem)MainTabControl.SelectedItem;
            if (selectedTab.Header.ToString() == "Profesori")
            {
                if (profesorDataGrid.SelectedItem == null)
                {
                    MessageBox.Show("Izaberite profesora kog želite da obrišete!");
                    return;
                }

                MessageBoxResult result = MessageBox.Show("Are you sure?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    _profesorRepository.Delete(SelectedProfesor);
                    Profesori.Remove(SelectedProfesor);
                    MessageBox.Show("Profesor uspešno obirsan!");
                    profesorDataGrid.Items.Refresh();
                    foreach (Predmet predmet in Predmeti)
                    {
                        predmet.ImePrezimeProfesora = "";
                        foreach (Profesor profesor in Profesori)
                        {
                            if (predmet.IdProfesora == profesor.Id)
                            {
                                predmet.Profesor = profesor;
                                predmet.ImePrezimeProfesora = profesor.Ime + " " + profesor.Prezime;
                            }
                        }
                        if(predmet.ImePrezimeProfesora == "")
                        {
                            predmet.IdProfesora = 0;
                            _predmetRepository.Update(predmet);
                        }
                    }
                    predmetDataGrid.Items.Refresh();
                }
            }
            else if (selectedTab.Header.ToString() == "Predmeti")
            {
                if (predmetDataGrid.SelectedItem == null)
                {
                    MessageBox.Show("Izaberite predmet koji želite da obrišete!");
                    return;
                }

                MessageBoxResult result = MessageBox.Show("Are you sure?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    _predmetRepository.Delete(SelectedPredmet);
                    Predmeti.Remove(SelectedPredmet);
                    MessageBox.Show("Predmet uspešno obirsan!");
                    profesorDataGrid.Items.Refresh();
                    predmetDataGrid.Items.Refresh();
                }
            }
            else if (selectedTab.Header.ToString() == "Studenti")
            {
                if (studentDataGrid.SelectedItem == null)
                {
                    MessageBox.Show("Izaberite studenta kog želite da obrišete!");
                    return;
                }

                MessageBoxResult result = MessageBox.Show("Are you sure?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    _studentRepository.Delete(SelectedStudent);
                    Studenti.Remove(SelectedStudent);
                    MessageBox.Show("Student uspešno obirsan!");
                    studentDataGrid.Items.Refresh();

                }
            }
        }

        public void EditDataItem(Profesor professor)
        {
            foreach(Profesor profesor in Profesori.Reverse<Profesor>())
            {
                Profesori.Remove(profesor);
            }
            foreach (Profesor profesor1 in _profesorRepository.GetAll())
            {
                Profesori.Add(profesor1);
            }
            profesorDataGrid.Items.Refresh();
            foreach (Predmet predmet in Predmeti)
            {
                foreach (Profesor profesor in Profesori)
                {
                    if (predmet.IdProfesora == profesor.Id)
                    {
                        predmet.Profesor = profesor;
                        predmet.ImePrezimeProfesora = profesor.Ime + " " + profesor.Prezime;
                    }
                }
            }
            predmetDataGrid.Items.Refresh();
        }
        
        public void AddDataItemPredmet(Predmet newDataItem)
        {
            Predmeti.Add(newDataItem);
            predmetDataGrid.Items.Refresh();
        }

        public void EditDataItemPredmet(Predmet predmet)
        {
            foreach (Predmet predmett in Predmeti.Reverse<Predmet>())
            {
                Predmeti.Remove(predmett);
            }
            foreach (Predmet predmettt in _predmetRepository.GetAll())
            {
                Predmeti.Add(predmettt);
            }
            foreach (Predmet predmetttt in Predmeti)
            {
                predmetttt.ImePrezimeProfesora = "";
                foreach (Profesor profesor in Profesori)
                {
                    if (predmetttt.IdProfesora == profesor.Id)
                    {
                        predmetttt.Profesor = profesor;
                        predmetttt.ImePrezimeProfesora = profesor.Ime + " " + profesor.Prezime;
                    }
                }
            }
            predmetDataGrid.Items.Refresh();
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string searchText = searchTextBox.Text.ToLower();
            string[] searchWords = searchText.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            TabItem selectedTab = (TabItem)MainTabControl.SelectedItem;
            if (selectedTab.Header.ToString() == "Profesori")
            {
                foreach (Profesor item in profesorDataGrid.Items)
                {
                    string columnValue = item.Ime.ToLower() + item.Prezime.ToLower();

                    // Check if all search words are present in the column value
                    bool allWordsFound = searchWords.All(word => columnValue.Contains(word));

                    // Show or hide the row based on the search result
                    var row = (DataGridRow)profesorDataGrid.ItemContainerGenerator.ContainerFromItem(item);
                    if (row != null)
                        row.Visibility = allWordsFound ? Visibility.Visible : Visibility.Collapsed;
                }
            }
            else if(selectedTab.Header.ToString() == "Predmeti")
            {
                foreach (Predmet item in predmetDataGrid.Items)
                {
                    string columnValue = item.Sifra.ToLower() + item.Naziv.ToLower();

                    // Check if all search words are present in the column value
                    bool allWordsFound = searchWords.All(word => columnValue.Contains(word));

                    // Show or hide the row based on the search result
                    var row = (DataGridRow)predmetDataGrid.ItemContainerGenerator.ContainerFromItem(item);
                    if (row != null)
                        row.Visibility = allWordsFound ? Visibility.Visible : Visibility.Collapsed;
                }
            }
            else if (selectedTab.Header.ToString() == "Studenti")
            {
                foreach (Student item in studentDataGrid.Items)
                {
                    string columnValue = item.Ime.ToLower() + item.Prezime.ToLower() + item.Indeks.ToLower();

                    // Check if all search words are present in the column value
                    bool allWordsFound = searchWords.All(word => columnValue.Contains(word));

                    // Show or hide the row based on the search result
                    var row = (DataGridRow)studentDataGrid.ItemContainerGenerator.ContainerFromItem(item);
                    if (row != null)
                        row.Visibility = allWordsFound ? Visibility.Visible : Visibility.Collapsed;
                }
            }
        }
    }
}
