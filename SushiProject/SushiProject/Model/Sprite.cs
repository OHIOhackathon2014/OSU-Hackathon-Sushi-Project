using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SushiProject.Model
{
    public class Sprite
    {
        public Collection<Image> Images { get; set; }
        public float FrameRate { get; set; }
        public string Name { get; set; }

        public Sprite()
        {
            Images = new Collection<Image>();
        }

        public void Add(Image image)
        {
            Images.Add(image);
        }

        public void Remove(int i)
        {
            Images.RemoveAt(i);
        }
    }
}
