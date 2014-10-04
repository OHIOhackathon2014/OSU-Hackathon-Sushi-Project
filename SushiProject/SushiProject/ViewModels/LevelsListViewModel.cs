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
    public class LevelsListViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<LevelViewModel> levelCollection = new ObservableCollection<LevelViewModel>();
        public ObservableCollection<LevelViewModel> LevelCollection
        {
            get {
                return levelCollection;
            }
            set {
                levelCollection = value;
                OnPropertyChanged("LevelCollection");
            }
        }
        private ObjectsListViewModel objectsVM;
        public ObjectsListViewModel ObjectsVM
        {
            get { return objectsVM; }
            set {
                objectsVM = value;
                OnPropertyChanged("ObjectsVM");
                foreach (LevelViewModel lvm in LevelCollection)
                {
                    lvm.ObjectsVM = objectsVM;
                }
            }
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
                foreach (Level level in Project.Levels)
                {
                    LevelViewModel lvm = new LevelViewModel();
                    lvm.ObjectsVM = this.ObjectsVM;
                    lvm.Name = level.Name;
                    LevelCollection.Add(lvm);
                }
            }
        }

        public LevelsListViewModel()
        {
            LevelCollection = new ObservableCollection<LevelViewModel>();
            LevelViewModel lvm = new LevelViewModel();
            lvm.level = new Level();
            lvm.ObjectsVM = ObjectsVM;
            lvm.Name = "level 1";
            LevelCollection.Add(lvm);
            NewCommand = new Command(NewLevel, AlwaysTrue);
        }

        public Command NewCommand
        {
            get;
            private set;
        }

        public void NewLevel(object target)
        {
            LevelViewModel lvm = new LevelViewModel();
            lvm.Name = "Level";
            Level level = new Level();
            level.Name = lvm.Name;
            Project.Levels.Add(level);
            LevelCollection.Add(lvm);
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
