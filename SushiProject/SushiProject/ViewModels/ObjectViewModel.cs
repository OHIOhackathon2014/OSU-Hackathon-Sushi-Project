using SushiProject.Commands;
using SushiProject.Model;
using SushiProject.Model.Logic;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media.Imaging;

namespace SushiProject.ViewModels
{
    public class ObjectViewModel : INotifyPropertyChanged
    {
        private SpriteViewModel sprite;
        public SpriteViewModel Sprite
        {
            get { return sprite; }
            set {
                sprite = value;
                OnPropertyChanged("Sprite");
            }
        }

        public MainWindowViewModel mainWindowVM;

        public GameObject gameObject { get; set; }

        private string windowTitle="";
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
                WindowTitle = String.Format("Object Editor - {0}", name);
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

            gameObject = new GameObject();

            DeleteCommand = new Command(DeleteBehavior, AlwaysTrue);
            SaveCommand = new Command(SaveAndClose, AlwaysTrue);
            AddBehaviorCommand = new Command(AddBehavior, AlwaysTrue);
        }

        public Command AddBehaviorCommand { get; private set; }
        public Command SaveCommand { get; private set; }
        public Command DeleteCommand { get; private set; }

        public void SaveAndClose(object target)
        {
            // Save the view model data to the model
            gameObject.Name = Name;
            ((System.Windows.Window)target).Close();
        }

        public void DeleteBehavior(object target)
        {
            BehaviorViewModel behavior = (BehaviorViewModel)target;
            if (behavior == null) return;
            DialogResult dialogResult = MessageBox.Show(String.Format("Are you sure you want to delete {0}?", behavior.Name), "Confirm Delete Behavior", System.Windows.Forms.MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                Behaviors.Remove(behavior);
                gameObject.Behaviors.Remove(behavior.Name);
            }
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

            string iconName = String.Empty;
            string bname = behaviorName.ToLower();
            if (bname.Contains("mouse")) iconName = "Mouse";
            else if (bname.Contains("initialize")) iconName = "Initialized";
            else if (bname.Contains("keyboard")) iconName = "Keyboard";
            else if (bname.Contains("collision")) iconName = "Collision";
            else if (bname.Contains("update")) iconName = "Update";
            else if (bname.Contains("destroy")) iconName = "Destroy";
            if (iconName != String.Empty) bvm.Icon = new BitmapImage(new Uri(String.Format("../Assets/Methods/{0}.png", behaviorName.ToLower()), UriKind.Relative));
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
