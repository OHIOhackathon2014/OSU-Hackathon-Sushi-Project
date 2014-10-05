using SushiProject.Commands;
using SushiProject.Model;
using SushiProject.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace SushiProject.ViewModels
{
    public class SpriteViewModel : INotifyPropertyChanged
    {
        private string windowTitle;
        public string WindowTitle
        {
            get { return windowTitle; }
            set { windowTitle = value; OnPropertyChanged("WindowTitle"); }
        }
        
        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                sprite.Name = name;
                WindowTitle = String.Format("Sprite Editor - {0}", name);
                OnPropertyChanged("Name");
            }
        }

        private Sprite sprite;
        public Sprite Sprite
        {
            get { return sprite; }
            set
            {
                sprite = value;
                Name = sprite.Name;
                OnPropertyChanged("Sprite");
            }
        }

        public SpriteViewModel()
        {
            sprite = new Sprite();
            OpenCommand = new Command(OpenImage, AlwaysTrue);
        }

        #region Commands
        public Command OpenCommand
        {
            get;
            private set;
        }

        public void OpenImage(object target)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "PNG Images|*.png";

            DialogResult result = ofd.ShowDialog();
            if (result == DialogResult.OK)
            {
                string copiedFilePath = ProjectDirectoryManager.Instance.AddImage(ofd.FileName);
                Image img = new Image(copiedFilePath, System.IO.Path.GetFileName(copiedFilePath));
                BitmapImage bmp = new BitmapImage();
                bmp.BeginInit();
                bmp.UriSource = new Uri(copiedFilePath, UriKind.Relative);
                bmp.CacheOption = BitmapCacheOption.OnLoad;
                bmp.EndInit();
                img.ImageSrc = bmp;
                Sprite.Add(img);
                OnPropertyChanged("Sprite");
            }
        }
        #endregion

        public void NewImage(object target)
        {
            Image image = new Image();
            sprite.Add(image);
        }
        public bool AlwaysTrue(object target) { return true; }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
