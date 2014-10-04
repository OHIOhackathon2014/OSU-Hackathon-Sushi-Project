using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SushiProject.Model
{
    public class Level
    {
        public string Name { get; set; }
        public Collection<LevelObject> levelObjects { get; set; }

        public Level()
        {
            levelObjects = new Collection<LevelObject>();
        }
    }
}
