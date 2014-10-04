using SushiProject.ViewModels;
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
    /// Interaction logic for ObjectsView.xaml
    /// </summary>
    public partial class ObjectsView : UserControl
    {
        public ObjectsView()
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

        private void OpenButton(object sender, RoutedEventArgs e)
        {
            object selected = ObjectsListBox.SelectedItem;
            if (selected == null) return;

            var window = new ObjectEditorView();
            window.Owner = Window.GetWindow(this);
            window.DataContext = selected;
            window.Show();
        }

        private void RenameButton(object sender, RoutedEventArgs e)
        {

        }

        private void DeleteButton(object sender, RoutedEventArgs e)
        {
            ObjectViewModel selected = (ObjectViewModel)ObjectsListBox.SelectedItem;
            if (selected == null) return;

            System.Windows.Forms.DialogResult dialogResult = System.Windows.Forms.MessageBox.Show(
                    String.Format("Are you sure you want to delete object {0}?", selected.Name),
                    "Confirm Delete",
                    System.Windows.Forms.MessageBoxButtons.YesNo);
            if (dialogResult == System.Windows.Forms.DialogResult.Yes)
            {
                ObjectsListViewModel ovm = (ObjectsListViewModel)DataContext;
                ovm.ObjectCollection.Remove(selected);
                ovm.Project.Objects.Remove(selected.gameObject);
            }
        }
    }
}
