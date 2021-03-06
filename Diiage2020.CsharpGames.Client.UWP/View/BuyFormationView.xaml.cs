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
    public sealed partial class BuyFormationView : Page
    {
        public Game Game { get; set; }
        public ObservableCollection<TrainingVM> AllTrainning;
        List<TrainingSession> NewTrainings;
        public BuyFormationView()
        {
            this.InitializeComponent();

            AllTrainning = new ObservableCollection<TrainingVM>();
            
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Game = (Game)e.Parameter;
            NewTrainings = Game.Trainings.Select(t => t.TrainingSession).ToList();

            NewTrainings.ForEach(sTS => AllTrainning.Add(new TrainingVM
            {
                TSDur = sTS.Duration,
                TSPrice = sTS.Price,
                TSTitle = sTS.Title,
                LstCertif = sTS.Certifications.Select(c => c.Field.Title).ToList(),
                Training = sTS.Training
            }));
        }
        private void btnRetourAchatForma_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(BoardGameView), Game);
        }

        private async void btnValiderAchatForma_Click(object sender, RoutedEventArgs e)
        {
            Training training = (lstAchatFormation.SelectedItem as TrainingVM).Training;
            if (Game.Companies.First(company => company.Player.Equals(App.Player)).Budget >= training.TrainingSession.Price)
            {

                this.Frame.Navigate(typeof(FormationDevView), new GameTraining { Game=Game, Training=training });
            }
            else
            {
                ContentDialog dialog = new ContentDialog
                {
                    Title = " Fond insufisant",
                    Content = "Vous n'avez pas les fond nécesaire pour acheter ce projet",
                    CloseButtonText = "OK"
                };
                await dialog.ShowAsync();
            }
        }


        private void btnDevM_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(AcheterDeveloppeurView), Game);
        }

        private void btnProjetM_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(AcheterProjetView), Game);
        }

        private void btnFormationsM_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(BuyFormationView), Game);
        }
    }
}




