using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SushiProject.Model
{
    public class PrefabInstance
    {
        public Prefab prefab;
        public Point Position { get; set; }
        public float Rotation { get; set; }

        public PrefabInstance(Prefab p)
        {
            prefab = p;
            Position = new Point();
            Rotation = 0;
        }

        
    }
}
