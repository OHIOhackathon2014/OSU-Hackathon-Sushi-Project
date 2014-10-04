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
        private WriteableBitmap particleBmp;
        private WriteableBitmap bmp;

        public LevelEditorView()
        {
            InitializeComponent();
            particleBmp = LoadBitmap("/Assets/ball.png");
            bmp = BitmapFactory.New(640, 480);
            bmp.Clear(Colors.BlueViolet);
            levelImage.Source = bmp;

            CompositionTarget.Rendering += new EventHandler(CompositionTarget_Rendering);

            Loaded += MyWindow_Loaded;
        }

        private void MyWindow_Loaded(object sender, RoutedEventArgs e)
        {
            levelImageScrollViewer.ScrollToVerticalOffset(levelImageScrollViewer.ScrollableHeight / 2);
            levelImageScrollViewer.ScrollToHorizontalOffset(levelImageScrollViewer.ScrollableWidth / 2);
        }

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
            // NOTE: This is not strictly necessary for the SL version as this is a WPF feature, however we include it here for completeness and to show
            // a similar API to WPF
            using (bmp.GetBitmapContext())
            {
                LevelViewModel context = (LevelViewModel)DataContext;
                bmp.Clear(Colors.LightSalmon);
                bmp.Blit(new Rect(10, 10, 64, 64), particleBmp, new Rect(0, 0, 48, 48));
            }
        }
        
    }
}
