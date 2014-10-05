using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SushiProject.ViewModels
{
    public class SpriteSelectorViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<SpriteViewModel> sprites;
        public ObservableCollection<SpriteViewModel> Sprites
        {
            get { return sprites; }
            set { sprites = value; OnPropertyChanged("Sprites"); }
        }
        private SpriteViewModel selectedSprite;
        public SpriteViewModel SelectedSprite
        {
            get { return selectedSprite; }
            set { selectedSprite = value; OnPropertyChanged("SelectedSprite"); }
        }

        public SpriteSelectorViewModel()
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
