using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace SushiProject.Utilities
{
    public class ProjectDirectoryManager
    {
        static private ProjectDirectoryManager _instance;
        static public ProjectDirectoryManager Instance {
            get {
                if (_instance == null)
                {
                    _instance = new ProjectDirectoryManager();
                }
                return _instance;
            }
        }

        private string projectName = "test";
        public string ProjectName
        {
            get { return projectName; }
            set { projectName = value; }
        }

        private string path;
        public string Path
        {
            get { return path; }
            set { path = value; }
        }
        
        private string ImagesDirectory = "Images\\";
        private string SoundsDirectory = "Sounds\\";
    
        public ProjectDirectoryManager()
        {
            Path = "Projects\\";
        }

        public void CreateProjectDirectories()
        {
            if (!Directory.Exists(Path)) Directory.CreateDirectory(Path);
            if (!Directory.Exists(Path + ProjectName + "\\")) Directory.CreateDirectory(Path + ProjectName + "\\");
            if (!Directory.Exists(Path + ProjectName + "\\" + ImagesDirectory)) Directory.CreateDirectory(Path + ProjectName + "\\" + ImagesDirectory);
            if (!Directory.Exists(Path + ProjectName + "\\" + SoundsDirectory)) Directory.CreateDirectory(Path + ProjectName + "\\" + SoundsDirectory);
        }

        public string AddImage(string sourceFile)
        {
            string fileName = System.IO.Path.GetFileName(sourceFile);
            string newFileName = Path + ProjectName + "\\" + ImagesDirectory + fileName;
            System.IO.File.Move(sourceFile, newFileName);
            return newFileName;
        }

        public string AddSound(string sourceFile)
        {
            string fileName = System.IO.Path.GetFileName(sourceFile);
            string newFileName = Path + ProjectName + "\\" + SoundsDirectory + fileName;
            System.IO.File.Move(sourceFile, newFileName);
            return newFileName;
        }
    }
}
