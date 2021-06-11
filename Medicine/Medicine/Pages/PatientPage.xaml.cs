using DevExpress.Xpf.WindowsUI;
using Medicine.Data;
using Medicine.Data.Entities;
using Medicine.Windows;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Medicine.Pages
{
    /// <summary>
    /// Interaction logic for PatientPage.xaml
    /// </summary>
    public partial class PatientPage : NavigationPage
    {
        private List<Patient> _patientList;
        private Patient _currentPatient => this.gcPatients.CurrentItem as Patient;
        public PatientPage()
        {
            InitializeComponent();
        }

        private void SimpleButton_Click(object sender, RoutedEventArgs e)
        {
            var patient = new Patient();
            if (EditPatientWindow.Execute(patient) == true)
            {
                this._patientList.Add(patient);
                this.gcPatients.RefreshData();
            }
        }

        private void NavigationPage_Loaded(object sender, RoutedEventArgs e)
        {
            this._patientList = App.Context.Patients.ToList();

            this.gcPatients.ItemsSource = this._patientList;
        }

        private void gcPatients_SelectedItemChanged(object sender, DevExpress.Xpf.Grid.SelectedItemChangedEventArgs e)
        {
            this.sbEdit.IsEnabled =
            this.sbDelete.IsEnabled =
            this._currentPatient != null;
        }

        private void sbDelete_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Удалить выбранного пациента?"
                , "Подтверждение"
                , MessageBoxButton.YesNo
                , MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                App.Context.Patients.Remove(this._currentPatient);
                App.Context.SaveChanges();

                this._patientList.Remove(this._currentPatient);
                this.gcPatients.RefreshData();
            }
        }

        private void sbEdit_Click(object sender, RoutedEventArgs e)
        {
            if (EditPatientWindow.Execute(this._currentPatient) == true)
            {
                var rowHandle = this.gcPatients.GetSelectedRowHandles();
                this.gcPatients.RefreshRow(rowHandle[0]);
            }
        }
    }
}
