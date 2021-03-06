using Diiage2020.CsharpGame.Entities;
using Diiage2020.CsharpGames.Client.UWP.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.ServiceModel.Channels;
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
    public sealed partial class DevMainFrame : Page
    {
        public List<StaffMember> StaffMembers;

        public ObservableCollection<DeveloperVM> DeveloppeurGameBoard;
        public DevMainFrame()
        {
            this.InitializeComponent();
            DeveloppeurGameBoard = new ObservableCollection<DeveloperVM>();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            StaffMembers = (List<StaffMember>)e.Parameter;

            StaffMembers.ForEach(s => DeveloppeurGameBoard.Add(
                new DeveloperVM
                {
                    FirstName = s.Developer.FirstName,
                    LastName = s.Developer.LastName,
                    LstCertifDev = s.Developer.Certifications.Select(c => c.Level.Description).ToList()
                }
            ));
        }
    }
}
