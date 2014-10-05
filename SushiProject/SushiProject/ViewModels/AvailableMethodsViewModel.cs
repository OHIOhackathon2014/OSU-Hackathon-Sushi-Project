using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

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

        public void AddMethod(string name, string imgName)
        {
            MethodViewModel mvm = new MethodViewModel();
            mvm.Name = name;
            mvm.Icon = new BitmapImage(new Uri(String.Format("../Assets/Methods/{0}.png", imgName), UriKind.Relative));
            AvailableMethods.Add(mvm);
        }

        public void PopulateAvailableMethods()
        {
            AddMethod("Spawn Instance", "newinstance");
            AddMethod("Move", "changeposition");
            AddMethod("Set Scale", "scale");
            AddMethod("Set Transparency", "transparency");
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
