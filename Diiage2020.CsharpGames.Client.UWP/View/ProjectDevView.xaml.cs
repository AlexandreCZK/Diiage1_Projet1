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
    public sealed partial class ProjectDevView : Page
    {
        private List<StaffMember> staffMemberCompagnie;
        Game Game;
        public ObservableCollection<DeveloperVM> Developpeurs { get; set; }
        public ProjectDevView()
        {
            this.InitializeComponent();
            Developpeurs = new ObservableCollection<DeveloperVM>();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Game = (Game)e.Parameter;
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

        private void btnValiderSelectDevProjet_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(BoardGameView), Game);
        }

        private void btnRetourSelectDevProjet_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(AcheterProjetView), Game);
        }
    }
}
