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
    public class LevelsListViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<LevelViewModel> LevelCollection
        {
            get;
            set;
        }

        public GameProject Project { get; set; }

        public LevelsListViewModel()
        {
            LevelCollection = new ObservableCollection<LevelViewModel>();
            LevelViewModel lvm = new LevelViewModel();
            lvm.Name = "level 1";
            LevelCollection.Add(lvm);
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
