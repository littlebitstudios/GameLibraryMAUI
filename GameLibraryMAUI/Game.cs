using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LittleBitsGameLibrary
{
    internal class Game
    {
        public string name = "";
        public string developer = "";
        public string description = "";
        public string imagepath = "";
        public string genre = "";

        public Game(string name, string developer, string description, string imagepath, string genre)
        {
            this.name = name;
            this.developer = developer;
            this.description = description;
            this.imagepath = imagepath;
            this.genre = genre;
        }
        public Game()
        {

        }
    }
}