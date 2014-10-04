using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SushiProject.Model
{
    public class LevelObject
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Rotation { get; set; }
        public GameObject Class { get; set; }

        public LevelObject(GameObject Class, double X, double Y)
        {
            this.X = X;
            this.Y = Y;
            this.Class = Class;
            Rotation = 0.0;
        }
    }
}
