
using Diiage2020.CsharpGame.Client.Services;
using Diiage2020.CsharpGame.Entities;
using Diiage2020.CsharpGames.Client.UWP.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

// Pour plus d'informations sur le modèle d'élément Page vierge, consultez la page https://go.microsoft.com/fwlink/?LinkId=234238

namespace Diiage2020.CsharpGames.Client.UWP.View
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class AcheterDeveloppeurView : Page
    {
        private List<Developer> newDevelopers;
        public Game Game { get; set; }
        public ObservableCollection<DeveloperVM> Developpeurs { get; set; }
        public AcheterDeveloppeurView()
        {
            this.InitializeComponent();
            Developpeurs = new ObservableCollection<DeveloperVM>();
            newDevelopers = new List<Developer>();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Game = (Game)e.Parameter;

            newDevelopers = Game.Turns.Last().NewDevelopers;

            newDevelopers.ForEach(ND => Developpeurs.Add(
                new DeveloperVM
                {
                    Salary = ND.Salary,
                    FirstName = ND.FirstName,
                    LastName = ND.LastName,
                    LstCertifDev = ND.Certifications.Select(c => c.Level.Description).ToList(),
                    Developer = ND
                })
            );
        }
        private async void btnValiderAchatDev_Click(object sender, RoutedEventArgs e)
        {
            var developer = (lstAchatDev.SelectedItem as DeveloperVM).Developer;
            var company = Game.Companies.First(c => c.Player.Equals(App.Player));
            if (developer != null)
            {
                if (Game.Companies.First(c => c.Player.Equals(App.Player)).Budget >= developer.Salary)
                {
                    AsynchronousClient.SendToServer(Game, "AddADevToABuisness", developer, company);
                    AsynchronousClient.DataReceived += AsynchronousClient_DataReceived;
                }
                else
                {

                    ContentDialog dialog = new ContentDialog
                    {
                        Title = " Fond insufisant",
                        Content = "Vous n'avez pas les fonds nécessaires pour acheter ce dev",
                        CloseButtonText = "OK"
                    };
                    await dialog.ShowAsync();
                }
            }
            else
            {
                ContentDialog dialog = new ContentDialog
                {
                    Title = " Erreur",
                    Content = "Selectionner un ou plusieurs developpeur",
                    CloseButtonText = "OK"
                };
                await dialog.ShowAsync();
            }
        }

        private async void AsynchronousClient_DataReceived(object sender, EventArgs e)
        {
            Game = (Game)sender;
            await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                this.Frame.Navigate(typeof(BoardGameView), Game);
            });
        }

        private void btnRetourAchatDev_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(BoardGameView), Game);
        }

        private void btnProjetM_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(AcheterProjetView), Game);
        }

        private void btnFormationsM_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(BuyFormationView), Game);
        }

        private void btnDevM_Click_1(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(AcheterDeveloppeurView), Game);

        }
    }
}
