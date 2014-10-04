using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SushiProject.Model
{
    class GameProject
    {
        public List<Prefab> Prefabs { get; set; }
        public List<Level> Levels { get; set; }
        public GameSettings Settings { get; set; }


        GameProject()
        {

        }
    }
}
