using DevExpress.Xpf.Printing;
using DevExpress.Xpf.WindowsUI;
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
using System.Windows.Shapes;

namespace Medicine.Pages
{
    /// <summary>
    /// Interaction logic for HistoryPage.xaml
    /// </summary>
    public partial class HistoryPage : NavigationPage
    {
        private List<History> _historyList = new List<History>();
        private History _selectedHistory => this.gcHistory.SelectedItem as History;
        public HistoryPage()
        {
            InitializeComponent();
        }

        private void sbAdd_Click(object sender, RoutedEventArgs e)
        {
            var newHistory = new History();
            if (EditHistoryWindow.Execute(newHistory) == true)
            {
                this.RefreshData();
            }
        }

        private void NavigationPage_Loaded(object sender, RoutedEventArgs e)
        {
            this.dpFrom.SelectedDate = DateTime.Now.Date.AddYears(-1);
            this.dpTo.SelectedDate = DateTime.Now.Date;
            this._defaultPatient = new Patient { Id = 0, LastName = "Все" };
            this._defaultTemplate = new Template { Id = 0, Name = "Все" };
            var patientList = new List<Patient>()
            {
                _defaultPatient
            };
            patientList.AddRange(App.Context.Patients.ToList());
            this.cePatient.ItemsSource = patientList;

            var templateList = new List<Template>()
            {
                _defaultTemplate
            };
            templateList.AddRange(App.Context.Templates.ToList()); ;
            this.ceTemplate.ItemsSource = templateList;

            this.RefreshData();
        }


        private Patient _defaultPatient;
        private Template _defaultTemplate;
        private DateTime from => this.dpFrom.SelectedDate ?? DateTime.MinValue;
        private DateTime to => this.dpTo.SelectedDate ?? DateTime.MaxValue;
        private Patient patient => this.cePatient.SelectedItem as Patient;
        private Template template => this.ceTemplate.SelectedItem as Template;
        private void RefreshData()
        {
            var historyId = this._selectedHistory?.Id;
            var allHistories = App.Context.Histories.ToList();
            this._historyList = allHistories
                .FindAll(x => x.CreateDate >= this.from && x.CreateDate <= this.to
                && ((this.patient is null || this.patient.Id == 0) || x.PatientId == this.patient.Id)
                && ((this.template is null || this.template.Id == 0) || x.TemplateExt?.Id == this.template.Id));

            this.gcHistory.ItemsSource = this._historyList;
            this.gcHistory.RefreshData();

            if (historyId.HasValue)
                this.gcHistory.SelectedItem = this._historyList.Find(x => x.Id == historyId);

        }

        private void sbApply_Click(object sender, RoutedEventArgs e)
        {
            this.RefreshData();
        }

        private void sbReset_Click(object sender, RoutedEventArgs e)
        {
            this.dpFrom.SelectedDate = DateTime.Now.Date.AddYears(-1);
            this.dpTo.SelectedDate = DateTime.Now.Date;
            this.cePatient.SelectedItem = this._defaultPatient;
            this.ceTemplate.SelectedItem = this._defaultTemplate;

            this.RefreshData();
        }

        private void gcHistory_SelectedItemChanged(object sender, DevExpress.Xpf.Grid.SelectedItemChangedEventArgs e)
        {
            this.sbEdit.IsEnabled =
                this.sbPrint.IsEnabled =
                this.sbDelete.IsEnabled = this._selectedHistory != null;
        }

        private void sbEdit_Click(object sender, RoutedEventArgs e)
        {
            if (EditHistoryWindow.Execute(this._selectedHistory) == true)
                this.gcHistory.RefreshData();
        }

        private void sbDelete_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Удалить анализ из истории?"
                , "Подтверждение"
                , MessageBoxButton.YesNo
                , MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                App.Context.Histories.Remove(this._selectedHistory);
                App.Context.SaveChanges();

                this._historyList.Remove(this._selectedHistory);
                this.gcHistory.RefreshData();
                this.gcHistory.SelectedItem = this._historyList.FirstOrDefault();
            }
        }

        private void sbPrint_Click(object sender, RoutedEventArgs e)
        {
            var report = HistoryReport.CreateReport(this._selectedHistory);
            PrintHelper.ShowRibbonPrintPreview(this, report);
        }
    }
}
