using DevExpress.Xpf.Core;
using Medicine.Data;
using Medicine.Data.Entities;
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

namespace Medicine.Windows
{
    /// <summary>
    /// Interaction logic for EditPatientWindow.xaml
    /// </summary>
    public partial class EditPatientWindow : ThemedWindow
    {
        private Patient _patient;
        private bool _isAdd => this._patient.Id == 0;

        public EditPatientWindow()
        {
            InitializeComponent();
        }

        public static bool? Execute(Patient patient)
        {
            var editWindow = new EditPatientWindow();
            editWindow._patient = patient ?? new Patient();

            return editWindow.ShowDialog();
        }

        private void ThemedWindow_Loaded(object sender, RoutedEventArgs e)
        {
            this.Title = this._isAdd ? "Создание пациента" : "Редактирование пациента";

            if (this._isAdd)
                return;

            this.iePhoto.EditValue = this._patient.Photo;
            this.teFirstName.Text = this._patient.FirstName;
            this.teLastName.Text = this._patient.LastName;
            this.teMiddleName.Text = this._patient.MiddleName;
            this.tePhone.EditValue = this._patient.PhoneNumber;
            this.teOtherPhone.EditValue = this._patient.OtherPhoneNumber;
            this.dtBirth.EditValue = this._patient.BirthDate;
            this.ceFemale.IsChecked = this._patient.GenderId == 1;
            this.ceMail.IsChecked = this._patient.GenderId == 0;
            this.teRegAddress.EditValue = this._patient.RegAddress;
            this.teResAddress.EditValue = this._patient.ResAddress;
        }

        private void ceMail_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {
            this.ceFemale.IsChecked = !this.ceMail.IsChecked;
        }

        private void sbCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void ceFemale_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {
            this.ceMail.IsChecked = !this.ceFemale.IsChecked;
        }

        private void sbSave_Click(object sender, RoutedEventArgs e)
        {
            this._patient.FirstName = this.teFirstName.Text;
            this._patient.LastName = this.teLastName.Text;
            this._patient.MiddleName = this.teMiddleName.Text;
            this._patient.PhoneNumber = this.tePhone.Text;
            this._patient.OtherPhoneNumber = this.teOtherPhone.Text;
            this._patient.BirthDate = this.dtBirth.DateTime;
            this._patient.RegAddress = this.teRegAddress.Text;
            this._patient.ResAddress = this.teResAddress.Text;
            this._patient.GenderId = (bool)this.ceMail.IsChecked ? 0 : 1;
            this._patient.Photo = this.iePhoto.EditValue as byte[];

            using (var context = new DataContext())
            {
                if (this._isAdd)
                    context.Patients.Add(this._patient);
                else
                    context.Entry(this._patient).State = System.Data.Entity.EntityState.Modified;

                context.SaveChanges();
            }

            this.DialogResult = true;
        }
    }
}
