using System;
using System.Collections.Generic;
using System.Text;

namespace Diiage2020.CsharpGame.Entities
{
    public class Field
    {
        private static int _id = 0;
        public int Id { get; set; }

        public string Title { get; set; }
        public Field()
        {
            _id++;
            Id = _id;
        }
        public Field(string title):this()
        {
            this.Title = title;
        }
    }
}
