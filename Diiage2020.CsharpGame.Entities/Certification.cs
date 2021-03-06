using System;
using System.Collections.Generic;
using System.Text;

namespace Diiage2020.CsharpGame.Entities
{
    public class Certification
    {
        private static int _id = 0;
        public int Id { get; set; }

        public Level Level { get; set; }
        public Field Field { get; set; }

        public Certification()
        {
            _id++;
            Id = _id;
        }
        public Certification(Level level, Field field):this()
        {
            this.Level = level;
            this.Field = field;
        }
    }
}
