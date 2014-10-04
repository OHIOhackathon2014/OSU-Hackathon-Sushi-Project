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
            SoundCollection = new ObservableCollection<SoundViewModel>();
            SoundViewModel svm = new SoundViewModel();
            svm.Name = "bounce.mp3";
            SoundCollection.Add(svm);
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
