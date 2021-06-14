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
        private List<Target> _targetList = new List<Target>();
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

            this._checkLists = App.Context.CheckLists
                .Where(x => x.ItemId == this._editItem.Id)
                .ToList();

            this.ceGroup.ItemsSource = this._checkLists;

            this._targetList = App.Context.Targets
               .Where(x => x.TemplateId == this._editItem.TemplateId)
               .ToList();

            this._targetList.ForEach(x => x.IsChecked = false);

            this.tlcTargets.ItemsSource = this._targetList;
            this.tlvTargets.ExpandAllNodes();
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
            if (!this.teGroup.DoValidate())
                return;

            var editCheckList = this._isAddCheckListMode
                ? new CheckList { ItemId = this._editItem.Id }
                : this._selectedCheckList;

            editCheckList.Name = this.teGroup.Text;

            if (this._isAddCheckListMode)
                App.Context.CheckLists.Add(editCheckList);

            App.Context.SaveChanges();

            if (this._isAddCheckListMode)
                this._checkLists.Add(editCheckList);

            this.ceGroup.RefreshData();
            this.ceGroup.SelectedItem = editCheckList;

            this.lgSelectMode.Visibility = Visibility.Visible;
            this.lgEditMode.Visibility = Visibility.Collapsed;
            RefreshBorders();
        }

        private void sbGroupCancel_Click(object sender, RoutedEventArgs e)
        {
            this.lgSelectMode.Visibility = Visibility.Visible;
            this.lgEditMode.Visibility = Visibility.Collapsed;
        }

        private void sbEditGroup_Click(object sender, RoutedEventArgs e)
        {
            if (!this.teGroup.DoValidate())
                return;

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
                App.Context.CheckLists.Remove(this._selectedCheckList);
                App.Context.SaveChanges();

                this._checkLists.Remove(this._selectedCheckList);
                this.ceGroup.RefreshData();

                var firstCheckList = this._checkLists.FirstOrDefault();
                if (firstCheckList is null)
                    this.sbEditGroup.IsEnabled = this.sbDeleteGroup.IsEnabled = false;

                this.ceGroup.SelectedItem = firstCheckList;
                RefreshBorders();
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

            this.RefreshBorders();
        }

        private void sbCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void sbSave_Click(object sender, RoutedEventArgs e)
        {
            if (!this.teName.DoValidate())
                return;

            if (this._selectedType.Id == 0 && !this.teMeasureUnit.DoValidate())
                return;

            if (this._selectedType.Id == 1 && this._checkLists.Count == 0)
            {
                MessageBox.Show("Заполните список хотя бы одним значением", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            this._editItem.TypeId = this._selectedType.Id;
            this._editItem.CheckLists = this._checkLists;
            this._editItem.MeasureUnit = this.teMeasureUnit.Text;
            this._editItem.Name = this.teName.EditValue.ToString();

            App.Context.SaveChanges();

            this.DialogResult = true;
        }

        private Target _selectedTarget => this.tlcTargets.SelectedItem as Target;

        private void tlcTargets_SelectedItemChanged(object sender, DevExpress.Xpf.Grid.SelectedItemChangedEventArgs e)
        {
            var cells = this.tlvTargets.GetSelectedCells();
            for (var i = 0; i < cells.Count(); i++)
            {
                var column = cells[i].Column;
                var rowHandle = cells[i].RowHandle;
                tlvTargets.UnselectCell(rowHandle, column);
            }

            this._targetList.ForEach(x => x.IsChecked = false);

            if (this._selectedTarget != null)
            {
                GetLowTargets(this._selectedTarget);
                GetTopTargets(this._selectedTarget);
                this._selectedTarget.IsChecked = true;
            }

            this.tlcTargets.RefreshData();
            this.RefreshBorders();

            void GetLowTargets(Target target)
            {
                var children = this._targetList.FirstOrDefault(x => x.ParentId == target.Id);
                if (children != null)
                {
                    children.IsChecked = true;
                    GetLowTargets(children);
                }
            }

            void GetTopTargets(Target target)
            {
                var parent = this._targetList.FirstOrDefault(x => x.Id == target.ParentId);
                if (parent != null)
                {
                    parent.IsChecked = true;
                    GetTopTargets(parent);
                }
            }
        }

        private void tsForAll_Click(object sender, RoutedEventArgs e)
        {
            if (this.tsForAll.IsChecked.Value)
            {
                this._targetList.ForEach(x => x.IsChecked = true);
                this.tlvTargets.IsEnabled = false;
                this.tlcTargets.RefreshData();
                this.RefreshBorders();
            }
            else
            {
                if (this.tlcTargets.SelectedItem != null)
                    this.tlcTargets.SelectedItem = null;
                else
                {
                    this._targetList.ForEach(x => x.IsChecked = false);
                    this.tlcTargets.RefreshData();
                    this.RefreshBorders();
                }

                this.tlvTargets.IsEnabled = true;
            }
        }

        private void teMeasureUnit_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {
            this.liNormalMeasureUnit.Label =
                this.liWarningMeasureUnit.Label = string.IsNullOrWhiteSpace(this.teMeasureUnit.Text)
                ? "ед. измерения" : this.teMeasureUnit.Text;
        }

        private void RefreshBorders()
        {
            this.spNumberNormal.Visibility = Visibility.Collapsed;
            this.spNumberWarning.Visibility = Visibility.Collapsed;
            this.spListNormal.Visibility = Visibility.Collapsed;
            this.spListWarning.Visibility = Visibility.Collapsed;
            this.lblNotSelected.Visibility = Visibility.Collapsed;
            this.lblHeader.Visibility = Visibility.Visible;
            this.sbSaveChanges.Visibility = Visibility.Visible;

            var checkedTargets = this._targetList
                .Where(x => x.IsChecked).ToList();

            if (checkedTargets.Count == 0)
            {
                this.lblNotSelected.Visibility = Visibility.Visible;
                this.lblHeader.Visibility = Visibility.Collapsed;
                this.sbSaveChanges.Visibility = Visibility.Collapsed;
                return;
            }

            var borderList = App.Context.Borders
                .Where(x => x.ItemId == this._editItem.Id)
                .ToList();

            var target = checkedTargets.FirstOrDefault(x => checkedTargets.FindAll(t => t.ParentId == x.Id).Count == 0);
            var border = borderList.Find(x => x.Id == target?.BorderId);

            if (this._selectedType.Id == 0)
            {
                this.spNumberNormal.Visibility = Visibility.Visible;
                this.spNumberWarning.Visibility = Visibility.Visible;

                this.seMinNormal.EditValue = border?.NormalMin ?? 0;
                this.seMaxNormal.EditValue = border?.NormalMax ?? 0;
                this.seMinWarning.EditValue = border?.WarningMin ?? 0;
                this.seMaxWarning.EditValue = border?.WarningMax ?? 0;
            }
            else
            {
                this.spListNormal.Visibility = Visibility.Visible;
                this.spListWarning.Visibility = Visibility.Visible;

                var list = this._checkLists.ToList();
                this.ceNormal.ItemsSource = list;
                this.ceWarning.ItemsSource = list;

                var nortmalItem = border?.NormalItem is null ? list.FirstOrDefault() : list.Find(x => x.Id == border.NormalItem);
                var warningItem = border?.WarningItem is null ? list.FirstOrDefault() : list.Find(x => x.Id == border.WarningItem);
                this.ceNormal.SelectedItem = nortmalItem;
                this.ceWarning.SelectedItem = warningItem;
            }
        }

        private CheckList _selectedNormalItem => this.ceNormal.SelectedItem as CheckList;
        private CheckList _selectedWarningItem => this.ceWarning.SelectedItem as CheckList;
        private void sbSaveChanges_Click(object sender, RoutedEventArgs e)
        {
            var borderList = App.Context.Borders
                .Where(x => x.ItemId == this._editItem.Id)
                .ToList();

            var checkedTargets = this._targetList
                .Where(x => x.IsChecked).ToList();

            foreach (var target in checkedTargets)
            {
                var border = borderList.Find(x => x.Id == target.BorderId);
                if (border is null)
                {
                    if (this._selectedType.Id == 0)
                    {
                        border = new Data.Entities.Border
                        {
                            ItemId = this._editItem.Id,
                            NormalMin = this.seMinNormal.Value,
                            NormalMax = this.seMaxNormal.Value,
                            WarningMin = this.seMinWarning.Value,
                            WarningMax = this.seMaxWarning.Value,
                            NormalItem = null,
                            WarningItem = null
                        };

                        App.Context.Borders.Add(border);
                        App.Context.SaveChanges();
                    }
                    else
                    {
                        border = new Data.Entities.Border
                        {
                            ItemId = this._editItem.Id,
                            NormalMin = 0,
                            NormalMax = 0,
                            WarningMin = 0,
                            WarningMax = 0,
                            NormalItem = this._selectedNormalItem?.Id,
                            WarningItem = this._selectedWarningItem?.Id
                        };

                        App.Context.Borders.Add(border);
                        App.Context.SaveChanges();
                    }
                }
                else
                {
                    if (this._selectedType.Id == 0)
                    {
                        border.NormalMin = this.seMinNormal.Value;
                        border.NormalMax = this.seMaxNormal.Value;
                        border.WarningMin = this.seMinWarning.Value;
                        border.WarningMax = this.seMaxWarning.Value;
                        border.NormalItem = null;
                        border.WarningItem = null;
                    }
                    else
                    {
                        border.ItemId = this._editItem.Id;
                        border.NormalMin = 0;
                        border.NormalMax = 0;
                        border.WarningMin = 0;
                        border.WarningMax = 0;
                        border.NormalItem = this._selectedNormalItem?.Id;
                        border.WarningItem = this._selectedWarningItem?.Id;
                    }
                }

                target.BorderId = border.Id;
            }

            App.Context.SaveChanges();
            MessageBox.Show("Данные успешно сохранены", "Сообщение", MessageBoxButton.OK, MessageBoxImage.Information);
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

        bool measure = true;
        private void teMeasureUnit_Validate(object sender, DevExpress.Xpf.Editors.ValidationEventArgs e)
        {
            if (measure)
            {
                measure = false;
                return;
            }

            if (!string.IsNullOrWhiteSpace(e.Value?.ToString()))
                return;

            e.IsValid = false;
            e.ErrorType = DevExpress.XtraEditors.DXErrorProvider.ErrorType.Critical;
            e.ErrorContent = "Ед. измерения обязательно для заполнения";
        }

        int group = 0;
        private void teGroup_Validate(object sender, DevExpress.Xpf.Editors.ValidationEventArgs e)
        {
            if (group < 2)
            {
                group++;
                return;
            }

            if (!string.IsNullOrWhiteSpace(e.Value?.ToString()))
                return;

            e.IsValid = false;
            e.ErrorType = DevExpress.XtraEditors.DXErrorProvider.ErrorType.Critical;
            e.ErrorContent = "Обязательно для заполнения";
        }
    }
}
