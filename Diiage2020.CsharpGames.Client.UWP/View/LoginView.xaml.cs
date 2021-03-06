using Diiage2020.CsharpGame.Client.Services;
using Diiage2020.CsharpGame.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Diiage2020.CsharpGames.Client.UWP.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LoginView : Page
    {
        public LoginView()
        {
            this.InitializeComponent();
        }

        private async void btnValider_Click(object sender, RoutedEventArgs e)
        {
            if (txtSaisieN.Text != String.Empty && txtSaisieP.Text != String.Empty && txtSaisiePseu.Text != String.Empty)
            {
                //Creer le player 
                Player player = new Player(txtSaisieP.Text, txtSaisieN.Text, txtSaisiePseu.Text, null);
                App.Player = player;

                //Si admin -> creer la game
                if ((bool)cbAdmin.IsChecked)
                {
                    this.Frame.Navigate(typeof(CreateGameView));
                }
                //Sinon creer la compagny 
                else
                {
                    //recuperer la game creer sur le serveur
                    AsynchronousClient.SendToServer("GetGame");
                    AsynchronousClient.DataReceived += AsynchronousClient_DataReceived;
                }
            }
            else
            {
                ContentDialog oubliDialog = new ContentDialog
                {
                    Title = "Saisie incorrecte",
                    Content = "Veuillez saisir tous les champs présents sur cette page.",
                    CloseButtonText = "Ok"
                };
                await oubliDialog.ShowAsync();
            }
        }

        private async void AsynchronousClient_DataReceived(object sender, EventArgs e)
        {
            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                this.Frame.Navigate(typeof(CreateCompanyView), (Game)sender);
            });
        }
    }
}
