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
        public double ScaleX { get; set; }
        public double ScaleY { get; set; }
        public GameObject Class { get; set; }

        public LevelObject(GameObject Class, double X, double Y, double Rotation, double ScaleX, double ScaleY)
        {
            this.X = X;
            this.Y = Y;
            this.ScaleX = ScaleX;
            this.ScaleY = ScaleY;
            this.Class = Class;
            this.Rotation = Rotation;
        }
        public LevelObject(GameObject Class, double X, double Y)
        {
            this.X = X;
            this.Y = Y;
            this.Class = Class;
            Rotation = 0;
        }
        public LevelObject()
        {
            this.X = 0;
            this.Y = 0;
            this.Class = null;
            Rotation = 0;
        }
    }
}
