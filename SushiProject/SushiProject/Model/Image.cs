using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace SushiProject.Model
{
    public class Image : INotifyPropertyChanged
    {
        public string Name { get; set; }
        public string FilePath { get; set; }

        public Image(string filePath, string name)
        {
            Name = name;
            FilePath = filePath;
        }

        public Image()
        {
            Name = "";
            FilePath = "";
        }

        private ImageSource imageSrc;
        public ImageSource ImageSrc
        {
            get { return imageSrc; }
            set {
                imageSrc = value;
                OnPropertyChanged("ImageSrc");
            }
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
