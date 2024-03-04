using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using TourPlanner.Commands;

namespace TourPlanner
{
    public class ViewModel : INotifyPropertyChanged
    {

        public ViewModel()
        {
            LoadTourLogs = new RelayCommand(o => Logs());
            Tours();
        }

        private Tour selectedTour;
        public ObservableCollection<Log> LogList { get; set; }
        public ObservableCollection<Tour> TourList { get; set; }

        public ICommand LoadTourLogs { get; set; }

        public Tour SelectedTour
        {
            get
            {
                return selectedTour;
            }

            set
            {
                selectedTour = value;
                OnPropertyChanged();
                Logs();
            }
        }

        public void Tours()
        {
            TourList = new ObservableCollection<Tour>();
            TourList.Add(new Tour("Test1", new List<Log> { new(DateTime.Today, 2.5f, 215.7f) }));
            TourList.Add(new Tour("Test2", new List<Log> { new(DateTime.Today.AddDays(-1), 1.7f, 135.9f) }));
            LogList = new();
        }

        public void Logs()
        {
            var tour = selectedTour;
            if (tour == null || tour.LogList == null) return;
            LogList.Clear();
            foreach (var log in tour.LogList)
            {
                LogList.Add(log);
            }
            //LogList.Add(new Log(DateTime.Now, 2.5f, 215.7f));
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

    }
}
