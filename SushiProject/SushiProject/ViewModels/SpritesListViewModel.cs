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
    public class SpritesListViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<SpriteViewModel> SpriteCollection
        {
            get;
            set;
        }

        private GameProject gameProject;

        public GameProject GameProject
        {
            get { return gameProject; }
            set {
                gameProject = value;

                SpriteCollection.Clear();
                foreach (Image img in gameProject.Images)
                {
                    SpriteViewModel svm = new SpriteViewModel();
                    svm.Image = img;
                    SpriteCollection.Add(svm);
                }

                OnPropertyChanged("GameProject");
            }
        }

        public SpritesListViewModel()
        {
            SpriteCollection = new ObservableCollection<SpriteViewModel>();
            SpriteViewModel svm = new SpriteViewModel();
            svm.Name = "image 1";
            SpriteCollection.Add(svm);
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
