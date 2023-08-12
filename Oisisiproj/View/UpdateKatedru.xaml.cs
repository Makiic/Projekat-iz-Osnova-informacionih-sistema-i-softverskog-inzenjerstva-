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
    /// Interaction logic for UpdateKatedru.xaml
    /// </summary>
    public partial class UpdateKatedru : Window,INotifyPropertyChanged
    {
        public MainWindow MainWindow { get; set; }
        private readonly KatedraRepository _katRepository;
        private readonly ProfesorRepository _profesorRepository;
        public Profesor profesor;
        Katedra SelectedDepartment = new Katedra();
        public Profesor SelectedProfesor { get; set; }

        public UpdateKatedru(MainWindow mainWindow,Katedra kat)
        {
            InitializeComponent();
            DataContext = this;

            SelectedDepartment = kat;
            _katRepository = new KatedraRepository();
            _profesorRepository = new ProfesorRepository();
            SifraKatedre = SelectedDepartment.SifraKatedre;
            NazivKatedre = SelectedDepartment.NazivKatedre;
            MainWindow = mainWindow;


            int sefId = SelectedDepartment.IdSefKatedre;
            Profesor profesor = _profesorRepository.GetById(sefId);

            if (profesor != null)
            {
                SelectedDepartment.SefKatedre = profesor;
                ImeProfesora = profesor.Ime;
                PrezimeProfesora = profesor.Prezime;
            }


        }


        private string _sifrakatedre;
        public string SifraKatedre
        {
            get => _sifrakatedre;
            set
            {
                if (value != _sifrakatedre)
                {
                    _sifrakatedre = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _nazivKatedre;
        public string NazivKatedre
        {
            get => _nazivKatedre;
            set
            {
                if (value != _nazivKatedre)
                {
                    _nazivKatedre = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _imeProfesora;
        public string ImeProfesora
        {
            get => _imeProfesora;
            set
            {
                if (value != _imeProfesora)
                {
                    _imeProfesora = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _prezimeProf;
        public string PrezimeProfesora
        {
            get => _prezimeProf;
            set
            {
                if (value != _prezimeProf)
                {
                    _prezimeProf = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        private void UpdateDepartmentHead_Click(object sender, RoutedEventArgs e)
        {

            Dispatcher.Invoke(() =>
            {

                UpdateSefKatedre updateDepartmentHead = new UpdateSefKatedre(SelectedProfesor, SelectedDepartment);
                updateDepartmentHead.Parent = this;
                updateDepartmentHead.ShowDialog();
             
            });

        }
        public void RefreshData()
        {
            ImeProfesora = SelectedProfesor?.Ime;
            PrezimeProfesora = SelectedProfesor?.Prezime;
        }



        private void UpdateDepartment_Click(object sender, RoutedEventArgs e)
        {
            
            Close();
        }
        private void CancelUpdateDepartment_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
