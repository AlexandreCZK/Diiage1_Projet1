using System;
using System.Collections.Generic;
using System.Text;

namespace Diiage2020.CsharpGame.Entities
{
    public class Turn
    {
        private static int _id = 0;
        public int Id { get; set; }
        public List<Project> NewProjects { get; set; }
        public List<Project> FailedProjects { get; set; }
        public List<Developer> NewDevelopers { get; set; }
        public List<School> Schools { get; set; }
        public List<TrainingSession> StartingTrainingSessions { get; set; }

        public Turn()
        {
            _id++;
            Id = _id;
            this.NewProjects = new List<Project>();
            this.FailedProjects = new List<Project>();
            this.NewDevelopers = new List<Developer>();
            this.Schools = new List<School>();
            this.StartingTrainingSessions = new List<TrainingSession>();
        }
    }
}
