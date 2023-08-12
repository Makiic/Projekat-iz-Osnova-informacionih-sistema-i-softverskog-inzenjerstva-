using Oisisiproj.Model;
using Oisisiproj.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
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
    /// Interaction logic for UpdateSefKatedre.xaml
    /// </summary>
    public partial class UpdateSefKatedre : Window
    {
       
        private readonly KatedraRepository _katRepository;
        private readonly ProfesorRepository _profesorRepository;
        public Profesor SelectedProfesor { get; set; }
        public Katedra SelectedDepartment { get; set; }

        public List<Profesor> Zadovoljava { get; set; }
        public Window Parent { get; set; }
        public UpdateSefKatedre(Profesor selectedProfesor, Katedra selectedKatedra)
        {
            InitializeComponent();
            DataContext = this;
            _katRepository = new KatedraRepository();
            _profesorRepository = new ProfesorRepository();
            
            SelectedProfesor = selectedProfesor;
            SelectedDepartment = selectedKatedra;
            int sefId = _katRepository.NadjiIdSefaKatedre(SelectedDepartment.Id);

            Zadovoljava = new List<Profesor>();

            foreach (Profesor profesor in _profesorRepository.GetAll())
            {
                if ((profesor.Zvanje == "REDOVNI_PROFESOR" || profesor.Zvanje == "VANREDNI_PROFESOR") && profesor.GodineStaza >= 5 && sefId != profesor.Id)
                {
                    Zadovoljava.Add(profesor);
                }
            }

        }


        private void AddDepartmentHead_Click(object sender, RoutedEventArgs e)
        {
            int katId = SelectedDepartment.Id;
            int professorId = SelectedProfesor.Id;
            SelectedDepartment.IdSefKatedre = professorId;
          
            _katRepository.Update(SelectedDepartment); // Save the changes to the CSV file

            UpdateKatedru updateDepartment = (UpdateKatedru)this.Parent;
            updateDepartment.SelectedProfesor = SelectedProfesor;

            // Use Dispatcher to update UI controls on the main UI thread
            Application.Current.Dispatcher.Invoke(() =>
            {
                updateDepartment.RefreshData();
            });

            Close();
        }





        private void CancelAddDepartmentHead_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
