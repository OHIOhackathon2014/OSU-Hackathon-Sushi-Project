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
    public class ObjectsListViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<ObjectViewModel> ObjectCollection
        {
            get;
            set;
        }

        public GameProject Project { get; set; }

        public ObjectsListViewModel()
        {
            ObjectCollection = new ObservableCollection<ObjectViewModel>();
            ObjectViewModel ovm = new ObjectViewModel();
            ovm.Name = "ball";
            ObjectCollection.Add(ovm);
            ObjectViewModel ovm2 = new ObjectViewModel();
            ovm2.Name = "block";
            ObjectCollection.Add(ovm2);
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
