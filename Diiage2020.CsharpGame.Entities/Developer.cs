using System;
using System.Collections.Generic;
using System.Text;

namespace Diiage2020.CsharpGame.Entities
{
    public class Developer : Person
    {

        public List<Certification> Certifications { get; set; }
        public List<Project> Projects { get; set; }
        public StaffMember StaffMember { get; set; }
        public double Salary { get; set; }

        public Developer()
        {
            this.Certifications = new List<Certification>();
            this.Projects = new List<Project>();
        }
        public Developer(double salary):this()
        {
            this.Salary = salary;
        }
        public Developer(double salary, string firstName, string lastName) : base(firstName, lastName)
        {
            this.Salary = salary;
            this.Certifications = new List<Certification>();
            this.Projects = new List<Project>();
        }
    }
}
