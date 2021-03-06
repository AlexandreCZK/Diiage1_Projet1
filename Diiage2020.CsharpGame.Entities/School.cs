using System;
using System.Collections.Generic;
using System.Text;

namespace Diiage2020.CsharpGame.Entities
{
    public class School
    {
        private static int _id = 0;
        public int Id { get; set; }
        public string Name { get; set; }
        public List<TrainingSession> TrainingSessions { get; set; }

        public School()
        {
            _id++;
            Id = _id;
            this.TrainingSessions = new List<TrainingSession>();
        }
        public School(string name):this()
        {
            this.Name = name;
        }
    }
}
