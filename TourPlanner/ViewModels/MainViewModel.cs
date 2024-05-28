using Microsoft.VisualBasic;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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
            SelectedTour = null;
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
                LoadWebView();
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
                if (!_blHandler.DeleteTourDb(SelectedTour))
                {
                    MessageBox.Show("Unable to delete tour, try again.", "Delete error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    TourList.Remove(SelectedTour);
                    SelectedTour = null;
                }
            }
        }
        public void DeleteLog()
        {
            if (SelectedTour != null && SelectedLog != null)
            {
                if (!_blHandler.DeleteTourLogDb(SelectedLog))
                {
                    MessageBox.Show("Unable to delete log, try again.", "Delete error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    SelectedTour.LogList.Remove(SelectedLog);
                    SelectedLog = null;
                }
            }
        }

        private void LoadWebView()
        {
            if (SelectedTour != null)
            {
                if (SelectedTour.MapJson == null || SelectedTour.MapJson == string.Empty)
                    return;
                WriteJs();
                WebViewRefreshEventHandler.Invoke(this, null);
            }
        }

        private void WriteJs()
        {
            string jsPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources/WebView/directions.js");
            using (StreamWriter sw = new StreamWriter(jsPath, false))
            {
                sw.Write($"var directions = ");
            }
            using (StreamWriter sw = new StreamWriter(jsPath, true))
            {
                sw.Write(SelectedTour.MapJson);
                sw.Write(";");
            }
        }

        public void DisplayMainView()
        {
            ViewModel = IoCContainerConfig.Instance.MainViewModel;
            LoadTours();
            SelectedTour = null;
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
