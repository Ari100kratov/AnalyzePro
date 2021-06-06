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
            using (var context = new DataContext())
            {
                this._templateList = context.Templates.Include("Group").Include("Items").ToList();
            };

            this.gcTemplates.ItemsSource = this._templateList;
            this.gcTemplates.GroupBy("Group.Name");
            this.gcItems.ItemsSource = this._itemList;
        }

        private void gcTemplates_SelectedItemChanged(object sender, DevExpress.Xpf.Grid.SelectedItemChangedEventArgs e)
        {
            this.sbEditTemplate.IsEnabled =
                this.sbDeleteTemplate.IsEnabled = this._selectedTemplate != null;

            this._itemList = this._selectedTemplate is null ? new List<Item>() : this._selectedTemplate.Items.ToList();
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
                    this._selectedTemplate.GroupId = null;
                    this._selectedTemplate.Group = null;
                    context.Entry(this._selectedTemplate).State = System.Data.Entity.EntityState.Deleted;
                    context.SaveChanges();
                }

                this._templateList.Remove(this._selectedTemplate);
                this.gcTemplates.RefreshData();
            }
        }

        private void sbEditTemplate_Click(object sender, RoutedEventArgs e)
        {
            EditTemplateWindow.Execute(this._selectedTemplate);
            using (var context = new DataContext())
            {
                this._templateList = context.Templates.Include("Group").ToList();
            };

            this.gcTemplates.ItemsSource = this._templateList;
        }

        private void gcItems_SelectedItemChanged(object sender, DevExpress.Xpf.Grid.SelectedItemChangedEventArgs e)
        {
            this.sbEditItem.IsEnabled =
                this.sbDeleteItem.IsEnabled = this._selectedItem != null;
        }

        private void sbAddItem_Click(object sender, RoutedEventArgs e)
        {

        }

        private void sbEditItem_Click(object sender, RoutedEventArgs e)
        {

        }

        private void sbDeleteItem_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
