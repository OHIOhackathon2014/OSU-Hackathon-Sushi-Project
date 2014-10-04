using SushiProject.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace SushiProject.ViewModels
{
    public class SettingsViewModel : INotifyPropertyChanged
    {
        private int projectTitle;
        public int ProjectTitle
        {
            get { return projectTitle; }
            set {
                projectTitle = value;
                OnPropertyChanged("ProjectTitle");
            }
        }

        private int screenHeight;
        public int ScreenHeight
        {
            get { return screenHeight; }
            set {
                screenHeight = value;
                OnPropertyChanged("ScreenHeight");
            }
        }

        private int screenWidth;
        public int ScreenWidth
        {
            get { return screenWidth; }
            set{
                screenWidth = value;
                OnPropertyChanged("ScreenWidth");
            }
        }
        public GameProject Project { get; set; }

        private int frameRate;
        public int FrameRate
        {
            get { return frameRate; }
            set { frameRate = value;
                OnPropertyChanged("FrameRate");
            }
        }

        private Color backgroundColor;
        public Color BackgroundColor
        {
            get { return backgroundColor; }
            set
            {
                backgroundColor = value;
                OnPropertyChanged("BackgroundColor");
            }
        }
        
        public SettingsViewModel()
        {

        }

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
