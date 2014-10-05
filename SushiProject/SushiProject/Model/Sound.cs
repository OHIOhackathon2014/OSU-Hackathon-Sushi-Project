using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SushiProject.Model
{
    public class Sound
    {
        public string Name { get; set; }
        public string FilePath { get; set; }

        public string FileName
        {
            get
            {
                return Path.GetFileName(FilePath);
            }
        }

        public Sound(string filePath, string name)
        {
            Name = name;
            FilePath = filePath;
        }
        public Sound()
        {
            Name = "";
            FilePath = "";
        }

    }
}
