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
    /// Interaction logic for SpritesView.xaml
    /// </summary>
    public partial class SpritesView : UserControl
    {
        private SpriteViewModel _local_ovm_for_rename;

        public SpritesView()
        {
            InitializeComponent();
        }

        protected void HandleDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var window = new SpriteEditorView();
            window.Owner = Window.GetWindow(this);
            window.DataContext = (sender as FrameworkElement).DataContext;
            window.Show();
        }

        private void OpenButton(object sender, RoutedEventArgs e)
        {
            object selected = SpritesListBox.SelectedItem;
            if (selected == null) return;
            var window = new SpriteEditorView();
            window.Owner = Window.GetWindow(this);
            window.DataContext = selected;
            window.Show();
        }

        private void RenameButton(object sender, RoutedEventArgs e)
        {
            SpriteViewModel selected = (SpriteViewModel)SpritesListBox.SelectedItem;
            if (selected == null) return;

            _local_ovm_for_rename = selected;

            RenameView window = new RenameView();
            window.SetText(selected.Name);
            window.Owner = Window.GetWindow(this);
            window.DataContext = selected;
            window.Show();
            window.Closed += window_Closed;
        }

        void window_Closed(object sender, EventArgs e)
        {
            _local_ovm_for_rename.Name = ((RenameView)sender).GetText();
        }

        private void DeleteButton(object sender, RoutedEventArgs e)
        {
            SpriteViewModel selected = (SpriteViewModel)SpritesListBox.SelectedItem;
            if (selected == null) return;

            System.Windows.Forms.DialogResult dialogResult = System.Windows.Forms.MessageBox.Show(
                    String.Format("Are you sure you want to delete sprite {0}?", selected.Name),
                    "Confirm Delete",
                    System.Windows.Forms.MessageBoxButtons.YesNo);
            if (dialogResult == System.Windows.Forms.DialogResult.Yes)
            {
                SpritesListViewModel svm = (SpritesListViewModel)DataContext;
                svm.SpriteCollection.Remove(selected);
                svm.Project.Sprites.Remove(selected.Sprite);
            }
        }
    }
}
