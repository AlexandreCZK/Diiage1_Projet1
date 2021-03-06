using Diiage2020.CsharpGame.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestCompany()
        {
            Company company = new Company();

            List<Developer> developers = new List<Developer>();
            for (int i = 0; i < 10; i++)
            {
                Developer developer = new Developer();
                developers.Add(developer);
            }
            company.StaffMembers.Add(
                new StaffMember
                {
                    Developer = developers[0],
                    StartTurn = new Turn(),
                    EndTurn = new Turn()
                }
                );
            company.StaffMembers.Add(
                new StaffMember
                {
                    Developer = developers[2],
                    StartTurn = new Turn(),
                    EndTurn = new Turn()
                }
                );
            company.StaffMembers.Add(
                new StaffMember
                {
                    Developer = developers[5],
                    StartTurn = new Turn(),
                    EndTurn = new Turn()
                }
                );
            var expected = 3;
            var actual = company.StaffMembers.Count;
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void TestClass()
        {
            //Creer une game
            Game game = new Game { Admin = new Player() };

            //Ajouter un player a la game
            game.Players.Add(new Player());

            //Ajouter une entreprise a la partie
            game.Companies.Add(
                new Company
                {
                    CompanyType = new CompanyType(),
                    Player = game.Players[0]
                });

            //Ajouter un tour a la partie
            game.Turns.Add(new Turn());

            //on ajoute une ecole au tour
            game.Turns[0].Schools.Add(new School());

            //Ajouter une seesion de training a la game
            game.Turns[0].Schools[0].TrainingSessions.Add(new TrainingSession());

            //on ajoute un training a la session de training
            game.Trainings.Add(new Training());
            
            //on reference la session de training au training
            game.Trainings[0].TrainingSession = game.Turns[0].Schools[0].TrainingSessions[0];

            //on ajoute la certification au training session
            game.Trainings[0].TrainingSession.Certifications.Add(new Certification());

            //ajouter un projet au tour
            game.Turns[0].NewProjects.Add(new Project());
            game.Turns[0].NewProjects[0].Requirements.Add(new Certification
            {
                Field = new Field(),
                Level = new Level()
            });

            //on ajoute un staffmember a la compagnie
            game.Companies[0].StaffMembers.Add(new StaffMember
            {
                Company = game.Companies[0],
                Developer = new Developer(),
                EndTurn = new Turn(),
                StartTurn = new Turn(),
            });
            game.Companies[0].StaffMembers.Add(new StaffMember
            {
                Company = game.Companies[0],
                Developer = new Developer(),
                EndTurn = new Turn(),
                StartTurn = new Turn(),
            });

            //ajoute un projet au staff member de l'entreprise
            game.Companies[0].StaffMembers[0].Developer.Projects.Add(game.Turns[0].NewProjects[0]);
            game.Companies[0].StaffMembers[0].Developer.Projects[0].Members.Add(game.Companies[0].StaffMembers[0]);

            //on envoie en training un staff member
            game.Trainings[0].StaffMember = game.Companies[0].StaffMembers[1];

            //le dev a fini son training on ajoute la certif du training a ses certif
            game.Companies[0].StaffMembers[1].Developer.Certifications.Add(game.Trainings[0].TrainingSession.Certifications[0]);
            game.Trainings[0].EndTurn = new Turn();
            game.Trainings[0].StartTurn = new Turn();

            //un joueurs a gagner
            game.Winner = game.Players[0];
        }
        [TestMethod]
        public void TestMethod1()
        {
            List<int> numbers = new List<int> { 1, 2, 3, 4, 5 };
            var json = SerializeJson.Serialise.ToJson<List<int>>(numbers);

            var listeDeSerializeNumbers = SerializeJson.Serialise.FromJson<List<int>>(json);

            for (int i = 0; i < numbers.Count; i++)
            {
                Assert.AreEqual(numbers[i], listeDeSerializeNumbers[i]);
            }

            Assert.AreEqual(4, numbers.First(number => number == 2 * 2));
        }
        [TestMethod]
        public void TestMethod2()
        {
            List<string> numbers = new List<string> { "DFC", "OL", "OM", "PSG" };
            var json = SerializeJson.Serialise.ToJson<List<string>>(numbers);

            var listeDeSerializeNumbers = SerializeJson.Serialise.FromJson<List<string>>(json);

            for (int i = 0; i < numbers.Count; i++)
            {
                Assert.AreEqual(numbers[i], listeDeSerializeNumbers[i]);
            }

            numbers.First();
            Assert.AreEqual("DFC", numbers.First());

            var clubs = numbers.Select(club => club.IndexOf("O"));
            var listeClub = clubs.ToList();

        }
    }
}