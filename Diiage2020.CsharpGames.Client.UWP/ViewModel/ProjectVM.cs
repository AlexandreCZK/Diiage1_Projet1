using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diiage2020.CsharpGames.Client.UWP.ViewModel
{
    public class ProjectVM
    {
        private string nomProjet;
        private int duree;
        private double cout;
        private List<string> lvlDescriptionCertif;
        private List<string> lesDevPrenom;

        public List<string> LesDevPrenom
        {
            get { return lesDevPrenom; }
            set { lesDevPrenom = value; }
        }


        public List<string> LvlDescriptionCertif
        {
            get { return lvlDescriptionCertif; }
            set { lvlDescriptionCertif = value; }
        }


        public double Cout
        {
            get { return cout; }
            set { cout = value; }
        }


        public int Duree
        {
            get { return duree; }
            set { duree = value; }
        }


        public string NomProjet
        {
            get { return nomProjet; }
            set { nomProjet = value; }
        }

        

    }
}
