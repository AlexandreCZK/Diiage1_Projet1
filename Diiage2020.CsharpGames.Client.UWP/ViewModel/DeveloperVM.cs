using Diiage2020.CsharpGame.Client.Services;
using Diiage2020.CsharpGame.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diiage2020.CsharpGames.Client.UWP.ViewModel
{
    public class DeveloperVM
    {
        private string firstName;
        private string lastName;
        private double salary;
        private List<string> lstCertifDev;
        private Developer developer;

        public Developer Developer
        {
            get { return developer; }
            set { developer = value; }
        }


        public List<string> LstCertifDev
        {
            get { return lstCertifDev; }
            set { lstCertifDev = value; }
        }


        public double Salary
        {
            get { return salary; }
            set { salary = value; }
        }

        public string LastName
        {
            get { return lastName; }
            set { lastName = value; }
        }

        public string FirstName
        {
            get { return firstName; }
            set { firstName = value; }
        }

        
    }
}
