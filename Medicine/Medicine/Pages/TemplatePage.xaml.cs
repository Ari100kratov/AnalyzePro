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
    /// Interaction logic for TemplatePage.xaml
    /// </summary>
    public partial class TemplatePage : NavigationPage
    {
        private List<Template> _templateList;
        private Template _selectedTemplate => this.gcTemplates.SelectedItem as Template;

        private List<Item> _itemList = new List<Item>();
        private Item _selectedItem => this.gcItems.SelectedItem as Item;
        public TemplatePage()
        {
            InitializeComponent();
        }

        private void sbAddTemplate_Click(object sender, RoutedEventArgs e)
        {
            var template = new Template();
            if (EditTemplateWindow.Execute(template) == true)
            {
                this._templateList.Add(template);
                this.gcTemplates.RefreshData();
            }
        }

        private void NavigationPage_Loaded(object sender, RoutedEventArgs e)
        {
            this.RefreshGcTemplates();
            this.gcTemplates.GroupBy("Group.Name");
            this.gcItems.ItemsSource = this._itemList;
        }

        private void gcTemplates_SelectedItemChanged(object sender, DevExpress.Xpf.Grid.SelectedItemChangedEventArgs e)
        {
            this.sbEditTemplate.IsEnabled =
                this.sbAddItem.IsEnabled =
                this.sbDeleteTemplate.IsEnabled = this._selectedTemplate != null;

            var templateId = this._selectedTemplate?.Id;
            if (!templateId.HasValue)
            {
                this._itemList = new List<Item>();
            }
            else
            {
                using (var context = new DataContext())
                {
                    this._itemList = context.Items
                        .Include("CheckLists").Include("Template")
                        .Where(x => x.TemplateId == templateId.Value)
                        .ToList();
                }
            }

            this.gcItems.ItemsSource = this._itemList;
            this.gcItems.RefreshData();
        }

        private void sbDeleteTemplate_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Удалить выбранный шаблон?"
                , "Подтверждение"
                , MessageBoxButton.YesNo
                , MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                using (var context = new DataContext())
                {
                    context.Templates.Attach(this._selectedTemplate);
                    context.Templates.Remove(this._selectedTemplate);
                    context.SaveChanges();
                }

                this._templateList.Remove(this._selectedTemplate);
                this.gcTemplates.RefreshData();
                this.gcTemplates.SelectedItem = this._templateList.FirstOrDefault();
            }
        }

        private void sbEditTemplate_Click(object sender, RoutedEventArgs e)
        {
            EditTemplateWindow.Execute(this._selectedTemplate);
            this.RefreshGcTemplates();
        }

        private void RefreshGcTemplates()
        {
            var selectedTemplateId = this._selectedTemplate?.Id;
            using (var context = new DataContext())
            {
                this._templateList = context.Templates.Include("Group").ToList();
            };

            this.gcTemplates.ItemsSource = this._templateList;

            if (selectedTemplateId.HasValue)
                this.gcTemplates.SelectedItem = this._templateList.Find(x => x.Id == selectedTemplateId);
        }

        private void gcItems_SelectedItemChanged(object sender, DevExpress.Xpf.Grid.SelectedItemChangedEventArgs e)
        {
            this.sbEditItem.IsEnabled =
                this.sbDeleteItem.IsEnabled = this._selectedItem != null;
        }

        private void sbAddItem_Click(object sender, RoutedEventArgs e)
        {
            var item = AddItemWindow.Execute(this._selectedTemplate);

            if (item.Id > 0)
            {
                this._itemList.Add(item);
                this.gcItems.RefreshData();
                if (EditItemWindow.Execute(item) == true)
                    this.gcItems.RefreshData();
            }
        }

        private void sbEditItem_Click(object sender, RoutedEventArgs e)
        {
            if (EditItemWindow.Execute(this._selectedItem) == true)
                this.gcItems.RefreshData();
        }

        private void sbDeleteItem_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Удалить выбранный параметр?"
               , "Подтверждение"
               , MessageBoxButton.YesNo
               , MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                using (var context = new DataContext())
                {
                    context.Items.Attach(this._selectedItem);
                    context.Items.Remove(this._selectedItem);
                    context.SaveChanges();
                }

                this._itemList.Remove(this._selectedItem);
                this.gcItems.RefreshData();
            }
        }
    }
}
