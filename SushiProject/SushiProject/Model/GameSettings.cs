using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace SushiProject.Model
{
    public class GameSettings
    {
        public int ScreenWidth { get; set; }
        public int ScreenHeight { get; set; }
        public float FrameRate { get; set; }
        public Color BackgroundColor { get; set; }

        public GameSettings()
        {
            ScreenWidth = 640;
            ScreenHeight = 480;
            FrameRate = 60;
            BackgroundColor = Colors.CornflowerBlue;

            var writer = new SushiProject.Utilities.ProjectAS3Writer();
            writer.WriteMain(this);
        }

        
    }
}
