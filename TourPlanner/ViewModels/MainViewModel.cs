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
            DeleteTourCommand = new RelayCommand(o => DeleteTour());
            DeleteLogCommand = new RelayCommand(o => DeleteLog());
            tourImage = "/Resources/noImageImage.jpg";
            LoadTours();
        }

        private Tour selectedTour;
        private TourLog selectedLog;

        private string tourImage;
        public Page DisplayPage { get; set; }
        public ObservableCollection<TourLog> LogList { get; private set; }
        public ObservableCollection<Tour> TourList { get; private set; }

        public ICommand DeleteTourCommand { get; set; }
        public ICommand DeleteLogCommand { get; set; }

        public string TourImage
        {
            get
            {
                return tourImage;
            }

            set
            {
                tourImage = value;
                OnPropertyChanged();

            }
        }
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
                LoadTourInformation();
            }
        }
        public TourLog SelectedLog
        {
            get
            {
                return selectedLog;
            }

            set
            {
                selectedLog = value;
                OnPropertyChanged();
            }
        }

        public void AddTour(Tour tour)
        {
            TourList.Add(tour);
        }

        public void AddTourLog(TourLog tourLog)
        {
            SelectedTour.LogList.Add(tourLog);
            LoadTourInformation();
        }

        public void LoadTours()
        {
            TourList = new ObservableCollection<Tour>();
            TourList.Add(new Tour("Test1", new List<TourLog> { new(DateTime.Today, TimeSpan.FromHours(2.5), 215.7f, "no comment", 5, 3) }, "/Resources/exampleImage.png"));
            TourList.Add(new Tour("Test2", new List<TourLog> { new(DateTime.Today.AddDays(-1), TimeSpan.FromHours(1.7), 135.9f, "no comment", 5, 3) }, "/Resources/exampleImage.png"));
            LogList = new();
        }

        public void LoadTourInformation()
        {
            var tour = selectedTour;
            LogList.Clear();
            if (tour == null)
            {
                TourImage = "/Resources/noImageImage.jpg";
                return;
            }
            TourImage = tour.Image;
            if (tour.LogList == null) return;
            foreach (var log in tour.LogList)
            {
                LogList.Add(log);
            }
        }

        public void DeleteTour()
        {
            if (SelectedTour != null)
            {
                TourList.Remove(SelectedTour);
                SelectedTour = null;
            }
        }
        public void DeleteLog()
        {
            if (SelectedTour != null)
            {
                selectedTour.LogList.Remove(SelectedLog);
                LoadTourInformation();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

    }
}
