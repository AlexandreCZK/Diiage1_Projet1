using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Diiage2020.CsharpGame.Entities;
using Diiage2020.CsharpGame.Client.Services;
using System.Threading.Tasks;
using Windows.UI.Core;

// Pour plus d'informations sur le modèle d'élément Page vierge, consultez la page https://go.microsoft.com/fwlink/?LinkId=234238

namespace Diiage2020.CsharpGames.Client.UWP.View
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class CreateGameView : Page
    {
        Game Game;
        public CreateGameView()
        {
            this.InitializeComponent();
            Game = new Game();
            Game.Admin = App.Player;
        }
        private void BtnCreateGame_Click(object sender, RoutedEventArgs e)
        {
            Game.TurnMax = int.Parse(txtTourMax.Text);
            Game.StartBudget = int.Parse(txtStartBudget.Text);

            AsynchronousClient.SendToServer(Game, "CreateAGame");
            AsynchronousClient.DataReceived += AsynchronousClient_DataReceived;
        }

        private async void AsynchronousClient_DataReceived(object sender, EventArgs e)
        {
            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                Game = (Game)sender;
                Frame.Navigate(typeof(CreateCompanyView), Game);
            });
        }
    }
}
