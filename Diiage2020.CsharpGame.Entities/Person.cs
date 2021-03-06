using System;
using System.Collections.Generic;
using System.Text;

namespace Diiage2020.CsharpGame.Entities
{
    public class Person
    {
        private static int _id = 0;
        public int Id { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Person()
        {
            _id++;
            this.Id = _id;
        }
        public Person(string firstName, string lastName):this()
        {
            this.FirstName = firstName;
            this.LastName = lastName;
        }
    }
}
