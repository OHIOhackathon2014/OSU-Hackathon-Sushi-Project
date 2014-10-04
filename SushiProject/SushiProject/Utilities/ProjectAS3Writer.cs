using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SushiProject.Model;
using System.IO;
using System.Collections.ObjectModel;

namespace SushiProject.Utilities
{
    class ProjectAS3Writer
    {
        public string path = "C:/sushigame/";

        public ProjectAS3Writer() { }

        public void WriteASFiles(GameProject project) {

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            WriteMain(project.Settings);
            WriteGameObjects(project.Objects);
            WriteAssets(project);

        }

        private void WriteMain(GameSettings settings) {
            string mainText = System.IO.File.ReadAllText("../../Templates/MainTemplate");

            string bgcolorstring = settings.BackgroundColor.R.ToString("X") + settings.BackgroundColor.G.ToString("X") + settings.BackgroundColor.B.ToString("X");
            mainText = mainText.Replace("!BGCOLOR!", bgcolorstring);
            mainText = mainText.Replace("!WIDTH!", settings.ScreenWidth.ToString());
            mainText = mainText.Replace("!HEIGHT!", settings.ScreenHeight.ToString());
            mainText = mainText.Replace("!FRAMERATE!", settings.FrameRate.ToString());

            // WriteAllText creates a file, writes the specified string to the file, 
            // and then closes the file.
            System.IO.File.WriteAllText(path + "Main.as", mainText);
        }

        private void WriteGameObjects(Collection<GameObject> gameObjects)
        {
            string templateText = System.IO.File.ReadAllText("../../Templates/GameObjectTemplate");

            foreach (GameObject go in gameObjects)
            {
                string gameObjectText = templateText.Replace("!NAME!", go.Name);
                System.IO.File.WriteAllText(path + go.Name + ".as", gameObjectText);
            }
        }

        private void WriteAssets(GameProject gameProject)
        {
            string templateText = System.IO.File.ReadAllText("../../Templates/AssetsTemplate");
            string assetsText = "";
            foreach (Image image in gameProject.Images)
            {
                assetsText += "[Embed src=\"" + image.FilePath + "\"]\nstatic public var " + image.Name + ":Class;\n";
            }
            /*
            foreach (Sound sound in gameProject.Sounds)
            {
                assetsText += "[Embed src=\"" + sound.FilePath + "\"]\nstatic public var " + sound.Name + ":Class;\n";
            }
            */
            System.IO.File.WriteAllText(path + "Assets.as", templateText.Replace("!ASSETLIST!", assetsText));
        }
    }
}
