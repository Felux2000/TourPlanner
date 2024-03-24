﻿using System;
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
            tourImage = "/Resources/noImageImage.jpg";
            LoadTours();
        }

        private Tour selectedTour;

        private string tourImage;
        public Page DisplayPage { get; set; }
        public ObservableCollection<TourLog> LogList { get; set; }
        public ObservableCollection<Tour> TourList { get; set; }

        public ICommand DeleteTourCommand { get; set; }

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

        public void LoadTours()
        {
            TourList = new ObservableCollection<Tour>();
            TourList.Add(new Tour("Test1", new List<TourLog> { new(DateTime.Today, 2.5f, 215.7f) }, "/Resources/exampleImage.png"));
            TourList.Add(new Tour("Test2", new List<TourLog> { new(DateTime.Today.AddDays(-1), 1.7f, 135.9f) }, "/Resources/exampleImage.png"));
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

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

    }
}