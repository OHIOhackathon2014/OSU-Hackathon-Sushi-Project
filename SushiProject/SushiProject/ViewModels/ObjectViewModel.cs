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
    public class ObjectViewModel : INotifyPropertyChanged
    {
        public GameObject prefab { get; set; }

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
        
        public ObservableCollection<BehaviorViewModel> Behaviors { get; set; }
        public AvailableMethodsViewModel AvailableMethods { get; set; }
        
        public ObjectViewModel()
        {
            Behaviors = new ObservableCollection<BehaviorViewModel>();
            Behaviors.Add(new BehaviorViewModel());
            AvailableMethods = new AvailableMethodsViewModel();

            prefab = new GameObject();

            SaveCommand = new Command(Save, AlwaysTrue);
            AddBehaviorCommand = new Command(AddBehavior, AlwaysTrue);
        }

        public Command AddBehaviorCommand
        {
            get;
            private set;
        }

        public Command SaveCommand
        {
            get;
            private set;
        }

        public void Save(object target)
        {
            // Save the view model data to the model
            prefab.Name = Name;
        }

        public void AddBehavior(object target)
        {
            string behaviorName = Convert.ToString(target);
            // Does behavior already exist?
            foreach (BehaviorViewModel behavior in Behaviors)
            {
                if (behaviorName.Equals(behavior.Name))
                {
                    return;
                }
            }
            BehaviorViewModel bvm = new BehaviorViewModel();
            bvm.Name = behaviorName;
            Behaviors.Add(bvm);
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
