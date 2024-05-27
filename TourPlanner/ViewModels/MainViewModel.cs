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

        public void LoadTours()
        {
            TourList = [.. _blHandler.LoadToursDb()];
        }

        public void DeleteTour()
        {
            if (SelectedTour != null)
            {
                //delete from db
                TourList.Remove(SelectedTour);
                SelectedTour = null;
            }
        }
        public void DeleteLog()
        {
            if (SelectedTour != null && SelectedLog != null)
            {
                //delete from db
                SelectedTour.LogList.Remove(SelectedLog);
                SelectedLog = null;
            }
        }

        public void DisplayMainView()
        {
            ViewModel = IoCContainerConfig.Instance.MainViewModel;
            LoadTours();
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
