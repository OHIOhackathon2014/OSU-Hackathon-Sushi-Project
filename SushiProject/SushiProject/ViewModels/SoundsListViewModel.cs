using SushiProject.Commands;
using SushiProject.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SushiProject.ViewModels
{
    public class SoundsListViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<SoundViewModel> SoundCollection
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
                SoundCollection.Clear();
                foreach (Sound sound in project.Sounds)
                {
                    SoundViewModel svm = new SoundViewModel();
                    svm.Sound = sound;
                    SoundCollection.Add(svm);
                }
                OnPropertyChanged("Project");
            }
        }

        public SoundsListViewModel()
        {
            NewCommand = new Command(NewSound, AlwaysTrue);
            SoundCollection = new ObservableCollection<SoundViewModel>();
            //SoundViewModel svm = new SoundViewModel();
            //svm.Name = "bounce.mp3";
            //SoundCollection.Add(svm);
        }
        public Command NewCommand
        {
            get;
            private set;
        }

        public void NewSound(object target)
        {
            DialogResult dialogResult = MessageBox.Show("Do you want to make a new sound file?", "New Project", System.Windows.Forms.MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                SoundViewModel svm = new SoundViewModel();
                svm.Name = "default";
                Sound sound = new Sound();
                sound.Name = svm.Name;
                Project.Sounds.Add(sound);
                SoundCollection.Add(svm);
            }
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
