using SushiProject.Commands;
using SushiProject.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SushiProject.ViewModels
{
    public class LevelObjectViewModel : INotifyPropertyChanged
    {
        public Level level { get; set; }

        private ObjectViewModel gameObjectRefence;
        public ObjectViewModel GameObjectRefernce
        {
            get { return gameObjectRefence; }
            set { gameObjectRefence = value;
            OnPropertyChanged("GameObjectRefernce");
            }
        }

        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                level.Name = name;
                OnPropertyChanged("Name");
            }
        }

        private int width = 32;
        public int Width
        {
            get { return width; }
            set
            {
                width = value;
                OnPropertyChanged("Width");
            }
        }

        private int height = 32;
        public int Height
        {
            get { return height; }
            set
            {
                height = value;
                OnPropertyChanged("Height");
            }
        }

        private double rotation = 0;
        public double Rotation
        {
            get { return rotation; }
            set { rotation = value; OnPropertyChanged("Rotation"); }
        }

        private double scaleX = 1;
        public double ScaleX
        {
            get { return scaleX; }
            set { scaleX = value; OnPropertyChanged("ScaleX"); }
        }

        private double scaleY = 1;
        public double ScaleY
        {
            get { return scaleY; }
            set { scaleY = value; OnPropertyChanged("ScaleY"); }
        }

        private double x;
        public double X
        {
            get { return x; }
            set
            {
                x = value;
                OnPropertyChanged("X");
            }
        }

        private double y;
        public double Y
        {
            get { return y; }
            set
            {
                y = value;
                OnPropertyChanged("Y");
            }
        }

        public LevelObjectViewModel()
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
