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
        public Animation animation { get; set; }

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

<<<<<<< HEAD
        private Image image;
        public Image Image
        {
            get { return image; }
            set
            {
                image = value;
                Name = image.Name;
                OnPropertyChanged("Image");
            }
        }
=======
        public Collection<ImageSource> Images { get; set; }
>>>>>>> 0c65f4137d26bae5e2388ff7bdeb9d96ee9e2ad3

        public SpriteViewModel()
        {
            Images = new Collection<ImageSource>();
            Animation animation = new Animation();
        }

        public void NewImage(object target)
        {
            Image image = new Image();
            animation.Add(image);
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
