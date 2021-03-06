using System;
using System.Collections.Generic;
using System.Text;

namespace Diiage2020.CsharpGame.Entities
{
    public class TrainingSession
    {
        private static int _id = 0;
        public int Id { get; set; }

        public string Title { get; set; }
        public int Duration { get; set; } //turn  =month
        public double Price { get; set; }
        public int Capacity { get; set; }
        public School School { get; set; }
        public Training Training { get; set; }
        public List<Certification> Certifications { get; set; }

        public TrainingSession()
        {
            _id++;
            Id = _id;
            this.Certifications = new List<Certification>();
        }
        public TrainingSession(int id, string title, int duration, double price, int capacity, School school, Training training):this()
        {
            this.Id = id;
            this.Title = title;
            this.Duration = duration;
            this.Price = price;
            this.Capacity = capacity;
            this.School = school;
            this.Training = training;
        }
    }
}
