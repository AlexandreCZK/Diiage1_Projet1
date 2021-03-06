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

namespace Diiage2020.CsharpGames.Client.UWP.View.Frame
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class FormationMainFram : Page
    {
        public Game Game { get; set; }
        public ObservableCollection<TrainingVM> TrainingGameBoard;
        public FormationMainFram()
        {
            this.InitializeComponent();
            TrainingGameBoard = new ObservableCollection<TrainingVM>();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Game = (Game)e.Parameter;
            var trainingSessionsStart = Game.Turns.SelectMany(t => t.StartingTrainingSessions.Where(st => st.Training.StaffMember != null));
            trainingSessionsStart.Where(t => t.Training.StaffMember.Company.Player.Equals(App.Player)).ToList()
                    .ForEach(t => TrainingGameBoard.Add(
                    new TrainingVM
                    {
                        TSDur = t.Duration,
                        LstCertif = t.Certifications.Select(c => c.Level.Description).ToList(),
                        School = t.School.Name
                    }
                 ));
            //Game.Trainings.Where(t => t.StaffMember.Company.Player.Equals(App.Player)).ToList()
            //        .ForEach(t => TrainingGameBoard.Add(
            //        new TrainingVM
            //        {
            //            TSDur = t.TrainingSession.Duration,
            //            LstCertif = t.TrainingSession.Certifications.Select(c => c.Level.Description).ToList(),
            //            School = t.TrainingSession.School.Name
            //        }
            //     ));

        }
    }
}
