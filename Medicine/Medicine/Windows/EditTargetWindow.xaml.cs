using DevExpress.Xpf.Core;
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
    /// Interaction logic for EditTargetWindow.xaml
    /// </summary>
    public partial class EditTargetWindow : ThemedWindow
    {
        private Target _editTarget;
        private bool _isAdd => this._editTarget.Id == 0;
        private List<Target> _targetList = new List<Target>();
        private Target _selectedParent => this.ceParentGroup.SelectedItem as Target;
        public EditTargetWindow()
        {
            InitializeComponent();
        }

        public static bool? Execute(Target target)
        {
            var window = new EditTargetWindow();
            window._editTarget = target;
            window.Title = window._isAdd ? "Новая группа" : "Редактирование группы";
            return window.ShowDialog();
        }

        private void sbCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void sbSave_Click(object sender, RoutedEventArgs e)
        {
            this._editTarget.Name = this.teName.Text;
            this._editTarget.Description = this.teDescription.Text;
            this._editTarget.ParentId = this._selectedParent.Id == 0
                ? (int?)null
                : this._selectedParent.Id;

            if (this._isAdd)
            {
                App.Context.Targets.Add(this._editTarget);
            }

            App.Context.SaveChanges();
            this.DialogResult = true;
        }

        private void ThemedWindow_Loaded(object sender, RoutedEventArgs e)
        {
            this._targetList = App.Context.Targets
                .Where(x => x.TemplateId == this._editTarget.TemplateId
                && x.Id != this._editTarget.Id)
                .ToList();

            var rootGroup = new Target { Id = 0, Name = "Корневая группа" };
            this._targetList.Insert(0, rootGroup);

            this.ceParentGroup.ItemsSource = this._targetList;
            this.teName.EditValue = this._editTarget.Name;
            this.teDescription.EditValue = this._editTarget.Description;
            this.ceParentGroup.SelectedItem = this._targetList
                .Find(x => x.Id == this._editTarget.ParentId) ?? rootGroup;
        }
    }
}
