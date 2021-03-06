using System;
using System.Collections.Generic;
using System.Text;

namespace Diiage2020.CsharpGame.Entities
{
    public class StaffMember
    {
        public Company Company { get; set; }
        public Developer Developer { get; set; }
        public Turn StartTurn { get; set; }
        public Turn EndTurn { get; set; }
        List<Project> Projects { get; set; } //Public ?

        public StaffMember()
        {
            this.Projects = new List<Project>();
        }
        public StaffMember(Company company, Developer developer, Turn startTurn, Turn endTurn):this()
        {
            this.Company = company;
            this.Developer = developer;
            this.StartTurn = startTurn;
            this.EndTurn = endTurn;
        }
        public override bool Equals(object obj)
        {
            bool result = false;
            if (obj != null && obj.GetType().Equals(this.GetType()))
            {
                StaffMember objStaff = obj as StaffMember;
                result =
                    this.Company.Id == objStaff.Company.Id &&
                    this.Developer.Id == objStaff.Developer.Id;
            }
            return result;
        }
    }
}
