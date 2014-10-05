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
    public class SpritesListViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<SpriteViewModel> SpriteCollection
        {
            get;
            set;
        }


        private GameProject project;
        public GameProject Project
        {
            get 
            { 
                return project; 
            }
            set 
            {
                project = value;
                SpriteCollection.Clear();
                foreach (Sprite sprite in project.Sprites)
                {
                    SpriteViewModel svm = new SpriteViewModel();
                    svm.Sprite = sprite;
                    SpriteCollection.Add(svm);
                }

                OnPropertyChanged("Project");
            }
        }

        public SpritesListViewModel()
        {
            SpriteCollection = new ObservableCollection<SpriteViewModel>();
            //SpriteViewModel svm = new SpriteViewModel();
            //svm.Name = "image 1";
            //SpriteCollection.Add(svm);
            NewCommand = new Command(NewSprite, AlwaysTrue);
        }
        public bool AlwaysTrue(object target) { return true; }
        public Command NewCommand { get; private set; }

        public void NewSprite(object target)
        {
            SpriteViewModel svm = new SpriteViewModel();
            svm.Name = "sprite";
            Sprite sprite = new Sprite();
            sprite.Name = svm.Name;
            Project.Sprites.Add(sprite);
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
