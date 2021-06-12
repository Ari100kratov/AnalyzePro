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
    /// Interaction logic for EditTemplateWindow.xaml
    /// </summary>
    public partial class EditTemplateWindow : ThemedWindow
    {
        private bool _isAddGroupMode = false;

        private List<TemplateGroup> _groupList;
        private Template _editTemplate;
        private bool _isAdd => this._editTemplate.Id == 0;
        private TemplateGroup _selectedGroup => this.ceGroup.SelectedItem as TemplateGroup;
        public EditTemplateWindow()
        {
            InitializeComponent();
        }

        public static bool? Execute(Template template)
        {
            var editWindow = new EditTemplateWindow();
            editWindow._editTemplate = template;
            editWindow.Title = editWindow._isAdd ? "Новый шаблон" : "Изменение шаблона";
            return editWindow.ShowDialog();
        }

        private void sbCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void ThemedWindow_Loaded(object sender, RoutedEventArgs e)
        {
            this._groupList = App.Context.Groups.Include("Templates").ToList();

            var notSelected = new TemplateGroup { Id = 0, Name = "Не выбрано" };
            this._groupList.Add(notSelected);
            this.ceGroup.ItemsSource = this._groupList;

            this.ceGroup.SelectedItem = this._editTemplate.Group is null
                ? notSelected
                : this._editTemplate.Group;

            this.teName.EditValue = this._editTemplate.Name;

            this._targetList = App.Context.Targets.Where(x => x.TemplateId == this._editTemplate.Id).ToList();
            this.tlcTargets.ItemsSource = this._targetList;
        }

        private void sbSave_Click(object sender, RoutedEventArgs e)
        {
            if (!this.teName.DoValidate())
                return;

            this._editTemplate.Name = this.teName.Text;

            if (this._selectedGroup.Id == 0)
            {
                this._editTemplate.GroupId = null;
                this._editTemplate.Group = null;
            }
            else
            {
                this._editTemplate.GroupId = this._selectedGroup.Id;
                this._editTemplate.Group = this._selectedGroup;
            }

            if (this._isAdd)
                App.Context.Templates.Add(this._editTemplate);

            App.Context.SaveChanges();

            this.DialogResult = true;
        }

        private void ceGroup_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {
            this.sbEditGroup.IsEnabled =
                this.sbDeleteGroup.IsEnabled = this._selectedGroup != null && this._selectedGroup.Id > 0;
        }

        private void sbAddGroup_Click(object sender, RoutedEventArgs e)
        {
            this.lgSelectMode.Visibility = Visibility.Collapsed;
            this.lgEditMode.Visibility = Visibility.Visible;
            this.teGroup.EditValue = null;
            this._isAddGroupMode = true;
        }

        private void sbGroupSave_Click(object sender, RoutedEventArgs e)
        {
            var editGroup = this._isAddGroupMode ? new TemplateGroup() : this._selectedGroup;
            editGroup.Name = this.teGroup.Text;

            if (this._isAddGroupMode)
                App.Context.Groups.Add(editGroup);

            App.Context.SaveChanges();

            if (this._isAddGroupMode)
                this._groupList.Add(editGroup);

            this.ceGroup.RefreshData();
            this.ceGroup.SelectedItem = editGroup;

            this.lgSelectMode.Visibility = Visibility.Visible;
            this.lgEditMode.Visibility = Visibility.Collapsed;
        }

        private void sbGroupCancel_Click(object sender, RoutedEventArgs e)
        {
            this.lgSelectMode.Visibility = Visibility.Visible;
            this.lgEditMode.Visibility = Visibility.Collapsed;
        }

        private void sbEditGroup_Click(object sender, RoutedEventArgs e)
        {
            this.lgSelectMode.Visibility = Visibility.Collapsed;
            this.lgEditMode.Visibility = Visibility.Visible;
            this._isAddGroupMode = false;

            this.teGroup.EditValue = this._selectedGroup.Name;
        }

        private void sbDeleteGroup_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Удалить выбранную группу шаблонов?"
                , "Подтверждение"
                , MessageBoxButton.YesNo
                , MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                App.Context.Groups.Remove(this._selectedGroup);
                App.Context.SaveChanges();

                this._groupList.Remove(this._selectedGroup);
                this.ceGroup.RefreshData();
                this.ceGroup.SelectedItem = this._groupList.FirstOrDefault();
            }
        }

        private List<Target> _targetList = new List<Target>();
        private Target _selectedTarget => this.tlcTargets.SelectedItem as Target;

        private void sbAddTarget_Click(object sender, RoutedEventArgs e)
        {
            var newTarget = new Target { TemplateId = this._editTemplate.Id };
            if (EditTargetWindow.Execute(newTarget) == true)
            {
                this._targetList.Add(newTarget);
                this.tlcTargets.RefreshData();
            }
        }

        private void sbEditTarget_Click(object sender, RoutedEventArgs e)
        {
            if (EditTargetWindow.Execute(this._selectedTarget) == true)
                this.tlcTargets.RefreshData();
        }

        private void sbDeleteTarget_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Удалить выбранную целевую группу?"
                , "Подтверждение"
                , MessageBoxButton.YesNo
                , MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                App.Context.Targets.Remove(this._selectedTarget);
                App.Context.SaveChanges();

                this._targetList.Remove(this._selectedTarget);
                this.tlcTargets.RefreshData();
                this.tlcTargets.SelectedItem = this._targetList.FirstOrDefault();
            }
        }

        private void tlcTargets_SelectedItemChanged(object sender, DevExpress.Xpf.Grid.SelectedItemChangedEventArgs e)
        {
            this.sbEditTarget.IsEnabled =
                this.sbDeleteTarget.IsEnabled = this._selectedTarget != null;
        }

        bool name = true;
        private void teName_Validate(object sender, DevExpress.Xpf.Editors.ValidationEventArgs e)
        {
            if (name)
            {
                name = false;
                return;
            }

            if (!string.IsNullOrWhiteSpace(e.Value?.ToString()))
                return;

            e.IsValid = false;
            e.ErrorType = DevExpress.XtraEditors.DXErrorProvider.ErrorType.Critical;
            e.ErrorContent = "Наименование обязательно для заполнения";
        }
    }
}
