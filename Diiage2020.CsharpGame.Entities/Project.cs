using System;
using System.Collections.Generic;
using System.Text;

namespace Diiage2020.CsharpGame.Entities
{
    public class Project
    {
        private static int _id = 0;
        public int Id { get; set; }
        public string Title { get; set; }
        public int Duration { get; set; } //months = turns
        public double Cost { get; set; }
        public int Start { get; set; }  // turn
        public List<StaffMember> Members { get; set; }
        public List<Certification> Requirements { get; set; }
        public bool IsSuccessFul { get; set; }

        public Project()
        {
            _id++;
            Id = _id;
            this.Members = new List<StaffMember>();
            this.Requirements = new List<Certification>();
        }
    }
}
