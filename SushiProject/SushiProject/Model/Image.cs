﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SushiProject.Model
{
    public class Image
    {
        public string FilePath { get; set; }

        public Image(string filePath)
        {
            FilePath = filePath;
        }

    }
}
