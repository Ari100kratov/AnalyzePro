using DevExpress.Xpf.Core;
using DevExpress.Xpf.Editors;
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
    /// Interaction logic for EditHistoryWindow.xaml
    /// </summary>
    public partial class EditHistoryWindow : ThemedWindow
    {
        private History _editHistory;
        private bool _isAdd => this._editHistory.Id == 0;

        private List<ResultData> resultDataList = new List<ResultData>();
        private List<Item> itemList = new List<Item>();
        public EditHistoryWindow()
        {
            InitializeComponent();
        }

        public static bool? Execute(History history)
        {
            var window = new EditHistoryWindow();
            window._editHistory = history;
            window.Title = window._isAdd ? "Заполнение результатов анализа" : "Изменение результатов анализа";
            return window.ShowDialog();
        }


        private List<Target> _targetList = new List<Target>();
        private Target _selectedTarget => this.tlcTargets.SelectedItem as Target;
        private void tlcTargets_SelectedItemChanged(object sender, DevExpress.Xpf.Grid.SelectedItemChangedEventArgs e)
        {
            this.RefreshTargets(this._selectedTarget);
        }

        private void RefreshTargets(Target setTarget)
        {
            var cells = this.tlvTargets.GetSelectedCells();
            for (var i = 0; i < cells.Count(); i++)
            {
                var column = cells[i].Column;
                var rowHandle = cells[i].RowHandle;
                tlvTargets.UnselectCell(rowHandle, column);
            }

            this._targetList.ForEach(x => x.IsChecked = false);


            if (setTarget != null)
            {
                GetLowTargets(setTarget);
                GetTopTargets(setTarget);
                setTarget.IsChecked = true;
            }


            this.tlcTargets.RefreshData();
            RefreshColors();

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

        private void ThemedWindow_Loaded(object sender, RoutedEventArgs e)
        {
            this.resultDataList = App.Context.ResultDatas
                .Where(x => x.HistoryId == this._editHistory.Id)
                .ToList();

            var item = App.Context.Items.Find(this.resultDataList.FirstOrDefault()?.ItemId);
            int templateId = item?.TemplateId ?? 0;

            var templateList = App.Context.Templates.ToList();
            var template = templateList.Find(x => x.Id == templateId)
                ?? templateList.FirstOrDefault();

            if (template != null)
                this.itemList = App.Context.Items.Where(x => x.TemplateId == template.Id).ToList();

            this.ceTemplate.ItemsSource = templateList;
            this.ceTemplate.SelectedItem = template;

            var patientList = App.Context.Patients.ToList();
            this.cePatient.ItemsSource = patientList;
            this.cePatient.SelectedItem = patientList
                .Find(x => x.Id == this._editHistory.PatientId)
                ?? patientList.FirstOrDefault();

            if (!this._isAdd)
                this.deCreate.EditValue = this._editHistory.CreateDate;

            this.tlcTargets.ItemsSource = this._targetList;
            this.tlvTargets.ExpandAllNodes();
        }


        private Patient _selectedPatient => this.cePatient.SelectedItem as Patient;
        private void cePatient_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {
            if (this._selectedPatient is null)
            {
                this.lblLastName.Content = "-";
                this.lblFirstName.Content = "-";
                this.lblMiddleName.Content = "-";
                this.iePhoto.EditValue = null;
                this.lblAge.Content = "-";
                this.lblGender.Content = "-";
            }
            else
            {
                this.lblLastName.Content = this._selectedPatient.LastName;
                this.lblFirstName.Content = this._selectedPatient.FirstName;
                this.lblMiddleName.Content = this._selectedPatient.MiddleName;
                this.iePhoto.EditValue = this._selectedPatient.PhotoExt;
                this.lblAge.Content = this._selectedPatient.Age;
                this.lblGender.Content = this._selectedPatient.Gender;
            }
        }

        bool patient = true;
        private void cePatient_Validate(object sender, DevExpress.Xpf.Editors.ValidationEventArgs e)
        {
            if (patient)
            {
                patient = false;
                return;
            }

            if (this._selectedPatient != null)
                return;

            e.IsValid = false;
            e.ErrorType = DevExpress.XtraEditors.DXErrorProvider.ErrorType.Critical;
            e.ErrorContent = "Выберите пациента, для которого проводился анализ";
        }

        int create = 0;
        private void deCreate_Validate(object sender, DevExpress.Xpf.Editors.ValidationEventArgs e)
        {
            if (create < 3)
            {
                create++;
                return;
            }

            if (!DateTime.TryParse(e.Value?.ToString(), out var date))
            {
                e.IsValid = false;
                e.ErrorType = DevExpress.XtraEditors.DXErrorProvider.ErrorType.Critical;
                e.ErrorContent = "Заполните дату проведения анализа";
                return;
            }

            if (date > DateTime.Now.Date)
            {
                e.IsValid = false;
                e.ErrorType = DevExpress.XtraEditors.DXErrorProvider.ErrorType.Critical;
                e.ErrorContent = "Дата не может быть больше текущей даты";
            }
        }

        private Template _selectedTemplate => this.ceTemplate.SelectedItem as Template;
        private void ceTemplate_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {
            this._rectDic.Clear();

            if (this.gridItems.RowDefinitions.Count > 2)
            {
                this.gridItems.Children.RemoveRange(2, this.gridItems.Children.Count - 2);
                this.gridItems.RowDefinitions.RemoveRange(2, this.gridItems.RowDefinitions.Count - 2);
            }

            if (this._selectedTemplate is null)
            {
                this._targetList = new List<Target>();
                this.tlcTargets.RefreshData();

                return;
            }

            this._targetList = App.Context.Targets
                .Where(x => x.TemplateId == this._selectedTemplate.Id)
                .ToList();

            this.tlcTargets.ItemsSource = null;
            this.tlcTargets.ItemsSource = this._targetList;
            var target = this._targetList.Find(x => x.Id == this._editHistory.TargetId);
            this.RefreshTargets(target);

            this.itemList = App.Context.Items
                .Where(x => x.TemplateId == this._selectedTemplate.Id)
                .ToList();

            this.resultDataList = App.Context.ResultDatas
                .Where(x => x.HistoryId == this._editHistory.Id)
                .ToList();

            int count = 1;
            foreach (var item in this.itemList)
            {
                count++;

                this.gridItems.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });

                var rec = this.CreateRectangle(item.Id);
                Grid.SetRow(rec, count);
                Grid.SetColumnSpan(rec, 3);
                this.gridItems.Children.Add(rec);
                this._rectDic[item.Id] = rec;

                var lbl = this.CreateLbl(item.Name);
                Grid.SetRow(lbl, count);
                Grid.SetColumn(lbl, 0);
                this.gridItems.Children.Add(lbl);

                var result = this.resultDataList.Find(x => x.ItemId == item.Id);

                if (item.TypeId == 0)
                {
                    var se = this.CreateSpinEdit(result?.Value, item.Id);
                    Grid.SetRow(se, count);
                    Grid.SetColumn(se, 1);
                    se.EditValueChanged += Se_EditValueChanged;

                    var lblMeasure = this.CreateLbl(item.MeasureUnit, 5, 11);
                    Grid.SetRow(lblMeasure, count);
                    Grid.SetColumn(lblMeasure, 2);

                    this.gridItems.Children.Add(lblMeasure);
                    this.gridItems.Children.Add(se);
                }
                else
                {
                    var checkLists = App.Context.CheckLists.Where(x => x.ItemId == item.Id).ToList();
                    var selectedItem = checkLists.Find(x => x.Id == result?.Value);
                    var ce = this.CreateCbEdit(checkLists, selectedItem, item.Id);
                    ce.EditValueChanged += Ce_EditValueChanged;
                    Grid.SetRow(ce, count);
                    Grid.SetColumn(ce, 1);
                    Grid.SetColumnSpan(ce, 2);
                    this.gridItems.Children.Add(ce);
                }
            }

            this.RefreshColors();
        }

        private void Ce_EditValueChanged(object sender, EditValueChangedEventArgs e)
        {
            var cbEdit = sender as ComboBoxEdit;
            if (!int.TryParse(cbEdit?.Name.Substring(4), out var itemId))
                return;

            var item = this.itemList.Find(x => x.Id == itemId);

            var checkedTargetList = this._targetList.FindAll(x => x.IsChecked);
            var target = checkedTargetList
                .FirstOrDefault(x => checkedTargetList
                .FindAll(t => t.ParentId == x.Id).Count == 0);

            if (!this._rectDic.TryGetValue(itemId, out var rect))
                return;

            if (!int.TryParse(e.NewValue?.ToString(), out var value) || item is null || target is null || !target.BorderId.HasValue)
            {
                rect.Fill = Brushes.Transparent;
                return;
            }

            var border = App.Context.Borders.Find(target.BorderId);
            if (border is null)
            {
                rect.Fill = Brushes.Transparent;
                return;
            }

            if (value == border.NormalItem)
                rect.Fill = Brushes.LightGreen;

            else if (value == border.WarningItem)
                rect.Fill = Brushes.LightGoldenrodYellow;

            else
                rect.Fill = Brushes.LightCoral;
        }

        private void Se_EditValueChanged(object sender, EditValueChangedEventArgs e)
        {
            var spinEdit = sender as SpinEdit;
            if (!int.TryParse(spinEdit?.Name.Substring(4), out var itemId))
                return;

            var item = this.itemList.Find(x => x.Id == itemId);

            var checkedTargetList = this._targetList.FindAll(x => x.IsChecked);
            var target = checkedTargetList
                .FirstOrDefault(x => checkedTargetList
                .FindAll(t => t.ParentId == x.Id).Count == 0);

            if (!this._rectDic.TryGetValue(itemId, out var rect))
                return;

            if (!decimal.TryParse(e.NewValue?.ToString(), out var value) || item is null || target is null || !target.BorderId.HasValue)
            {
                rect.Fill = Brushes.Transparent;
                return;
            }

            var border = App.Context.Borders.Find(target.BorderId);
            if (border is null)
            {
                rect.Fill = Brushes.Transparent;
                return;
            }

            if (value >= border.NormalMin && value <= border.NormalMax)
                rect.Fill = Brushes.LightGreen;

            else if (value >= border.WarningMin && value <= border.WarningMax)
                rect.Fill = Brushes.LightGoldenrodYellow;

            else
                rect.Fill = Brushes.LightCoral;
        }

        private Rectangle CreateRectangle(int itemId)
        {
            return new Rectangle
            {
                Name = "rec" + itemId
            };
        }

        private Label CreateLbl(string content, int marginLeft = 11, int marginRight = 0)
        {
            return new Label
            {
                Content = content,
                Margin = new Thickness(marginLeft, 3, marginRight, 3)
            };
        }

        private ComboBoxEdit CreateCbEdit(List<CheckList> itemSource, CheckList selectedItem, int id)
        {
            var cb = new ComboBoxEdit
            {
                Name = "item" + id,
                ItemsSource = itemSource,
                DisplayMember = "Name",
                ValueMember = "Id",
                Margin = new Thickness(5, 3, 11, 3)
            };

            cb.SelectedItem = selectedItem;
            return cb;
        }

        private SpinEdit CreateSpinEdit(decimal? value, int id)
        {
            return new SpinEdit
            {
                Name = "item" + id,
                EditValue = value,
                Margin = new Thickness(5, 3, 0, 3)
            };
        }

        private void sbCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void sbSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!this.cePatient.DoValidate()
                    | !this.deCreate.DoValidate()
                    | !this.ceTemplate.DoValidate())
                    return;

                var checkedTargetList = this._targetList.FindAll(x => x.IsChecked);
                var target = checkedTargetList
                    .FirstOrDefault(x => checkedTargetList
                    .FindAll(t => t.ParentId == x.Id).Count == 0);

                if (target is null)
                {
                    MessageBox.Show("Не указана целевая группа, в которю входит пациент", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                var resultDataList = new List<ResultData>();
                var isNotValid = false;
                foreach (var control in this.gridItems.Children)
                {
                    if (!(control is BaseEdit baseEdit))
                        continue;

                    if (!baseEdit.Name.StartsWith("item"))
                        continue;

                    if (baseEdit.EditValue is null)
                    {
                        isNotValid = true;
                        break;
                    }

                    var idStr = baseEdit.Name.Substring(4);
                    if (!int.TryParse(idStr, out var itemId))
                        continue;

                    var item = this.itemList.Find(x => x.Id == itemId);
                    if (item is null)
                        continue;

                    var value = Convert.ToDecimal(baseEdit.EditValue);
                    var resultData = new ResultData
                    {
                        ItemId = item.Id,
                        Value = value
                    };

                    resultDataList.Add(resultData);
                }

                if (isNotValid)
                {
                    MessageBox.Show("Заполните все параметры анализа", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                this._editHistory.PatientId = this._selectedPatient.Id;
                this._editHistory.TargetId = target.Id;
                this._editHistory.CreateDate = this.deCreate.DateTime.Date;

                if (this._isAdd)
                    App.Context.Histories.Add(this._editHistory);

                App.Context.SaveChanges();

                resultDataList.ForEach(x => x.HistoryId = this._editHistory.Id);

                var currentResultData = App.Context.ResultDatas
                    .Where(x => x.HistoryId == this._editHistory.Id)
                    .ToList();

                App.Context.ResultDatas.RemoveRange(currentResultData);
                App.Context.ResultDatas.AddRange(resultDataList);
                App.Context.SaveChanges();

                this.DialogResult = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("При сохранении произошла ошибка", "Исключение", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        bool template = true;
        private void ceTemplate_Validate(object sender, DevExpress.Xpf.Editors.ValidationEventArgs e)
        {
            if (template)
            {
                template = false;
                return;
            }

            if (this._selectedTemplate != null)
                return;

            e.IsValid = false;
            e.ErrorType = DevExpress.XtraEditors.DXErrorProvider.ErrorType.Critical;
            e.ErrorContent = "Не выбран шаблон анализа";
        }


        private Dictionary<int, Rectangle> _rectDic = new Dictionary<int, Rectangle>();
        private void RefreshColors()
        {
            foreach (var control in this.gridItems.Children)
            {
                if (!(control is BaseEdit baseEdit))
                    continue;

                if (!baseEdit.Name.StartsWith("item"))
                    continue;

                if (!int.TryParse(baseEdit.Name.Substring(4), out var itemId))
                    continue;

                var item = this.itemList.Find(x => x.Id == itemId);

                var checkedTargetList = this._targetList.FindAll(x => x.IsChecked);
                var target = checkedTargetList
                    .FirstOrDefault(x => checkedTargetList
                    .FindAll(t => t.ParentId == x.Id).Count == 0);

                if (!this._rectDic.TryGetValue(itemId, out var rect))
                    continue;

                rect.Fill = Brushes.Transparent;

                var border = App.Context.Borders.Find(target?.BorderId);
                if (border is null || item is null || target is null)
                    continue;

                if (baseEdit is SpinEdit spinEdit)
                {
                    if (!decimal.TryParse(spinEdit.EditValue?.ToString(), out var value))
                        continue;

                    if (value >= border.NormalMin && value <= border.NormalMax)
                        rect.Fill = Brushes.LightGreen;

                    else if (value >= border.WarningMin && value <= border.WarningMax)
                        rect.Fill = Brushes.LightGoldenrodYellow;

                    else
                        rect.Fill = Brushes.LightCoral;
                }
                else if (baseEdit is ComboBoxEdit cbEdit)
                {
                    if (!int.TryParse(cbEdit.EditValue?.ToString(), out var value))
                        continue;

                    if (value == border.NormalItem)
                        rect.Fill = Brushes.LightGreen;

                    else if (value == border.WarningItem)
                        rect.Fill = Brushes.LightGoldenrodYellow;

                    else
                        rect.Fill = Brushes.LightCoral;
                }
            }
        }
    }
}
