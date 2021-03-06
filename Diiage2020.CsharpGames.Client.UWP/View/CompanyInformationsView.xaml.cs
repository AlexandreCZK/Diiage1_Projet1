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
    public sealed partial class CompanyInformationsView : Page
    {
        /* Enlever pour voir les données
        private Company Company;
        private List<Project> Projets;
        private ObservableCollection<Project> Projetsé;
        private List<Certification> Certification;
        */
        CompanyVM CompanyN;

        public CompanyInformationsView()
        {
            this.InitializeComponent();

        }

        private void btnReturn_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(BoardGameView));
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            CompanyN = (CompanyVM)e.Parameter;
            CompanyVM CompanyNext = new CompanyVM();
            CompanyNext.NameCompany = CompanyN.NameCompany;
        }
    }
}
