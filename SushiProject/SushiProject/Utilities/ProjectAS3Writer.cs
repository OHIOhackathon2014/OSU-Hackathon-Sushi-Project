using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SushiProject.Model;
using System.IO;

namespace SushiProject.Utilities
{
    class ProjectAS3Writer
    {
        public string path = "C:/sushigame/";

        public ProjectAS3Writer() { }

        public void WriteMain(GameSettings settings) {
            string mainText = System.IO.File.ReadAllText("../../Templates/MainTemplate");

            string bgcolorstring = settings.BackgroundColor.R.ToString("X") + settings.BackgroundColor.G.ToString("X") + settings.BackgroundColor.B.ToString("X");
            mainText = mainText.Replace("!BGCOLOR!", bgcolorstring);
            mainText = mainText.Replace("!WIDTH!", settings.ScreenWidth.ToString());
            mainText = mainText.Replace("!HEIGHT!", settings.ScreenHeight.ToString());
            mainText = mainText.Replace("!FRAMERATE!", settings.FrameRate.ToString());

            // WriteAllText creates a file, writes the specified string to the file, 
            // and then closes the file.


            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            System.IO.File.WriteAllText(path + "Main.as", mainText);
        }

        public void WriteGameObjects(List<GameObject> gameObjects)
        {
            string templateText = System.IO.File.ReadAllText("../../Templates/GameObjectTemplate");

            foreach (GameObject go in gameObjects)
            {
                string gameObjectText = templateText.Replace("!NAME!", go.Name);
                System.IO.File.WriteAllText(path + go.Name + ".as", gameObjectText);
            }
        }
    }
}
