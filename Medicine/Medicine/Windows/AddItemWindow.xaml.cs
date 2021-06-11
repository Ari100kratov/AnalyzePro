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
    /// Interaction logic for AddItemWindow.xaml
    /// </summary>
    public partial class AddItemWindow : ThemedWindow
    {
        private Item _newItem;
        private Template _newTemplate;
        public AddItemWindow()
        {
            InitializeComponent();
        }

        public static Item Execute(Template template)
        {
            var window = new AddItemWindow();
            window.Title = "Новый параметр";
            window._newItem = new Item { TemplateId = template.Id };
            window.ShowDialog();
            return window._newItem;
        }

        public static Template Execute()
        {
            var window = new AddItemWindow();
            window.Title = "Новый шаблон";
            window._newTemplate = new Template();
            window.ShowDialog();
            return window._newTemplate;
        }

        private void sbCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void sbSave_Click(object sender, RoutedEventArgs e)
        {
            if (this._newItem != null)
            {
                this._newItem.Name = this.teName.Text;
                App.Context.Items.Add(this._newItem);
            }
            else if (this._newTemplate != null)
            {
                this._newTemplate.Name = this.teName.Text;
                App.Context.Templates.Add(this._newTemplate);
            }

            App.Context.SaveChanges();
            this.DialogResult = true;
        }
    }
}
