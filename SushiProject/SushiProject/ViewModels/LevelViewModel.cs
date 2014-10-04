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
    public class LevelViewModel : INotifyPropertyChanged
    {
        private Level _level;
        public Level level
        {
            get { return _level; }
            set {
                _level = value;
                OnPropertyChanged("level");
            }
        }

        private string windowTitle;

        public string WindowTitle
        {
            get { return windowTitle; }
            set {
                windowTitle = value;
                OnPropertyChanged("WindowTitle");
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
                WindowTitle = String.Format("Level Editor - {0}", name);
                OnPropertyChanged("Name");
            }
        }

        public ObservableCollection<LevelObjectViewModel> LevelObjects{ get; set; }

        private ObjectsListViewModel objectsVM;
	    public ObjectsListViewModel ObjectsVM
	    {
		    get { return objectsVM;}
		    set {
                objectsVM = value;
                OnPropertyChanged("ObjectsVM");
            }
	    }
	
        public LevelViewModel()
        {
            level = new Level();
            LevelObjects = new ObservableCollection<LevelObjectViewModel>();

            SaveCommand = new Command(Save, AlwaysTrue);
        }

        public Command SaveCommand
        {
            get;
            private set;
        }

        public void Save(object target)
        {
            // Save the view model data to the model
            level.Name = Name;
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
