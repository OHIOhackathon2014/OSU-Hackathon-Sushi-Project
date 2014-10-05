using SushiProject.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace SushiProject.ViewModels
{
    public class SpriteViewModel : INotifyPropertyChanged
    {

        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
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
        }

        public void NewImage(object target)
        {
            Image image = new Image();
            sprite.Add(image);
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
