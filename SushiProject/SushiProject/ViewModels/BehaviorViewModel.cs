using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SushiProject.ViewModels
{
    public class BehaviorViewModel : INotifyPropertyChanged
    {
        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }
        private ObservableCollection<MethodViewModel> methods = new ObservableCollection<MethodViewModel>();
        public ObservableCollection<MethodViewModel> Methods {
            get
            {
                return methods;
            }
            set
            {
                methods = value;
                OnPropertyChanged("Methods");
            }
        }

        public BehaviorViewModel()
        {
            Name = "Update";
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
