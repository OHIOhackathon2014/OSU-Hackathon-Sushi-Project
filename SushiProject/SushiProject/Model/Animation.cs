using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SushiProject.Model
{
    class Animation
    {
        public List<Image> Images { get; set; }
        public float FrameRate { get; set; }

        Animation()
        {
            Images = new List<Image>();
        }

        void Add(Image image) {
            Images.Add(image);
        }

        void Remove(int i)
        {
            Images.RemoveAt(i);
        }
    }
}
