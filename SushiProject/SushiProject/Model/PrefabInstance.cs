using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SushiProject.Model
{
    class PrefabInstance
    {
        public Prefab prefab;
        public Vector2 Position { get; set; }
        public float Rotation { get; set; }

        PrefabInstance(Prefab p)
        {
            prefab = p;
            Position = new Vector2();
            Rotation = 0;
        }

        
    }
}
