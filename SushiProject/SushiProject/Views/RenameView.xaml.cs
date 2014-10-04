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

namespace SushiProject.Views
{
    /// <summary>
    /// Interaction logic for RenameView.xaml
    /// </summary>
    public partial class RenameView : Window
    {
        public RenameView()
        {
            InitializeComponent();
        }

        public void SetText(string to)
        {
            RenameTextBox.Text = to;
        }

        public string GetText()
        {
            return RenameTextBox.Text;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
