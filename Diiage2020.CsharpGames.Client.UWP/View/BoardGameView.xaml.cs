using Diiage2020.CsharpGame.Entities;
using Diiage2020.CsharpGame.Client.Services;
using Diiage2020.CsharpGames.Client.UWP.View.Frame;
using Diiage2020.CsharpGames.Client.UWP.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using System.Timers;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
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
    public sealed partial class BoardGameView : Page
    {
        public Game Game { get; set; }
        public Company Company { get; set; }

        public CompanyVM CompanyN { get; set; }

        public ObservableCollection<CompanyVM> CompanyGameBoard;

        
        
        public BoardGameView()
        {
            this.InitializeComponent();
            CompanyGameBoard = new ObservableCollection<CompanyVM>();
            App.turnOver += App_turnOver;
        }

        private void App_turnOver(object sender, EventArgs e)
        {
            AsynchronousClient.SendToServer(Game, "FinishATurnInAGame");
            AsynchronousClient.DataReceived += AsynchronousClient_DataReceived;
        }

        private void AsynchronousClient_DataReceived(object sender, EventArgs e)
        {
            Game = (Game)sender;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Game = (Game)e.Parameter;
            Game.Companies.ForEach(c => CompanyGameBoard.Add(new CompanyVM
            {
                BudgetComp = c.Budget,
                NameCompany = c.Name,
                StaffMembers = c.StaffMembers
            }));

            MyFrame.Navigate(typeof(DevMainFrame), 
                Game.Companies.First(c => c.Player.Equals(App.Player)).StaffMembers);
        }

        private void btnHam_Click(object sender, RoutedEventArgs e)
        {
            SplitViewGB.IsPaneOpen = !SplitViewGB.IsPaneOpen;
        }

        private void IconsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstBIDev.IsSelected) 
            { MyFrame.Navigate(typeof(DevMainFrame), 
                Game.Companies.First(c => c.Player.Equals(App.Player)).StaffMembers);
            }
            else if (lstBIProj.IsSelected) 
            { MyFrame.Navigate(typeof(ProjetMainFrame), 
                Game.Companies.First(c => c.Player.Equals(App.Player))); 
            }
            else if (lstBIForma.IsSelected) 
            { 
                MyFrame.Navigate(typeof(FormationMainFram), Game);
            }
        }
        private void Image_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Image objectSender = (Image)sender;
            this.Frame.Navigate(typeof(CompanyInformationsView), objectSender.DataContext);
        }

        private void btnDevM_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(AcheterDeveloppeurView),
                Game
                );
        }

        private void btnProjetM_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(AcheterProjetView),
                Game
                );
        }

        private void btnFormaM_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(BuyFormationView),
                Game
                );
        }


    }
}
