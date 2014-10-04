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
    /// Interaction logic for SettingsView.xaml
    /// </summary>
    public partial class SettingsView : Window
    {
        public SettingsView()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SettingsViewModel SVM = (SettingsViewModel)DataContext;
            SVM.FrameRate = Convert.ToInt32(FPS.Text);
            SVM.ScreenHeight = Convert.ToInt32(ScreenHeight.Text);
            SVM.ScreenWidth = Convert.ToInt32(ScreenWidth.Text);
            SVM.BackgroundColor = BCP.SelectedColor;

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
