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
using TourPlanner.BusinessLogic;
using TourPlanner.Commands;
using TourPlanner.Models;

namespace TourPlanner.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private BLHandler _blHandler;
        public MainViewModel(BLHandler blHandler)
        {
            _blHandler = blHandler;
            DeleteTourCommand = new RelayCommand(o => DeleteTour());
            DeleteLogCommand = new RelayCommand(o => DeleteLog());
            LoadTours();
            viewModel = this;
        }

        private Tour selectedTour;
        private TourLog selectedLog;
        private BaseViewModel viewModel;

        public Page DisplayPage { get; set; }
        public ObservableCollection<Tour> TourList { get; private set; }

        public ICommand DeleteTourCommand { get; set; }
        public ICommand DeleteLogCommand { get; set; }

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

        public BaseViewModel ViewModel
        {
            get
            {
                return viewModel;
            }
            set
            {
                viewModel = value;
                OnPropertyChanged();
            }
        }

        public void AddTour(Tour tour)
        {
            TourList.Add(tour);
            SelectedTour = tour;
        }

        public void EditTour(Tour editedTour)
        {
            editedTour.LogList = SelectedTour.LogList;
            TourList[TourList.IndexOf(SelectedTour)] = editedTour;
            SelectedTour = editedTour;
        }

        public void AddTourLog(TourLog tourLog)
        {
            SelectedTour.LogList.Add(tourLog);
            SelectedLog = tourLog;
        }

        public void EditTourLog(TourLog editedTourLog)
        {
            SelectedTour.LogList[SelectedTour.LogList.IndexOf(SelectedLog)] = editedTourLog;
            SelectedLog = editedTourLog;
        }

        public void LoadTours()
        {
            TourList = new ObservableCollection<Tour>();
            TourList.Add(new Tour("Longus Tourus", new List<TourLog> { new(DateTime.Today, TimeSpan.FromHours(2.5), 215.7f, "Long and dangerous", 8, 3) }, "/Resources/exampleImage.png"));
            TourList.Add(new Tour("Carus wroomus", new List<TourLog> { new(DateTime.Today.AddMonths(-1), TimeSpan.FromHours(1.7), 135.9f, "Car go wroom", 4, 6) }, "/Resources/exampleImage.png"));
            TourList.Add(new Tour("Car", "Good for cars to loose the zoomies", "Top of car tree", "food bowl", "car", 2, 0.1f, "/Resources/exampleImage.png"));
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
            if (SelectedTour != null && SelectedLog != null)
            {
                SelectedTour.LogList.Remove(SelectedLog);
                SelectedLog = null;
            }
        }

        public void DisplayMainView()
        {
            ViewModel = IoCContainerConfig.Instance.MainViewModel;

        }

        public ICommand DisplayAddTourView
        {
            get
            {
                return new RelayCommand(action =>
                {
                    var vm = IoCContainerConfig.Instance.AddTourViewModel;
                    ViewModel = vm;
                    vm.OnRequestClose += (s, e) => DisplayMainView();
                });
            }
        }

        public ICommand DisplayAddTourLogView
        {
            get
            {
                return new RelayCommand(action =>
                {
                    var vm = IoCContainerConfig.Instance.AddTourLogViewModel;
                    ViewModel = vm;
                    vm.OnRequestClose += (s, e) => DisplayMainView();
                });
            }
        }

        public ICommand DisplayEditTourView
        {
            get
            {
                return new RelayCommand(action =>
                {
                    var vm = IoCContainerConfig.Instance.EditTourViewModel;
                    ViewModel = vm;
                    vm.OnRequestClose += (s, e) => DisplayMainView();
                });
            }
        }

        public ICommand DisplayEditTourLogView
        {
            get
            {
                return new RelayCommand(action =>
                {
                    var vm = IoCContainerConfig.Instance.EditTourLogViewModel;
                    ViewModel = vm;
                    vm.OnRequestClose += (s, e) => DisplayMainView();
                });
            }
        }
    }
}
