using Microsoft.Win32;
using SushiProject.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;
using System.Windows.Media;
using System.Globalization;

namespace SushiProject.Utilities
{
    public class ProjectXMLWriter
    {
        public ProjectXMLWriter()
        {

        }

        public string AskForSavePath(string defaultFilename)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.FileName = defaultFilename; // Default file name
            dialog.DefaultExt = ".sushi"; // Default file extension
            dialog.Filter = "Sushi Project Files (.sushi)|*.sushi"; // Filter files by extension 

            // Show save file dialog box
            Nullable<bool> result = dialog.ShowDialog();

            // Process save file dialog box results 
            if (result == true)
            {
                // Save document 
                string filename = dialog.FileName;
                return filename;
            }
            else
            {
                return string.Empty;
            }
        }

        public void SaveProject(GameProject gameProject)
        {
            string filepath = AskForSavePath(gameProject.Name);
            if (string.IsNullOrEmpty(filepath)) return;
            // Mostly validate filepath with a try catch
            try
            {
                FileInfo finfo = new FileInfo(filepath);
            }
            catch
            {
                return;
            }
            XElement xml = new XElement("Project");
            xml.Add(new XElement("Name", gameProject.Name));

            ///<GameObject>Objects
            XElement gameObjectsXml = new XElement("Objects");
            foreach (GameObject go in gameProject.Objects)
            {
                XElement goXml = new XElement("Object");
                goXml.Add(new XElement("Name", go.Name));
                gameObjectsXml.Add(goXml);
            }
            xml.Add(gameObjectsXml);

            ///<Level>Levels
            XElement gameLevelsXml = new XElement("Levels");
            foreach (Level go in gameProject.Levels)
            {
                XElement goLevXml = new XElement("Level");
                goLevXml.Add(new XElement("Name", go.Name));

                ///<LevelObject> levelObjects
                foreach (LevelObject levelobjgo in go.levelObjects)
                {
                    XElement levelObjs = new XElement("LevelObject");
                    levelObjs.Add(new XElement("X", levelobjgo.X));
                    levelObjs.Add(new XElement("Y", levelobjgo.Y));
                    levelObjs.Add(new XElement("Rotation", levelobjgo.Rotation));
                    levelObjs.Add(new XElement("Class.Name", levelobjgo.Class.Name));
                    goLevXml.Add(levelObjs);
                }

                gameLevelsXml.Add(goLevXml);
            }
            xml.Add(gameLevelsXml);

            ///<Sprite>Sprites
            XElement spritesXml = new XElement("Sprites");
            foreach (Sprite sprite in gameProject.Sprites)
            {
                XElement spriteXml = new XElement("Sprite");
                spriteXml.Add(new XElement("Name", sprite.Name));
                spriteXml.Add(new XElement("FrameRate", sprite.FrameRate));

                ///<Image>Images
                XElement imagesXml = new XElement("Images");
                spriteXml.Add(imagesXml);
                foreach (Image image in sprite.Images)
                {
                    XElement imgXml = new XElement("Image");
                    imgXml.Add(new XElement("Name", image.Name));
                    imgXml.Add(new XElement("FilePath", image.FilePath));
                    imagesXml.Add(imgXml);
                }

                spritesXml.Add(spriteXml);
            }
            xml.Add(spritesXml);


            ///<Settings>Settings
            XElement gameSettingsXml = new XElement("Settings");
            gameSettingsXml.Add(new XElement("ScreenWidth", gameProject.Settings.ScreenWidth));
            gameSettingsXml.Add(new XElement("ScreenHeight", gameProject.Settings.ScreenHeight));
            gameSettingsXml.Add(new XElement("BackgroundColor", gameProject.Settings.BackgroundColor));
            gameSettingsXml.Add(new XElement("FrameRate", gameProject.Settings.FrameRate));
            xml.Add(gameSettingsXml);


            System.Console.Write(xml);

            xml.Save(filepath);
        }

