using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using Xamarin.Forms;

namespace TourPlanner
{
    public class ViewModel : BindableObject
    {

        public ViewModel()
        {
            LoadTourLogs = new Command(Logs);
        }

        public ObservableCollection<Log> LogList { get; set; }
        public ObservableCollection<Tour> TourList { get; set; }

        public ICommand LoadTourLogs { get; }

        public void Tours()
        {
            TourList = new ObservableCollection<Tour>();
            TourList.Add(new Tour("Test1", new ObservableCollection<Log> { new(DateTime.Today, 2.5f, 215.7f) }));
            TourList.Add(new Tour("Test2", new ObservableCollection<Log> { new(DateTime.Today.AddDays(-1), 1.7f, 135.9f) }));
            LogList = new ObservableCollection<Log>();
        }

        public void Logs(object sender)
        {
            var tour = sender as Tour;
            if (tour == null || tour.LogList == null) return;
            LogList.Clear();
            foreach (var log in tour.LogList)
            {
                LogList.Add(log);
            }
            //LogList.Add(new Log(DateTime.Now, 2.5f, 215.7f));
        }

    }
}
