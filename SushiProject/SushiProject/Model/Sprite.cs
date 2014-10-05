using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SushiProject.Model
{
    public class Sprite : INotifyPropertyChanged
    {
        private Image firstImage;
        public Image FirstImage
	    {
            get { return firstImage; }
            set {
                firstImage = value;
                OnPropertyChanged("FirstImage");
            }
	    }

        public ObservableCollection<Image> Images { get; set; }
        public float FrameRate { get; set; }
        public string Name { get; set; }

        public Sprite()
        {
            Images = new ObservableCollection<Image>();
        }

        public void Add(Image image)
        {
            Images.Add(image);
            if (Images.Count == 1)
            {
                FirstImage = image;
                OnPropertyChanged("FirstImage");
            }
            OnPropertyChanged("Images");
        }

        public void Remove(int i)
        {
            Images.RemoveAt(i);
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
