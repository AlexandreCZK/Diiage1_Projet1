using Diiage2020.CsharpGame.Client.Services;
using Diiage2020.CsharpGame.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// Pour plus d'informations sur le modèle d'élément Page vierge, consultez la page https://go.microsoft.com/fwlink/?LinkId=234238

namespace Diiage2020.CsharpGames.Client.UWP.View
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class CreateCompanyView : Page
    {
        Game Game;
        Company Compagny;
        public CreateCompanyView()
        {
            this.InitializeComponent();
            Compagny = new Company();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Game = (Game)e.Parameter;
        }

        private void btnRetour_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(LoginView));
        }

        private async void btnContinuer_Click(object sender, RoutedEventArgs e)
        {
            Compagny.Name = txtNomCompagnie.Text;
            Compagny.Player = App.Player;

            AsynchronousClient.SendToServer(Game, "AddAnBuisnessToAGame", Compagny);
            AsynchronousClient.DataReceived += AsynchronousClient_DataReceived;
            if (!Game.Admin.Equals(App.Player))
            {
                AsynchronousClient.GameAsStart += AsynchronousClient_GameAsStart;
                ContentDialog dialog = new ContentDialog
                {
                    Title = " Waiting",
                    Content = "Nous attendons que l'admin lance la game.",
                };
                await dialog.ShowAsync();
            }
            else
            {
                ContentDialog demarrerGameDialog = new ContentDialog
                {
                    Title = " Démarrer la game",
                    Content = "Lancer la game dès que tout les joueurs sont prêt.",
                    CloseButtonText = "Lancer"
                };
                demarrerGameDialog.CloseButtonClick += OubliDialog_CloseButtonClick;
                await demarrerGameDialog.ShowAsync();
            }
        }

        private async void AsynchronousClient_GameAsStart(object sender, EventArgs e)
        {
            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                Frame.Navigate(typeof(BoardGameView), (Game)sender);
            });
        }

        private void OubliDialog_CloseButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            //Lancer la game
            AsynchronousClient.SendToServer("StartGame");
            AsynchronousClient.DataReceived += AsynchronousClient_DataReceived1;
        }
        private void AsynchronousClient_DataReceived(object sender, EventArgs e)
        {
            Game = (Game)sender;
        }
        private async void AsynchronousClient_DataReceived1(object sender, EventArgs e)
        {
            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                Frame.Navigate(typeof(BoardGameView), (Game)sender);
            });
        }
    }
}
