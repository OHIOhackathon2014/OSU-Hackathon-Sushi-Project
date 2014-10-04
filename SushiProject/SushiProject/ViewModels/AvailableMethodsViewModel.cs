using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SushiProject.ViewModels
{
    public class AvailableMethodsViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<MethodViewModel> AvailableMethods {get;set;}


        public AvailableMethodsViewModel()
        {
            AvailableMethods = new ObservableCollection<MethodViewModel>();
            PopulateAvailableMethods();
        }
        
        public void AddMethod(string name)
        {
            MethodViewModel mvm = new MethodViewModel();
            mvm.Name = name;
            AvailableMethods.Add(mvm);
        }

        public void PopulateAvailableMethods()
        {
            AddMethod("Spawn Instance");
            AddMethod("Move");
            AddMethod("Set Image Transform");
            AddMethod("Set Variable");
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
