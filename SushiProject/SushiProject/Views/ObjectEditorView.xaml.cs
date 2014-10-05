using SushiProject.CustomControls;
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
using System.Windows.Shapes;

namespace SushiProject.Views
{
    /// <summary>
    /// Interaction logic for ObjectEditorView.xaml
    /// </summary>
    public partial class ObjectEditorView : Window
    {
        ListViewDragDropManager<MethodViewModel> dragMgr;
        private Point? startPoint;
        object dragData;

        public ObjectEditorView()
        {
            InitializeComponent();
            this.Loaded += Window1_Loaded;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            BehaviorSelectView window = new BehaviorSelectView();
            window.Owner = Window.GetWindow(this);
            window.DataContext = DataContext;
            window.Show();
        }

        #region Drag and Drop
        void Window1_Loaded(object sender, RoutedEventArgs e)
        {
            this.dragMgr = new ListViewDragDropManager<MethodViewModel>();
        }

        private void DragAction_PreviewMouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            startPoint = e.GetPosition(null);

            ItemsControl itemsControl = (ItemsControl)sender;

            for(int i = 0; i < itemsControl.Items.Count; i++)
            {
                UIElement uiElement = (UIElement)itemsControl.ItemContainerGenerator.ContainerFromIndex(i);
                if (uiElement as ContentPresenter != null && (uiElement as ContentPresenter).IsMouseOver == true)
                {
                    dragData = (uiElement as ContentPresenter).DataContext;
                }
            }
        }

        private void DragAction_PreviewMouseMove_1(object sender, MouseEventArgs e)
        {
            if (startPoint == null) return;
            Mouse.OverrideCursor = Cursors.Cross;
        }

        private static T FindAncestor<T>(DependencyObject current) where T : DependencyObject
        {
            do
            {
                if (current is T)
                {
                    return (T)current;
                }
                current = VisualTreeHelper.GetParent(current);
            }
            while (current != null);
            return null;
        }

        private void MethodsListBox_AcceptDrop(object sender, MouseButtonEventArgs e)
        {
            if (startPoint == null) return;
            MethodViewModel draggedMethod = dragData as MethodViewModel;
            if (draggedMethod == null) return;
            
            ObjectViewModel ovm = DataContext as ObjectViewModel;

            ListBox listBox = (ListBox)sender;
            BehaviorViewModel behavior = listBox.DataContext as BehaviorViewModel;
            int insertIndex = -1;

            for (int i = 0; i < listBox.Items.Count; i++)
            {
                UIElement uiElement = (UIElement)listBox.ItemContainerGenerator.ContainerFromIndex(i);
                if ((uiElement as ListBoxItem) != null && (uiElement as ListBoxItem).IsMouseOver == true)
                {
                    insertIndex = i;
                }
            }

            if (insertIndex == -1)
            {
                behavior.Methods.Add(draggedMethod.Instantiate());
            }
            else
            {
                behavior.Methods.Insert(insertIndex, draggedMethod.Instantiate());
            }

            startPoint = null;
        }

        public static IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                    if (child != null && child is T)
                    {
                        yield return (T)child;
                    }

                    foreach (T childOfChild in FindVisualChildren<T>(child))
                    {
                        yield return childOfChild;
                    }
                }
            }
        }

        private void Window_MouseLeftButtonUp_1(object sender, MouseButtonEventArgs e)
        {
            dragData = null;
            startPoint = null;
            Mouse.OverrideCursor = null;
        }
        #endregion // Drag and Drop

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            SpriteSelectorView ssv = new SpriteSelectorView();
            ObjectViewModel ovm = (ObjectViewModel)DataContext;
            ssv.DataContext = new SpriteSelectorViewModel();
            ssv.SpriteCollection = ovm.mainWindowVM.Sprites.SpriteCollection;
            ssv.Owner = this;
            ssv.ShowDialog();
        }
    }
}
