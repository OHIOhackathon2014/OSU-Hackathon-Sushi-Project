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
            System.Console.Write(xml);

            xml.Save(filepath);
        }

        public void OpenProject(GameProject gameProject)
        {

        }
    }
}
