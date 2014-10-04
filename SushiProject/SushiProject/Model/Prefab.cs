using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SushiProject.Model
{
    class Prefab
    {
        public string Name { get; set; }

        public Dictionary<string, float> Variables { get; set; }
        public Dictionary<string, Behavior> Behaviors { get; set; }

        Prefab()
        {
            Variables = new Dictionary<string, float>();
            Behaviors = new Dictionary<string, Behavior>();
        }

    }
}
