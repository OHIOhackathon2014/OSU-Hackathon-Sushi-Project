using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SushiProject.Model
{
    public class GameProject
    {
        private string name = "noname";
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        
        public Collection<Prefab> Prefabs { get; set; }
        public Collection<Level> Levels { get; set; }
        public GameSettings Settings { get; set; }

        public GameProject()
        {

        }


    }
}
