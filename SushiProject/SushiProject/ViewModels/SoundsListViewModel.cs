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

        public GameProject Project { get; set; }

        public SoundsListViewModel()
        {
            NewSoundCommand = new Command(NewSound, AlwaysTrue);
            SoundCollection = new ObservableCollection<SoundViewModel>();
            SoundViewModel svm = new SoundViewModel();
            svm.Name = "bounce.mp3";
            SoundCollection.Add(svm);

            
        }

        public void NewSound(object target)
        {
            DialogResult dialogResult = MessageBox.Show("Do you want to make a new sound file?", "New Project", System.Windows.Forms.MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                //NewSoundFile = new Sound();
            }
        }

        public Command NewSoundCommand
        {
            get;
            private set;
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
