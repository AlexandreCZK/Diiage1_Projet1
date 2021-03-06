using System;
using System.Collections.Generic;
using System.Text;

namespace Diiage2020.CsharpGame.Entities
{
    public class Company
    {
        private static int _id = 0;
        public int Id { get; set; }
        public string Name { get; set; }
        public double SuccessRate { get; set; }
        public double Budget { get; set; }
        public List<StaffMember> StaffMembers { get; set; }
        public CompanyType CompanyType { get; set; }
        public Player Player { get; set; }
        public bool TurnPass { get; set; }

        public Company()
        {
            _id++;
            this.Id = _id;
            this.StaffMembers = new List<StaffMember>();
            this.TurnPass = false;
        }
        public Company(string name, double successRate, double budget, CompanyType companyType, Player player) :this()
        {
            this.Name = name;
            this.SuccessRate = successRate;
            this.Budget = budget;
            this.CompanyType = companyType;
            this.Player = player;
        }
    }
}
