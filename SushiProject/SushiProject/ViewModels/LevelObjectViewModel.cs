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
    public class LevelObjectViewModel : INotifyPropertyChanged
    {
        public Level level { get; set; }

        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                level.Name = name;
                OnPropertyChanged("Name");
            }
        }

        public LevelObjectViewModel()
        {
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
