using Diiage2020.CsharpGame.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diiage2020.CsharpGames.Client.UWP.ViewModel
{
    public class TrainingVM
    {
        private string tSTitle;
        private int tSDur;
        private double tSPrice;
        private string school;
        private Training training;

        public Training Training
        {
            get { return training; }
            set { training = value; }
        }


        public string School
        {
            get { return school; }
            set { school = value; }
        }

        private List<string> lstCertif;

        public List<string> LstCertif
        {
            get { return lstCertif; }
            set { lstCertif = value; }
        }


        public double TSPrice
        {
            get { return tSPrice; }
            set { tSPrice = value; }
        }


        public int TSDur
        {
            get { return tSDur; }
            set { tSDur = value; }
        }


        public string TSTitle
        {
            get { return tSTitle; }
            set { tSTitle = value; }
        }

    }
}
