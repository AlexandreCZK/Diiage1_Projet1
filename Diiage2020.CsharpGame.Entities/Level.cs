using System;
using System.Collections.Generic;
using System.Text;

namespace Diiage2020.CsharpGame.Entities
{
    public class Level
    {
        private static int _id = 0;
        public int Id { get; set; }
        public int Niveau { get; set; }
        public string Description { get; set; }

        public Level()
        {
            _id++;
            Id = Id;
        }
        public Level(int niveau, string description):this()
        {
            this.Niveau = niveau;
            this.Description = description;
        }
    }

}
