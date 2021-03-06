using Diiage2020.CsharpGame.Client.Services;
using Diiage2020.CsharpGame.Entities;
using Diiage2020.CsharpGames.Client.UWP.Model;
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
    public sealed partial class FormationDevView : Page
    {
        private List<StaffMember> staffMemberCompagnie;
        Game Game;
        Training Training;
        public ObservableCollection<DeveloperVM> Developpeurs { get; set; }
        public FormationDevView()
        {
            this.InitializeComponent();
            Developpeurs = new ObservableCollection<DeveloperVM>();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            GameTraining gameTraining = e.Parameter as GameTraining;

            Game = gameTraining.Game;
            Training = gameTraining.Training;

            staffMemberCompagnie = Game.Companies.First(c => c.Player.Equals(App.Player)).StaffMembers;

            staffMemberCompagnie.ForEach(sm => Developpeurs.Add(
                new DeveloperVM
                {
                    Salary = sm.Developer.Salary,
                    FirstName = sm.Developer.FirstName,
                    LastName = sm.Developer.LastName,
                    LstCertifDev = sm.Developer.Certifications.Select(c => c.Level.Description).ToList(),
                    Developer = sm.Developer
                })
            );
        }

        private void btnDevM_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(AcheterDeveloppeurView), Game);
        }

        private void BtnProjetM_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(AcheterProjetView), Game);
        }

        private void BtnFormationsM_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(BuyFormationView), Game);
        }

        private void btnRetourSelectDevForma_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(BuyFormationView), Game);
        }

        private void btnChoixDev_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(BoardGameView), Game);
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            var developer = ((sender as Button).DataContext as DeveloperVM).Developer;
            int maxCertif = 1;
            foreach (var certif in developer.Certifications)
            {
                if (certif.Level.Niveau > maxCertif)
                {
                    maxCertif = certif.Level.Niveau; 
                }
            }

            if (Game.Turns.SelectMany(t=> t.StartingTrainingSessions).Any(trainingSession => trainingSession.Training.StaffMember.Developer.Equals(developer) && trainingSession.Duration!=0))
            {
                ContentDialog dialog = new ContentDialog
                {
                    Title = " Erreurs",
                    Content = "Ce dev est déja dans une formation non terminer",
                    CloseButtonText = "OK"
                };
                await dialog.ShowAsync();
            }
            else if (Training.TrainingSession.Certifications.First().Level.Niveau <= maxCertif)
            {
                ContentDialog dialog = new ContentDialog
                {
                    Title = " Erreurs",
                    Content = "Ce dev possède deja cette certifications",
                    CloseButtonText = "OK"
                };
                await dialog.ShowAsync();
            }
            else
            {
                AsynchronousClient.SendToServer(Game, "AddADevInATrainingSession", developer.StaffMember, Training);
                AsynchronousClient.DataReceived += AsynchronousClient_DataReceived;
            }
        }

        private async void AsynchronousClient_DataReceived(object sender, EventArgs e)
        {
            Game = sender as Game;
            await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                this.Frame.Navigate(typeof(BoardGameView), Game);
            });
        }
    }
}
