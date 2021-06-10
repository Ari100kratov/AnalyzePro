using DevExpress.Xpf.Core;
using Medicine.Common;
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
    /// Interaction logic for EditItemWindow.xaml
    /// </summary>
    public partial class EditItemWindow : ThemedWindow
    {
        private Item _editItem;
        private ItemTypeModel _selectedType => this.ceType.SelectedItem as ItemTypeModel;

        private CheckList _selectedCheckList => this.ceGroup.SelectedItem as CheckList;
        private bool _isAddCheckListMode = false;
        private List<CheckList> _checkLists = new List<CheckList>();
        public EditItemWindow()
        {
            InitializeComponent();
        }

        public static bool? Execute(Item item)
        {
            var editWindow = new EditItemWindow();
            editWindow._editItem = item;
            editWindow.Title = "Редактирование параметра";
            return editWindow.ShowDialog();
        }

        private void ThemedWindow_Loaded(object sender, RoutedEventArgs e)
        {
            var typeList = ItemType.List;
            this.ceType.ItemsSource = typeList;
            this.ceType.SelectedItem = typeList.FirstOrDefault(x => x.Id == this._editItem.TypeId);
            this.teName.EditValue = this._editItem.Name;
            this.teMeasureUnit.EditValue = this._editItem.MeasureUnit;

            using (var context = new DataContext())
            {
                this._checkLists = context.CheckLists
                    .Where(x => x.ItemId == this._editItem.Id)
                    .ToList();
            }

            this.ceGroup.ItemsSource = this._checkLists;
        }

        private void ceGroup_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {
            this.sbEditGroup.IsEnabled =
                this.sbDeleteGroup.IsEnabled = this._selectedCheckList != null;
        }

        private void sbAddGroup_Click(object sender, RoutedEventArgs e)
        {
            this.lgSelectMode.Visibility = Visibility.Collapsed;
            this.lgEditMode.Visibility = Visibility.Visible;
            this.teGroup.EditValue = null;
            this._isAddCheckListMode = true;
        }

        private void sbGroupSave_Click(object sender, RoutedEventArgs e)
        {
            var editCheckList = this._isAddCheckListMode
                ? new CheckList { ItemId = this._editItem.Id }
                : this._selectedCheckList;

            editCheckList.Name = this.teGroup.Text;

            using (var context = new DataContext())
            {
                context.CheckLists.Attach(editCheckList);

                if (this._isAddCheckListMode)
                    context.CheckLists.Add(editCheckList);
                else
                    context.Entry(editCheckList).State = System.Data.Entity.EntityState.Modified;

                context.SaveChanges();
            }

            if (this._isAddCheckListMode)
                this._checkLists.Add(editCheckList);

            this.ceGroup.RefreshData();
            this.ceGroup.SelectedItem = editCheckList;

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
            this._isAddCheckListMode = false;

            this.teGroup.EditValue = this._selectedCheckList.Name;
        }

        private void sbDeleteGroup_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Удалить выбранный вариант?"
                , "Подтверждение"
                , MessageBoxButton.YesNo
                , MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                using (var context = new DataContext())
                {
                    context.CheckLists.Attach(this._selectedCheckList);
                    context.CheckLists.Remove(this._selectedCheckList);
                    context.SaveChanges();
                }

                this._checkLists.Remove(this._selectedCheckList);
                this.ceGroup.RefreshData();

                var firstCheckList = this._checkLists.FirstOrDefault();
                if (firstCheckList is null)
                    this.sbEditGroup.IsEnabled = this.sbDeleteGroup.IsEnabled = false;

                this.ceGroup.SelectedItem = firstCheckList;
            }
        }

        private void ceType_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {
            this.lgCheckList.Visibility = this._selectedType.Id == 1
                ? Visibility.Visible
                : Visibility.Collapsed;

            this.liMeasureUnit.Visibility = this._selectedType.Id == 0
                ? Visibility.Visible
                : Visibility.Collapsed;
        }

        private void sbCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void sbSave_Click(object sender, RoutedEventArgs e)
        {
            this._editItem.TypeId = this._selectedType.Id;
            this._editItem.CheckLists = this._checkLists;
            this._editItem.MeasureUnit = this.teMeasureUnit.Text;
            this._editItem.Name = this.teName.EditValue.ToString();

            using (var context = new DataContext())
            {
                context.Items.Attach(this._editItem);
                context.Entry(this._editItem).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }

            this.DialogResult = true;
        }
    }
}
