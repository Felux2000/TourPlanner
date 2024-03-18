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
using TourPlanner.Models;

namespace TourPlanner.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public MainViewModel()
        {
            LoadTourLogs = new RelayCommand(o => LoadLogs());
            DeleteTourCommand = new RelayCommand(o => DeleteTour());
            LoadTours();
        }

        private Tour selectedTour;
        public Page DisplayPage { get; set; }
        public ObservableCollection<TourLog> LogList { get; set; }
        public ObservableCollection<Tour> TourList { get; set; }

        public ICommand LoadTourLogs { get; set; }
        public ICommand DeleteTourCommand { get; set; }

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
                LoadLogs();
            }
        }

        public void LoadTours()
        {
            TourList = new ObservableCollection<Tour>();
            TourList.Add(new Tour("Test1", new List<TourLog> { new(DateTime.Today, 2.5f, 215.7f) }));
            TourList.Add(new Tour("Test2", new List<TourLog> { new(DateTime.Today.AddDays(-1), 1.7f, 135.9f) }));
            LogList = new();
        }

        public void LoadLogs()
        {
            var tour = selectedTour;
            LogList.Clear();
            if (tour == null || tour.LogList == null) return;
            foreach (var log in tour.LogList)
            {
                LogList.Add(log);
            }
        }

        public void DeleteTour()
        {
            if(SelectedTour != null)
            {
                TourList.Remove(SelectedTour);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

    }
}