        public void OpenProject(GameProject gameProject)
        {
            string filepath = AskForOpenPath(gameProject.Name);
            if (string.IsNullOrEmpty(filepath)) return;
            // Mostly validate filepath with a try catch
            try
            {
                FileInfo finfo = new FileInfo(filepath);
            }
            catch
            {
                return;
            }
            XElement xml = XElement.Load(filepath);

            if (xml == null) return;
            
            gameProject.Name = xml.Element("Name").Value;
            
            ///<GameObject>Objects
            gameProject.Objects = new Collection<GameObject>();
            XElement gameObjectsXml = xml.Element("Objects");
            foreach (XElement objectXml in gameObjectsXml.Elements("Object"))
            {
                GameObject go = new GameObject();
                go.Name = objectXml.Element("Name").Value;
                gameProject.Objects.Add(go);
            }


            ///<Level>Levels
            XElement gameLevelsXml = xml.Element("Levels");
            gameProject.Levels = new Collection<Level>();
            foreach (XElement levelXml in gameLevelsXml.Elements("Level"))
            {
                Level level = new Level();
                level.Name = levelXml.Element("Name").Value;
                gameProject.Levels.Add(level);

                ///<LevelObject> levelObjects
                foreach (XElement levelObjXml in levelXml.Elements("LevelObject"))
                {
                    double x = double.Parse(levelObjXml.Element("X").Value);
                    double y = double.Parse(levelObjXml.Element("Y").Value);
                    double rot = double.Parse(levelObjXml.Element("Rotation").Value);
                    string classname = levelObjXml.Element("Class.Name").Value;
                    foreach (GameObject go in gameProject.Objects)
                    {
                        if (go.Name == classname)
                        {
                            level.levelObjects.Add(new LevelObject(go, x, y, rot));
                            break;
                        }
                    }
                }
            }

            ///<Sprite>Sprites
            gameProject.Sprites = new Collection<Sprite>();
            XElement gameSpritesXml = xml.Element("Sprites");
            foreach (XElement spriteXml in gameSpritesXml.Elements("Sprite"))
            {
                Sprite sprite = new Sprite();
                sprite.Name = spriteXml.Element("Name").Value;
                sprite.FrameRate = int.Parse(spriteXml.Element("FrameRate").Value);

                ///<Image>Images
                sprite.Images = new ObservableCollection<Image>();
                XElement imagesXml = spriteXml.Element("Images");
                foreach (XElement imageXml in imagesXml.Elements("Image"))
                {
                    string name = imageXml.Element("Name").Value;
                    string imgFilepath = imageXml.Element("FilePath").Value;
                    sprite.Images.Add(new Image(imgFilepath, name));
                }
                gameProject.Sprites.Add(sprite);
            }

            ///<Settings>Settings
            gameProject.Settings = new GameSettings();
            XElement gameSettingsXml = xml.Element("Settings");
            gameProject.Settings.ScreenWidth = int.Parse(gameSettingsXml.Element("ScreenWidth").Value);
            gameProject.Settings.ScreenHeight = int.Parse(gameSettingsXml.Element("ScreenHeight").Value);

            string colorString = gameSettingsXml.Element("BackgroundColor").Value.Substring(3);
            byte r = byte.Parse(colorString.Substring(0,2), NumberStyles.HexNumber);
            byte g = byte.Parse(colorString.Substring(2,2), NumberStyles.HexNumber);
            byte b = byte.Parse(colorString.Substring(4,2), NumberStyles.HexNumber);
            Color newColor = new Color();
            newColor = Color.FromRgb(r, g, b);
            gameProject.Settings.BackgroundColor = newColor;

            gameProject.Settings.FrameRate = int.Parse(gameSettingsXml.Element("FrameRate").Value);

        }

        public string AskForOpenPath(string defaultFilename)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.FileName = defaultFilename; // Default file name
            dialog.DefaultExt = ".sushi"; // Default file extension
            dialog.Filter = "Sushi Project Files (.sushi)|*.sushi"; // Filter files by extension 

            // Show open file dialog box
            Nullable<bool> result = dialog.ShowDialog();

            // Process save file dialog box results 
            if (result == true)
            {
                // Save document 
                string filename = dialog.FileName;
                return filename;
            }
            else
            {
                return string.Empty;
            }
        }
    }
}
