using SushiProject.Commands;
using SushiProject.Model;
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
        private Sound sound;
        public Sound Sound
        {
            get { return sound; }
            set
            {
                sound = value;
                Name = sound.Name;
                OnPropertyChanged("Sound");
            }
        }

        private System.Media.SoundPlayer soundClip = new System.Media.SoundPlayer();
        public System.Media.SoundPlayer SoundClip
        {
            get{return soundClip;}
            set { soundClip = value;
            OnPropertyChanged("SoundClip");
                
            }
        }
        
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
            sound = new Sound();
            System.Media.SoundPlayer soundPlayer = new System.Media.SoundPlayer();
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
