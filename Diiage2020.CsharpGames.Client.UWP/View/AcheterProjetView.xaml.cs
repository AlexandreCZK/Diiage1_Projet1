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
    public sealed partial class AcheterProjetView : Page
    {
        public Game Game { get; set; }
        public ObservableCollection<ProjectVM> Projets;
        List<Project> NewProjects;

        public AcheterProjetView()
        {
            this.InitializeComponent();
            Projets = new ObservableCollection<ProjectVM>();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Game = (Game)e.Parameter;
            NewProjects = Game.Turns.Last().NewProjects;

            NewProjects.ForEach(NP => Projets.Add(
                new ProjectVM
                {
                    Cout = NP.Cost,
                    Duree = NP.Duration,
                    NomProjet = NP.Title,
                    LvlDescriptionCertif = NP.Requirements.Select(r => r.Level.Description).ToList()
                }
            ));
        }
        private void BtnDevM_Click(object sender, RoutedEventArgs e)
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

        private void BtnValiderAchatProjet_Click(object sender, RoutedEventArgs e)
        {
            
            this.Frame.Navigate(typeof(ProjectDevView));
        }

        private void BtnRetourAchatProjet_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(BoardGameView), Game);
        }
    }
    }

        


        

        
