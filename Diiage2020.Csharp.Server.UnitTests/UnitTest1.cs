using Diiage2020.CsharpGame.Client.Services;
using Diiage2020.CsharpGame.Entities;
using Diiage2020.CsharpGame.Server.Core;
using Diiage2020.CSharpGame.Server.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Diiage2020.Csharp.Server.UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        #region TestSendToServer
        [TestMethod]
        public void TestSocketSendData1()
        {
            Player admin = new Player();
            Game game = new Game(10, 10, 5000, admin);
            new Thread(AsynchronousSocketListener.StartListening).Start();
            new Thread(AsynchronousClient.StartClient).Start();

            Assert.IsTrue(game.Players.Count == 0);

            Thread.Sleep(10000);
            AsynchronousClient.SendToServer(game, "CreateAGame");
            AsynchronousClient.DataReceived += AsynchronousClient_DataReceived1;
        }
        private void AsynchronousClient_DataReceived1(object sender, System.EventArgs e)
        {
            Game game = (Game)sender;
            Assert.IsTrue(game.Players.Count == 1);
        }

        [TestMethod]
        public void TestSocketSendData2()
        {
            Player admin = new Player();
            Game game = new Game(10, 10, 5000, admin);
            game.Players.Add(admin);
            game.Etat = "Création";
            Player player = new Player();

            new Thread(AsynchronousSocketListener.StartListening).Start();
            new Thread(AsynchronousClient.StartClient).Start();

            Assert.IsTrue(game.Players.Count == 1);

            Thread.Sleep(10000);
            AsynchronousClient.SendToServer(game, "AddAPlayerToAGame", player);
            AsynchronousClient.DataReceived += AsynchronousClient_DataReceived2;
        }

        private void AsynchronousClient_DataReceived2(object sender, System.EventArgs e)
        {
            Game game = (Game)sender;
            Assert.IsTrue(game.Players.Count == 2);
        }

        [TestMethod]
        public void TestSocketSendData3()
        {
            Player player = new Player();
            Game game = new Game();
            game.Players.Add(player);
            Company company = new Company("CUCBD", 0, 5000, new CompanyType(), player);
            game.Companies.Add(company);
            Developer developer = new Developer(1, "Alex", "Czipack");

            Assert.IsTrue(game.Companies[0].StaffMembers.Count == 0);

            new Thread(AsynchronousSocketListener.StartListening).Start();
            new Thread(AsynchronousClient.StartClient).Start();

            Thread.Sleep(10000);
            AsynchronousClient.SendToServer(game, "AddADevToABuisness", developer, company);
            AsynchronousClient.DataReceived += AsynchronousClient_DataReceived3;
        }

        private void AsynchronousClient_DataReceived3(object sender, System.EventArgs e)
        {
            Game game = (Game)sender;
            Assert.IsTrue(game.Companies[0].StaffMembers.Count == 1);
        }
        #endregion


        #region TestServerMethod
        [TestMethod]
        public void TestCreateANewTurn()
        {
            Game game = new Game() ;
            Service service = new Service();
            game = service.CreateAGame(game);
            service.AddAnTurnToAGame(game);
            Console.ReadLine();
        }
        #endregion
    }
}
