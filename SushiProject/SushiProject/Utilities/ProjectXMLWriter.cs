using Microsoft.Win32;
using SushiProject.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;

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
            foreach (Level go in gameProject.Levels){
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

            ///<Image>Images
            XElement gameImagesXml = new XElement("Images");
            foreach (Image go in gameProject.Images)
            {
                XElement goImgXml = new XElement("Image");
                goImgXml.Add(new XElement("Name", go.Name));
                goImgXml.Add(new XElement("FilePath", go.FilePath));
                gameImagesXml.Add(goImgXml);
            }
            xml.Add(gameImagesXml);


            ///<Setting>Settings
            XElement gameSettingsXml = new XElement("Settings");
            gameSettingsXml.Add(new XElement("ScreenWidth",GameSettings.ScreenWidth));
            gameSettingsXml.Add(new XElement("ScreenHeight", GameSettings.ScreenHeight));
            gameSettingsXml.Add(new XElement("ScreenWidth", GameSettings.BackgroundColor));
            xml.Add(gameSettingsXml);


            System.Console.Write(xml);

            xml.Save(filepath);
        }

        public void OpenProject(GameProject gameProject)
        {

        }
    }
}
