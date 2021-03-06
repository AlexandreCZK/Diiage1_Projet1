using System;
using System.Collections.Generic;
using System.Text;

namespace Diiage2020.CsharpGame.Entities
{
    public class Game
    {
        private static int _id = 0; 
        public int Id { get; set; }
        public List<Turn> Turns { get; set; }
        public List<Company> Companies { get; set; }
        public List<Player> Players { get; set; }
        public int PlayerMax { get; set; }
        public int TurnMax { get; set; }
        public int StartBudget { get; set; }
        public Player Winner { get; set; }
        public Player Admin { get; set; }
        public List<Training> Trainings { get; set; }
        /// <summary>
        /// 3 etat :
        /// - creation
        /// - cours
        /// - finish
        /// </summary>
        public string Etat { get; set; }

        public Game()
        {
            _id++;
            this.Id = _id;
            this.Turns = new List<Turn>();
            this.Companies = new List<Company>();
            this.Players = new List<Player>();
            this.Trainings = new List<Training>();
        }
        public Game(int playerMax, int turnMax, int startBudget, Player admin) :this()
        {
            this.PlayerMax = playerMax;
            this.TurnMax = turnMax;
            this.StartBudget = startBudget;
            this.Admin = admin;
        }
        public Game(int playerMax, int turnMax, int startBudget, Player admin, string etat) : this(playerMax, turnMax, startBudget, admin)
        {
            this.Etat = etat;
        }
    }
}
