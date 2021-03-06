using Diiage2020.CsharpGame.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Diiage2020.CsharpGames.Client.UWP.ViewModel
{
    public class CompanyVM : INotifyPropertyChanged
    {
        private string _timer;
        private string nameCompany;
        private double budgetComp;
        private List<StaffMember> staffMembers;

        public event PropertyChangedEventHandler PropertyChanged;
        public string Timer { get { return String.Format("{0} : {1}", App.minutes, App.secondes); } set { _timer = value; } }
        public CompanyVM()
        {
            if (App.aTimer.Enabled != true)
            {
                App.aTimer.Enabled = true;
            }
            App.secondesPassed += App_secondesPassed;
        }
        public List<StaffMember> StaffMembers
        {
            get { return staffMembers; }
            set { staffMembers = value; }
        }


        public double BudgetComp
        {
            get { return budgetComp; }
            set { budgetComp = value; }
        }


        public string NameCompany
        {
            get { return nameCompany; }
            set { nameCompany = value; }
        }
        private async void App_secondesPassed(object sender, EventArgs e)
        {
            await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                NotifyPropertyChanged(nameof(Timer));
            });
        }



        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
