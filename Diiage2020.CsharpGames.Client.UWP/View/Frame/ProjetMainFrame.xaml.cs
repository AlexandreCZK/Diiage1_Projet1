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
    public sealed partial class ProjetMainFrame : Page
    {
        public Company Company;
        public ObservableCollection<ProjectVM> ProjetGB;

        public ProjetMainFrame()
        {
            this.InitializeComponent();
            ProjetGB = new ObservableCollection<ProjectVM>();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Company = (Company)e.Parameter;
            Company.StaffMembers.ForEach(s => s.Developer.Projects.ForEach(p => ProjetGB.Add(new ProjectVM
            {
                Cout = p.Cost,
                NomProjet = p.Title,
                Duree = p.Duration,
                LesDevPrenom = p.Members.Select(m => m.Developer.FirstName).ToList()
            }
            )));
        }

        private void lstProjetsGB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btnPopUpProjetGB_Click(object sender, RoutedEventArgs e)
        {
           
        }
    }
}
