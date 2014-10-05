using SushiProject.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for SpriteSelectorView.xaml
    /// </summary>
    public partial class SpriteSelectorView : Window
    {
        private ObservableCollection<SpriteViewModel> spriteCollection;
        public ObservableCollection<SpriteViewModel> SpriteCollection
        {
            get
            {
                return spriteCollection;
            }
            set {
                spriteCollection = value;
                if (DataContext != null)
                {
                    ((SpriteSelectorViewModel)DataContext).Sprites = value;
                }
            }
        }
        
        public SpriteViewModel result;

        public SpriteSelectorView()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SpriteSelectorViewModel ssv = (SpriteSelectorViewModel)DataContext;
            result = ssv.SelectedSprite;
            Close();
        }
    }
}
