using Diiage2020.CsharpGame.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;

namespace Diiage2020.CSharpGame.Server.Services
{
    public class Service
    {
        private Game GameS;
        private readonly Random random;
        private readonly List<string> trainingSessionTitle;
        private readonly List<string> projectTitle;
        private readonly List<string> nameDev;
        private readonly List<string> lastNameDev;
        private readonly List<Certification> certifications;
        public Service()
        {
            trainingSessionTitle = new List<string>();
            trainingSessionTitle.Add("C#");
            trainingSessionTitle.Add("UWP");
            trainingSessionTitle.Add(".Net");
            trainingSessionTitle.Add("Thread");
            trainingSessionTitle.Add("Socket");

            projectTitle = new List<string>();
            projectTitle.Add("Developper un site web");
            projectTitle.Add("Developper une application");
            projectTitle.Add("Developper un jeu vidéo");

            nameDev = new List<string>();
            nameDev.Add("Dorine");
            nameDev.Add("Lou");
            nameDev.Add("Oscar");
            nameDev.Add("Alexandre");
            nameDev.Add("Charlotte");
            nameDev.Add("Nicolas");
            nameDev.Add("Baptiste");
            nameDev.Add("Antoine");

            lastNameDev = new List<string>();
            lastNameDev.Add("Warren");
            lastNameDev.Add("Payne");
            lastNameDev.Add("Smith");
            lastNameDev.Add("Styles");
            lastNameDev.Add("Gomez");
            lastNameDev.Add("Gayat");
            lastNameDev.Add("Czipack");

            certifications = new List<Certification> {
            new Certification(
                    new Level(1, "Junior"), new Field("Junior")
                    ),
             new Certification(
                    new Level(2, "Senior"), new Field("Senior")
                    ),
             new Certification(
                    new Level(3, "Architect"), new Field("Architect")
                    )
              };

            random = new Random();
        }
        //Creer une game
        public Game CreateAGame(Game gameC)
        {
            try
            {
                GameS = new Game(10, gameC.TurnMax, gameC.StartBudget, gameC.Admin, "Creation");

                ServerLog.Log("Création d'une game");
            }
            catch (Exception ex)
            {
                ServerLog.Error(ex);
            }
            return GameS;
        }
        //Recuperer la game
        public Game GetGame()
        {
            ServerLog.Log("Get Game");
            return GameS;
        }
        //Start la game
        public Game StartGame()
        {
            try
            {
                GameS.Etat = "En Cours";
                AddAnTurnToAGame(GameS);
                ServerLog.Log("Start Game");
            }
            catch (Exception ex)
            {
                ServerLog.Error(ex);
            }
            return GameS;
        }
        //Creer une entreprise
        public Game AddAnBuisnessToAGame(Game game, object objectToAdd)
        {
            try
            {
                Company company = SerializeJson.Serialise.FromJson<Company>(objectToAdd.ToString());
                AddAPlayerToAGame(game, company.Player);
                if (!game.Companies.Contains(company))
                {
                    company.Budget = game.StartBudget;
                    game.Companies.Add(company);
                }
                GameS = game;
                ServerLog.Log($"Add {company.Name} company to the game");
            }
            catch (Exception ex)
            {
                ServerLog.Error(ex);
                throw;
            }
            return game;
        }
        //ajouter un player a la game
        public void AddAPlayerToAGame(Game game, Player player)
        {
            try
            {
                if (!game.Players.Contains(player))
                {
                    game.Players.Add(player);
                }
                ServerLog.Log($"Add {player.Pseudo} in the game");
            }
            catch (Exception ex)
            {
                ServerLog.Error(ex);
            }
            GameS = game;
        }
        //Creer un tour
        public void AddAnTurnToAGame(Game game)
        {
            try
            {
                Turn turn = new Turn();
                int randomN = random.Next(5);
                for (int i = 0; i < randomN; i++)
                {
                    CreateASchoolForATurn(turn);
                }
                for (int i = 0; i < randomN; i++)
                {
                    CreateADevForATurn(turn);
                }
                for (int i = 0; i < randomN; i++)
                {
                    CreateAProjectForATurn(turn);
                }
                for (int i = 0; i < randomN; i++)
                {
                    CreateATrainingSessionForAGame(turn);
                }
                game.Turns.Add(turn);
                ServerLog.Log($"Add turn n° {game.Turns.Count} in the game");
            }
            catch (Exception ex)
            {
                ServerLog.Error(ex);
            }
            GameS = game;
        }
        //Creer une ecole
        public void CreateASchoolForATurn(Turn turn)
        {
            try
            {
                School school = new School("CUCBD");
                turn.Schools.Add(school);
                ServerLog.Log("Add a school in a turn");

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //Creer une session training 
        public void CreateATrainingSessionForAGame(Turn turn)
        {
            try
            {

                TrainingSession trainingSession = new TrainingSession();
                Training training = new Training();

                trainingSession.Capacity = 1;
                trainingSession.Title = trainingSessionTitle[random.Next(trainingSessionTitle.Count - 1)];
                trainingSession.School = turn.Schools[random.Next(turn.Schools.Count - 1)];
                trainingSession.Price = random.Next(1000, 10000);
                trainingSession.Duration = trainingSession.Price < 5500 ? 1 : 2;
                trainingSession.Certifications.Add(certifications[random.Next(0, 2)]);

                training.TrainingSession = trainingSession;
                trainingSession.Training = training;
                GameS.Trainings.Add(training);
                ServerLog.Log($"Create training session {trainingSession.Title} in a game");

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //Creer un projet
        public void CreateAProjectForATurn(Turn turn)
        {
            try
            {
                Project project = new Project();
                project.Title = projectTitle[random.Next(projectTitle.Count - 1)];
                project.Cost = random.Next(1000, 10000);
                project.Duration = project.Cost < 5500 ? 1 : 2;
                int randomN = random.Next(4);
                for (int i = 0; i < randomN; i++)
                {
                    project.Requirements.Add(certifications[random.Next(certifications.Count - 1)]);
                }
                turn.NewProjects.Add(project);
                ServerLog.Log($"Add project {project.Title} in turn n° {GameS.Turns.Count}");

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //Creer un dev
        public void CreateADevForATurn(Turn turn)
        {
            try
            {
                Developer developer = new Developer(random.Next(1500, 10000), nameDev[random.Next(nameDev.Count - 1)], lastNameDev[random.Next(lastNameDev.Count - 1)]);
                if (developer.Salary <= 3000)
                {
                    developer.Certifications.Add(certifications[0]);
                }
                else if (developer.Salary <= 7500)
                {
                    developer.Certifications.Add(certifications[1]);
                }
                else
                {
                    developer.Certifications.Add(certifications[2]);
                }
                turn.NewDevelopers.Add(developer);
                ServerLog.Log($"Create dev {developer.FirstName} in the game");

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //ajouter un dev a une entreprise (staff member)
        public Game AddADevToABuisness(Game game, object objectToAdd, object objectWhereAdd)
        {
            try
            {
                Developer developer = (Developer)objectToAdd;
                Company company = (Company)objectWhereAdd;

                //Developer developer = SerializeJson.Serialise.FromJson<Developer>(objectToAdd.ToString());
                //Company company = SerializeJson.Serialise.FromJson<Company>(objectWhereAdd.ToString());
                StaffMember staffMember = new StaffMember(company, developer, game.Turns.Last(), null);
                developer.StaffMember = staffMember;
                //add staff to a company
                company.StaffMembers.Add(staffMember);
                company.Budget = company.Budget - developer.Salary;
                //delete dev to new dev
                game.Turns.Last().NewDevelopers.Remove(developer);

                GameS = game;
                ServerLog.Log($"Add dev {developer.FirstName} in buisness {company.Name}");
            }
            catch (Exception ex)
            {
                ServerLog.Error(ex);
            }
            return game;
        }
        //ajouter un projet a des devs (staff member)
        public Game AddAProjectToADev(Game game, object objectToAdd, object objectWhereAdd)
        {
            try
            {
                Project project = SerializeJson.Serialise.FromJson<Project>(objectToAdd.ToString());
                List<StaffMember> staffMembers = SerializeJson.Serialise.FromJson<List<StaffMember>>(objectWhereAdd.ToString());

                staffMembers.ForEach(staffM =>
                    staffM.Developer.Projects.Add(project)
                    );

                GameS = game;
                ServerLog.Log($"Add the project {project.Title} in {staffMembers.Count} dev");
            }
            catch (Exception ex)
            {
                ServerLog.Error(ex);
            }
            return game;
        }
        //envoyer un staff en formation
        public Game AddADevInATrainingSession(Game game, object objectToAdd, object objectWhereAdd)
        {
            try
            {
                StaffMember staff = objectToAdd as StaffMember;
                Training training = objectWhereAdd as Training;
                //add staff in training
                training.StaffMember = staff;
                ServerLog.Log($"Add dev {staff.Developer.FirstName} in the training {training.TrainingSession.Title}");
                game.Trainings.Remove(training);
                game.Turns.Last().StartingTrainingSessions.Add(training.TrainingSession);
            }
            catch (Exception ex)
            {
                ServerLog.Error(ex);
            }

            return game;
        }
        //attribuer une certif a un dev (staff member)
        public void AddACertifToADev(Certification certification, StaffMember staffMember)
        {
            try
            {
                if (!staffMember.Developer.Certifications.Contains(certification)) { staffMember.Developer.Certifications.Add(certification); } else { new Exception("Dev have already this certif"); }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //terminer un tour 
        public Game FinishATurnInAGame()
        {
            Game game = GameS;
            try
            {
                //Il reste des tours a effectuer avant de terminer la game
                if (game.Turns.Count != game.TurnMax)
                {
                    //il faux verfier si un/des projets sont terminer
                    game.Companies.ForEach(
                        company =>
                        company.StaffMembers.ForEach(
                            staffMember =>
                            staffMember.Developer.Projects.Where(
                                project =>
                                project.Duration != 0)
                            .ToList()
                            .ForEach((projet)
                            =>
                            {
                                projet.Duration--;

                                if (projet.Duration == 0)
                                {
                                    projet.IsSuccessFul = (random.Next(100) % 2 == 0);
                                }
                            }
                         )
                        )
                    );
                    //il faux verfier si un/des trainings sont terminer
                    game.Trainings.Where(training =>
                    training.TrainingSession.Duration != 0 &&
                    training.StaffMember != null
                    )
                        .ToList()
                        .ForEach(training =>
                        {
                            TrainingSession trainingSession = training.TrainingSession;
                            trainingSession.Duration--;
                            if (trainingSession.Duration == 0)
                            {
                                training.EndTurn = game.Turns.Last();
                                training.TrainingSession.Certifications.ForEach(
                                    certifications =>
                                    {
                                        //Ajouter toutes certifications du training au dev
                                        AddACertifToADev(certifications, training.StaffMember);
                                    }
                                );
                            }
                        }
                    )
                    ;
                    ServerLog.Log("Turn is fininsh");
                    AddAnTurnToAGame(game);
                    GameS = game;
                }
                //Il n'a plus de tour a effectuer la game se termine
                else
                {
                    FinishAGame(game);
                    ServerLog.Log("Start finish the game");
                }
            }
            catch (Exception ex)
            {
                ServerLog.Error(ex);
            }
            return game;
        }
        //terminer la partie
        public Game FinishAGame(Game game)
        {
            try
            {
                List<Company> companiesWinner = game.Companies.Where(
                        comp => comp.Budget.ToString() == game.Companies.Max(compagny => compagny.Budget).ToString()
                        ).ToList();

                //si il y'a plusieurs potentiel winner il faut choisir un winner selon ()
                game.Winner = new Player();
                game.Etat = "Finish";
                GameS = null;
                ServerLog.Log("Game is finish");
            }
            catch (Exception ex)
            {
                ServerLog.Error(ex);
            }
            return game;
        }
    }
}
