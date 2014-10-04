using SushiProject.Commands;
using SushiProject.Model;
using SushiProject.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SushiProject.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        public ProjectAS3Compiler projectAS3Compiler = new ProjectAS3Compiler();
        public ProjectXMLWriter projectXMLWriter = new ProjectXMLWriter();

        private GameProject project = new GameProject();
        public GameProject Project
        {
            get { return project; }
            set { project = value;
                OnPropertyChanged("Project");
            }
        }

        public ObjectsListViewModel Objects { get; private set; }
        public LevelsListViewModel Levels { get; private set; }
        public SoundsListViewModel Sounds { get; private set; }
        public SpritesListViewModel Sprites { get; private set; }
        public SettingsViewModel Settings { get; private set; }

        public MainWindowViewModel()
        {
            Objects = new ObjectsListViewModel();
            Levels = new LevelsListViewModel();
            Sprites = new SpritesListViewModel();
            Sounds = new SoundsListViewModel();
            Settings = new SettingsViewModel();
            CompileCommand = new Command(CompileProject, AlwaysTrue);
            SaveCommand = new Command(SaveProject, AlwaysTrue);
            OpenCommand = new Command(OpenProject, AlwaysTrue);
        }

        public Command CompileCommand
        {
            get;
            private set;
        }

        public Command SaveCommand
        {
            get;
            private set;
        }

        public Command OpenCommand
        {
            get;
            private set;
        }

        public void CompileProject(object target)
        {
            projectAS3Compiler.CompileProject(Project);
        }
        public void OpenProject(object target)
        {
            projectXMLWriter.OpenProject(Project);
        }
        public void SaveProject(object target)
        {
            projectXMLWriter.SaveProject(Project);
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
