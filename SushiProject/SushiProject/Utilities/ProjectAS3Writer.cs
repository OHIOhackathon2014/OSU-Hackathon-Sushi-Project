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

            if (!Directory.Exists(path + "assets"))
            {
                Directory.CreateDirectory(path + "assets");
            }
            // move all images into here
            ProjectDirectoryManager.Instance.CopyAllImagesToFolder(path + "assets");

            WriteMain(project.Settings);
            WriteGameObjects(project.Objects);
            WriteLevels(project.Levels);
            WriteAssets(project);
            WriteHelperClasses(project);
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
        private string ImageResourceName(string name)
        {
            string str = name.Replace(" ", "_");
            int extStart = str.IndexOf(".");
            if (extStart > 0) {
                str = str.Substring(0,extStart);
            }
            return String.Format("IMG_{0}", str);
        }

        private void WriteGameObjects(Collection<GameObject> gameObjects)
        {
            string templateText = System.IO.File.ReadAllText("../../Templates/GameObjectTemplate");

            foreach (GameObject go in gameObjects)
            {
                string gameObjectText = templateText.Replace("!NAME!", go.Name.Replace(" ", "_"));
                if (go.Sprite != null)
                {
                    gameObjectText = gameObjectText.Replace("!SPRITEFIELDS!", "public var img:Bitmap;");
                    gameObjectText = gameObjectText.Replace("!INITSPRITE!", String.Format("img = new Assets.{0}();", ImageResourceName(go.Sprite.FirstImage.Name)));
                    gameObjectText = gameObjectText.Replace("!ADDSPRITE!", "addChild(img);");
                }
                else
                {
                    gameObjectText = gameObjectText.Replace("!SPRITEFIELDS!", "");
                    gameObjectText = gameObjectText.Replace("!INITSPRITE!", "");
                    gameObjectText = gameObjectText.Replace("!ADDSPRITE!", "");
                }

                System.IO.File.WriteAllText(path + go.Name.Replace(" ", "_") + ".as", gameObjectText);
            }
        }

        private void WriteLevels(Collection<Level> levels)
        {
            string templateText = System.IO.File.ReadAllText("../../Templates/SceneTemplate");

            foreach (Level level in levels)
            {
                string levelText = templateText.Replace("!SCENECLASSNAME!", level.Name.Replace(" ", "_"));
                int counter = 0;
                string initgameobjects = "";

                foreach (LevelObject lo in level.levelObjects)
                {
                    initgameobjects += "\t\t\tvar go_"+counter.ToString() +":" + lo.Class.Name.Replace(" ", "_") + " = new "+ lo.Class.Name.Replace(" ", "_") +"(" + lo.X.ToString() + ", " + lo.Y.ToString() + ");\n";
                    initgameobjects += "\t\t\tgameObjectsList.push(go_"+counter.ToString()+");\n";
                    ++counter;
                }
                
                levelText = levelText.Replace("!INITGAMEOBJECTS!", initgameobjects);
                System.IO.File.WriteAllText(path + level.Name.Replace(" ", "_") + ".as", levelText);
            }

            string sceneManagerTemplateText = System.IO.File.ReadAllText("../../Templates/SceneManagerTemplate");
            string sceneManagerText = sceneManagerTemplateText;
            string levelsadded = "";
            foreach (Level level in levels)
            {
                levelsadded += "\t\t\tvar lvl_" + level.Name.Replace(" ", "_") + ":" + level.Name.Replace(" ", "_") + " = new " + level.Name.Replace(" ", "_") + "();\n";
                levelsadded += "\t\t\tscenes.push(lvl_" + level.Name.Replace(" ", "_") + ");\n";
            }
            sceneManagerText = sceneManagerText.Replace("!ADDLEVELS!", levelsadded);
            sceneManagerText = sceneManagerText.Replace("!FIRSTLEVEL!", "lvl_" + levels[0].Name.Replace(" ", "_"));
            System.IO.File.WriteAllText(path + "SceneManager.as", sceneManagerText);
        }

        private void WriteAssets(GameProject gameProject)
        {
            string templateText = System.IO.File.ReadAllText("../../Templates/AssetsTemplate");
            string assetsText = String.Empty,
                   graphicsList = String.Empty,
                   soundsList = String.Empty,
                   levelsList = String.Empty;

            foreach (Sprite sprite in gameProject.Sprites)
            {
                //[Embed(source = "assets/images/ball.png")] public static var IMG_BALL:Class;
                foreach (Image image in sprite.Images)
                {
                    graphicsList += "[Embed(source = \"assets/" + image.FileName + "\")] public static var " + ImageResourceName(image.Name) + ":Class;\n";
                }
            }
            foreach (Sound sound in gameProject.Sounds)
            {
                //soundsList += "[Embed(source = \"assets/" + sound.FileName + "\")] public static var SND_" + sound.Name.Replace(" ", "_") + ":Class;\n";
            }
            //[Embed(source = 'assets/levels/lvl1.xml', mimeType = "application/octet-stream")] public static const LEVEL1:Class;
            templateText = templateText.Replace("!GRAPHICS!", graphicsList);
            templateText = templateText.Replace("!SOUNDS!",   soundsList  );
            templateText = templateText.Replace("!XML!", levelsList);
            System.IO.File.WriteAllText(path + "Assets.as", templateText.Replace("!ASSETLIST!", assetsText));
        }

        private void WriteHelperClasses(GameProject gameProject)
        {
            string templateText = System.IO.File.ReadAllText("../../Templates/SceneBaseTemplate");
            System.IO.File.WriteAllText(path + "SceneBase.as", templateText);


        }
    }
}
