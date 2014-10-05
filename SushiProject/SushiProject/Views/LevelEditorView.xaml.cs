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
    /// Interaction logic for LevelEditorView.xaml
    /// </summary>
    public partial class LevelEditorView : Window
    {
        private LevelObjectViewModel draggedLevelObject;

        ListViewDragDropManager<ObjectViewModel> dragMgr;
        private Point? startPoint;
        object dragData;

        private WriteableBitmap defaultErrorBmp;
        private WriteableBitmap bmpStage;
        private LevelViewModel dataContext;

        public LevelEditorView()
        {
            InitializeComponent();
            defaultErrorBmp = LoadBitmap("/Assets/missing.png");


            Loaded += MyWindow_Loaded;
        }

        private void MyWindow_Loaded(object sender, RoutedEventArgs e)
        {
            this.dragMgr = new ListViewDragDropManager<ObjectViewModel>();

            if (DataContext == null) return;
            dataContext = (LevelViewModel)DataContext;
            bmpStage = BitmapFactory.New(dataContext.ScreenWidth, dataContext.ScreenHeight);
            bmpStage.Clear(dataContext.BackgroundColor);
            levelImage.Source = bmpStage;

            CompositionTarget.Rendering += new EventHandler(CompositionTarget_Rendering);
            levelImageScrollViewer.ScrollToVerticalOffset(levelImageScrollViewer.ScrollableHeight / 2);
            levelImageScrollViewer.ScrollToHorizontalOffset(levelImageScrollViewer.ScrollableWidth / 2);
        }

        #region Draw the canvas
        WriteableBitmap LoadBitmap(string path)
        {
            var img = new BitmapImage();
            img.BeginInit();
            img.CreateOptions = BitmapCreateOptions.None;
            var s = Application.GetResourceStream(new Uri(path, UriKind.Relative)).Stream;
            img.StreamSource = s;
            img.EndInit();
            return BitmapFactory.ConvertToPbgra32Format(img);
        }

        void CompositionTarget_Rendering(object sender, EventArgs e)
        {
            // Wrap updates in a GetContext call, to prevent invalidation and nested locking/unlocking during this block
            using (bmpStage.GetBitmapContext())
            {
                bmpStage.Clear(dataContext.BackgroundColor);

                // Draw the game objects
                foreach (LevelObjectViewModel obj in dataContext.LevelObjects)
                {
                    WriteableBitmap source;
                    if (obj.GameObjectRefernce != null &&
                        obj.GameObjectRefernce.Sprite != null &&
                        obj.GameObjectRefernce.Sprite.Sprite.FirstImage != null &&
                        obj.GameObjectRefernce.Sprite.Sprite.FirstImage.ImageSrc != null) {
                            source = BitmapFactory.ConvertToPbgra32Format(
                                new WriteableBitmap((BitmapSource)obj.GameObjectRefernce.Sprite.Sprite.FirstImage.ImageSrc));
                    } else {
                        source = defaultErrorBmp;
                    }
                    bmpStage.Blit(new Rect(obj.X, obj.Y, obj.Width, obj.Height),            // Destination rectangle
                        source,                                                        // Image source
                        new Rect(0, 0, source.PixelWidth, source.PixelHeight)               // Source rectangle
                        );
                }
                
            }
        }
        #endregion // Draw the canvas

        #region Level editor user input

        private LevelObjectViewModel GetLevelObjectAtPostiion(Point point)
        {
            Rect hitbox;
            foreach (LevelObjectViewModel obj in dataContext.LevelObjects)
            {
                hitbox = new Rect(obj.X,obj.Y,obj.Width,obj.Height);
                if (hitbox.Contains(point))
                {
                    return obj;
                }
            }
            return null;
        }

        private void levelImage_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Point point = e.GetPosition((Image)sender);
            LevelObjectViewModel lo = GetLevelObjectAtPostiion(point);
            if (lo == null) return;
            draggedLevelObject = lo;
            lo.X = point.X;
            lo.Y = point.Y;
            dataContext.LastSelectedInstance = lo;
        }

        private void levelImage_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (draggedLevelObject == null) return;
            Point point = e.GetPosition((Image)sender);
            draggedLevelObject.X = point.X;
            draggedLevelObject.Y = point.Y;
        }

        #endregion

        #region Drag n Drop
        private void ListBox_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
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

        private void ListBox_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (startPoint == null) return;
            Mouse.OverrideCursor = Cursors.Hand;
        }

        private static T FindAncestor<T>(DependencyObject current) where T : DependencyObject
        {
            do {
                if (current is T) {
                    return (T)current;
                }
                current = VisualTreeHelper.GetParent(current);
            }
            while (current != null);
            return null;
        }

        private void levelImage_AcceptDrop(object sender, MouseButtonEventArgs e)
        {
            if (startPoint == null) return;
            ObjectViewModel draggedObjectVM = dragData as ObjectViewModel;
            if (draggedObjectVM == null) return;

            ObjectViewModel ovm = DataContext as ObjectViewModel;

            Image img = (Image)sender;
            Point pos = Mouse.GetPosition(img);

            // Create a new level object
            dataContext.AddLevelObject(pos.X, pos.Y, draggedObjectVM);

            startPoint = null;
        }

        #endregion

        private void Window_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            draggedLevelObject = null;
            dragData = null;
            startPoint = null;
            Mouse.OverrideCursor = null;
        }

    }
}
