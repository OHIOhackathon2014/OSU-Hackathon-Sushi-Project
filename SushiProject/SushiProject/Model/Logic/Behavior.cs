﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SushiProject.Model.Logic
{

    public class Behavior
    {
        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public Collection<Behavior> AttachedBehaviors { get; set; }

        public Behavior()
        {
            AttachedBehaviors = new Collection<Behavior>();
        }

        

    }
}
