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
    /// Interaction logic for LevelsView.xaml
    /// </summary>
    public partial class LevelsView : UserControl
    {
        private LevelViewModel _local_ovm_for_rename;

        public LevelsView()
        {
            InitializeComponent();
        }

        protected void HandleDoubleClick(object sender, MouseButtonEventArgs e)
        {
            LevelViewModel levelVM = (LevelViewModel)((sender as FrameworkElement).DataContext);
            LevelsListViewModel lvm = DataContext as LevelsListViewModel;
            levelVM.LoadProjectSettings(lvm.Project.Settings);
            var window = new LevelEditorView();
            window.Owner = Window.GetWindow(this);
            window.DataContext = levelVM;

            window.Show();
        }

        private void OpenButton(object sender, RoutedEventArgs e)
        {
            object selected = LevelsListBox.SelectedItem;
            if (selected == null) return;
            var window = new LevelEditorView();
            window.Owner = Window.GetWindow(this);
            window.DataContext = selected;
            window.Show();
        }

        private void RenameButton(object sender, RoutedEventArgs e)
        {
            LevelViewModel selected = (LevelViewModel)LevelsListBox.SelectedItem;
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
            LevelViewModel selected = (LevelViewModel)LevelsListBox.SelectedItem;
            if (selected == null) return;

            System.Windows.Forms.DialogResult dialogResult = System.Windows.Forms.MessageBox.Show(
                    String.Format("Are you sure you want to delete level {0}?", selected.Name),
                    "Confirm Delete",
                    System.Windows.Forms.MessageBoxButtons.YesNo);
            if (dialogResult == System.Windows.Forms.DialogResult.Yes)
            {
                LevelsListViewModel lvm = (LevelsListViewModel)DataContext;
                lvm.LevelCollection.Remove(selected);
                lvm.Project.Levels.Remove(selected.level);
            }
        }
    }
}
