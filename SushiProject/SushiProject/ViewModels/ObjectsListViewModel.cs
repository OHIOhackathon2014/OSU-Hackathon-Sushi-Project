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
    public class ObjectsListViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<ObjectViewModel> ObjectCollection
        {
            get;
            set;
        }

        private GameProject project;
        public GameProject Project {
            get
            {
                return project;
            }
            set
            {
                project = value;
                ObjectCollection.Clear();
                foreach (GameObject go in Project.Objects)
                {
                    ObjectViewModel ovm = new ObjectViewModel();
                    ovm.mainWindowVM = mainWindowVM;
                    ovm.Name = go.Name;
                    ovm.gameObject = go;
                    ObjectCollection.Add(ovm);
                }
            }
        }

        public MainWindowViewModel mainWindowVM;

        public ObjectsListViewModel()
        {
            ObjectCollection = new ObservableCollection<ObjectViewModel>();
            NewCommand = new Command(NewGameObject, AlwaysTrue);
        }

        public Command NewCommand
        {
            get;
            private set;
        }

        public void NewGameObject(object target)
        {
            ObjectViewModel ovm = new ObjectViewModel();
            ovm.Name = "default";
            ovm.mainWindowVM = mainWindowVM;
            GameObject go = new GameObject();
            go.Name = ovm.Name;
            Project.Objects.Add(go);
            ObjectCollection.Add(ovm);
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
