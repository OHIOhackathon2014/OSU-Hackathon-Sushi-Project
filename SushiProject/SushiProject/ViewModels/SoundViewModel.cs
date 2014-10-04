using SushiProject.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SushiProject.ViewModels
{
    public class SoundViewModel : INotifyPropertyChanged
    {
<<<<<<< HEAD
        private System.Media.SoundPlayer soundClip = new System.Media.SoundPlayer();
        public System.Media.SoundPlayer SoundClip
        {
            get{return soundClip;}
            set { soundClip = value;
            OnPropertyChanged("SoundClip");
                
            }
        }
        
=======


        private string windowTitle;
        public string WindowTitle
        {
            get { return windowTitle; }
            set
            {
                windowTitle = value;
                OnPropertyChanged("WindowTitle");
            }
        }

>>>>>>> d5d0626d6e4a1326d3fc2472344260bf49029581
        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                WindowTitle = String.Format("Sound Editor - {0}", Name);
                OnPropertyChanged("Name");
            }
        }

        public SoundViewModel()
        {
            NewSoundCommand = new Command(NewSound, AlwaysTrue);
        }

        public Command NewSoundCommand
        {
            get;
            private set;
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
