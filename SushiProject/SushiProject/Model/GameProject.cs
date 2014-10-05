using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SushiProject.Model
{
    public class GameProject
    {
        private string name = "noname";
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        
        public Collection<GameObject> Objects { get; set; }
        public Collection<Level> Levels { get; set; }
        public Collection<Sprite> Sprites { get; set; }
        public Collection<Sound> Sounds { get; set; }
        public GameSettings Settings { get; set; }

        public GameProject()
        {
            Objects = new Collection<GameObject>();
            GameObject go = new GameObject();
            go.Name = "ball";
            Objects.Add(go);

            Levels = new Collection<Level>();
            Level lo = new Level();
            lo.Name = "level 1";
            Levels.Add(lo);

            Sprites = new Collection<Sprite>();
            Sprite spr = new Sprite();
            spr.Name = "ballsprite";
            Sprites.Add(spr);

            Sounds = new Collection<Sound>();
            Sound sound = new Sound();
            sound.Name = "ballsound";
            Sounds.Add(sound);

            Settings = new GameSettings();
        }
    }
}
