using System;
using System.Collections.Generic;
using System.Text;

namespace Diiage2020.CsharpGame.Entities
{
    public class Player : Person
    {
        public string Pseudo { get; set; }
        public string Password { get; set; }
        public Player():base()
        {

        }
        public Player(string firstName, string lastName):base(firstName, lastName)
        {

        }
        public Player(string firstName, string lastName, string pseudo, string password):base(firstName, lastName)
        {
            this.Pseudo = pseudo;
            this.Password = password;
        }
        public override bool Equals(Object obj)
        {
            bool result = false;

            if (obj!=null && obj.GetType().Equals(this.GetType()))
            {
                Player equalObject = (Player)obj;
                result =
                    this.FirstName == equalObject.FirstName &&
                    this.LastName == equalObject.LastName &&
                    this.Pseudo == equalObject.Pseudo;
            }

            return result;
        }

    }
}
