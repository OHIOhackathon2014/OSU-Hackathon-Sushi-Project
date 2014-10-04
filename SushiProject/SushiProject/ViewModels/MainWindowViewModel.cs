using SushiProject.Commands;
using SushiProject.Model;
using SushiProject.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
                Objects.Project = Project;
                Levels.Project = Project;
                Sprites.GameProject = Project;
                Sounds.Project = Project;
                Settings.Project = Project;

                OnPropertyChanged("Project");
            }
        }

        public ObjectsListViewModel Objects { get; private set; }
        public LevelsListViewModel  Levels { get; private set; }
        public SoundsListViewModel  Sounds { get; private set; }
        public SpritesListViewModel Sprites { get; private set; }
        public SettingsViewModel    Settings { get; private set; }

        public MainWindowViewModel()
        {
            Objects = new ObjectsListViewModel();
            Objects.Project = Project;
            Levels = new LevelsListViewModel();
            Levels.Project = Project;
            Levels.ObjectsVM = Objects;
            Sprites = new SpritesListViewModel();
            Sounds = new SoundsListViewModel();
            Settings = new SettingsViewModel();
            NewCommand = new Command(NewProject, AlwaysTrue);
            CompileCommand = new Command(CompileProject, AlwaysTrue);
            SaveCommand = new Command(SaveProject, AlwaysTrue);
            OpenCommand = new Command(OpenProject, AlwaysTrue);
        }

        public Command NewCommand
        {
            get;
            private set;
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

        public void NewProject(object target)
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure you want to start a new project? This will discard any unsaved changes.", "New Project", System.Windows.Forms.MessageBoxButtons.YesNo);
            if (dialogResult != DialogResult.Yes) return;
            
            Project = new GameProject();

            Objects.Project = Project;
            Levels.ObjectsVM = Objects;
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
