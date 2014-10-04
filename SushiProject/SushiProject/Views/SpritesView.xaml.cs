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

namespace SushiProject.Views
{
    /// <summary>
    /// Interaction logic for SpritesView.xaml
    /// </summary>
    public partial class SpritesView : UserControl
    {
        public SpritesView()
        {
            InitializeComponent();
        }

        protected void HandleDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var window = new ObjectEditorView();
            window.Owner = Window.GetWindow(this);
            window.DataContext = (sender as FrameworkElement).DataContext;
            window.Show();
        }
    }
}
