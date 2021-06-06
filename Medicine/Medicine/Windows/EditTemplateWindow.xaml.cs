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
            using (var context = new DataContext())
            {
                this._groupList = context.Groups.Include("Templates").ToList();
            };

            var notSelected = new TemplateGroup { Id = 0, Name = "Не выбрано" };
            this._groupList.Add(notSelected);
            this.ceGroup.ItemsSource = this._groupList;

            this.ceGroup.SelectedItem = this._editTemplate.Group is null
                ? notSelected
                : this._groupList
                .Find(x => x.Id == this._editTemplate.Group.Id);

            this.teName.EditValue = this._editTemplate.Name;
        }

        private void sbSave_Click(object sender, RoutedEventArgs e)
        {
            this._editTemplate.Name = this.teName.EditValue.ToString();

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

            using (var context = new DataContext())
            {
                if (this._isAdd)
                    context.Templates.Add(this._editTemplate);
                else
                    context.Entry(this._editTemplate).State = System.Data.Entity.EntityState.Modified;

                context.SaveChanges();
            }

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
            editGroup.Name = this.teGroup.EditValue.ToString();

            using (var context = new DataContext())
            {
                if (this._isAddGroupMode)
                    context.Groups.Add(editGroup);
                else
                    context.Entry(editGroup).State = System.Data.Entity.EntityState.Modified;

                context.SaveChanges();
            }

            if (this._isAddGroupMode)
                this._groupList.Add(editGroup);

            this.ceGroup.RefreshData();
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
            if (MessageBox.Show("Удалить выбранную группу?"
                , "Подтверждение"
                , MessageBoxButton.YesNo
                , MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                using (var context = new DataContext())
                {
                    //this._selectedGroup.GroupId = null;
                    //this._selectedTemplate.Group = null;
                    context.Entry(this._selectedGroup).State = System.Data.Entity.EntityState.Deleted;
                    context.SaveChanges();
                }

                this._groupList.Remove(this._selectedGroup);
                this.ceGroup.RefreshData();
                this.ceGroup.SelectedItem = this._groupList.FirstOrDefault();
            }
        }
    }
}
