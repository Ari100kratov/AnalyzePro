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
        public AddItemWindow()
        {
            InitializeComponent();
        }

        public static Item Execute(Template template)
        {
            var window = new AddItemWindow();
            window._newItem = new Item { TemplateId = template.Id, Template = template };
            window.ShowDialog();
            return window._newItem;
        }

        private void sbCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void sbSave_Click(object sender, RoutedEventArgs e)
        {
            this._newItem.Name = this.teName.EditValue.ToString();

            using (var context = new DataContext())
            {
                context.Items.Add(this._newItem);
                context.SaveChanges();
            }

            this.DialogResult = true;
        }
    }
}
