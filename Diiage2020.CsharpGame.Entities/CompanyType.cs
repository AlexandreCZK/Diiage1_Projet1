using System;
using System.Collections.Generic;
using System.Text;

namespace Diiage2020.CsharpGame.Entities
{
    public class CompanyType
    {
        private static int _id = 0;
        public int Id { get; set; }
        public string Title { get; set; }
        public int SalariesLimite { get; set; }
        public CompanyType()
        {
            _id++;
            Id = _id;
        }
        public CompanyType(string title, int salariesLimite):this()
        {
            this.Title = title;
            this.SalariesLimite = salariesLimite;
        }
    }
}
