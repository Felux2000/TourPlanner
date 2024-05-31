using Microsoft.VisualBasic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Forms;
using System.Windows.Input;
using TourPlanner.BusinessLayer;
using TourPlanner.PresentationLayer.Commands;
using TourPlanner.HelperLayer.Models;

namespace TourPlanner.PresentationLayer.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private BLHandler _blHandler;
        public MainViewModel(BLHandler blHandler)
        {
            _blHandler = blHandler;
            LoadTours();
            viewModel = this;
            SelectedTour = null;
        }

        private Tour selectedTour;
        private TourLog selectedLog;
        private BaseViewModel viewModel;
        private string searchText;

        public Page DisplayPage { get; set; }
        private ObservableCollection<Tour> tourList;
        public List<Tour> UnfilteredTourList { get; private set; }
        public delegate Task<byte[]> ImageCaptureDelegate(object sender, EventArgs e);
        public event ImageCaptureDelegate CaptureTourImageEvent;


        public ObservableCollection<Tour> TourList
        {
            get
            {
                return tourList;
            }

            set
            {
                tourList = value;
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

        public string SearchText
        {
            get
            {
                return searchText;
            }

            set
            {
                searchText = value;
                OnPropertyChanged();
                FilterTours();
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

        private void LoadTours()
        {
            TourList = new();
            UnfilteredTourList = [.. _blHandler.LoadToursDb()];
            foreach (var tour in UnfilteredTourList)
            {
                TourList.Add(tour);
            }
        }

        private void DeleteTour()
        {
            if (SelectedTour != null)
            {
                if (!_blHandler.DeleteTourDb(SelectedTour))
                {
                    System.Windows.MessageBox.Show("Unable to delete tour, try again.", "Delete error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    TourList.Remove(SelectedTour);
                    SelectedTour = null;
                }
            }
        }
        private void DeleteLog()
        {
            if (SelectedTour != null && SelectedLog != null)
            {
                if (!_blHandler.DeleteTourLogDb(SelectedLog))
                {
                    System.Windows.MessageBox.Show("Unable to delete log, try again.", "Delete error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    SelectedTour.LogList.Remove(SelectedLog);
                    SelectedLog = null;
                }
            }
        }

        private string GetSavePath(FileType type)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.InitialDirectory = "c:\\";
                saveFileDialog.FileName = type == FileType.pdf ? "Report" : "TourExport";
                saveFileDialog.DefaultExt = type == FileType.pdf ? ".pdf" : ".json";
                saveFileDialog.Filter = type == FileType.pdf ? "Pdf documents (.pdf)|*.pdf" : "Json files (.json)|*.json";

                saveFileDialog.FilterIndex = 2;
                saveFileDialog.RestoreDirectory = true;

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    return saveFileDialog.FileName;
                }
                return string.Empty;
            }
        }

        private string GetOpenFilePath()
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.DefaultExt = ".json";
                openFileDialog.Filter = "Json files (.json)|*.json";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    return openFileDialog.FileName;
                }
                return string.Empty;
            }
        }

        private Task<byte[]> CaptureTourImage()
        {
            return CaptureTourImageEvent.Invoke(this, null);
        }

        private void GenerateTourReport()
        {
            if (SelectedTour != null)
            {
                //reload webview to original size
                SelectedTour = SelectedTour;
                string path = GetSavePath(FileType.pdf);
                if (path != string.Empty)
                    _blHandler.GenerateReport(SelectedTour, path, CaptureTourImage());
            }
        }

        private void GenerateSummarizeReport()
        {
            if (TourList.Count != 0)
            {
                string path = GetSavePath(FileType.pdf);
                if (path != string.Empty)
                    _blHandler.GenerateSummary(TourList.ToList(), path);
            }
        }

        private void ExportTour()
        {
            string savePath = GetSavePath(FileType.json);
            if (savePath != string.Empty)
            {
                _blHandler.ExportTour(selectedTour, savePath);
            }
        }

        private void ImportTour()
        {
            string importFilePath = GetOpenFilePath();
            if (importFilePath != string.Empty)
            {
                Tour importedTour = _blHandler.ImportTour(importFilePath);
                if (importedTour != null)
                {
                    TourList.Add(importedTour);
                }
            }
        }
        private void ExampleFileTour()
        {
            string savePath = GetSavePath(FileType.json);
            if (savePath != string.Empty)
            {
                _blHandler.ExportExampleTour(savePath);
            }
        }

        private void FilterTours()
        {
            SelectedTour = null;
            SelectedLog = null;
            if (SearchText == string.Empty || SearchText == null)
            {
                TourList.Clear();
                foreach (Tour tour in UnfilteredTourList)
                {
                    TourList.Add(tour);
                }
            }
            else
            {
                TourList.Clear();
                foreach (Tour tour in UnfilteredTourList)
                {
                    if (tour.Name != null && tour.Name.Contains(SearchText)) { TourList.Add(tour); continue; }
                    if (tour.Description != null && tour.Description.Contains(SearchText)) { TourList.Add(tour); continue; }
                    if (tour.From != null && tour.From.Contains(SearchText)) { TourList.Add(tour); continue; }
                    if (tour.To != null && tour.To.Contains(SearchText)) { TourList.Add(tour); continue; }
                    if (tour.TransportType != null && tour.TransportType.Contains(SearchText)) { TourList.Add(tour); continue; }
                    if (tour.Distance != null && tour.Distance.ToString().Contains(SearchText)) { TourList.Add(tour); continue; }
                    if (tour.Estimation != null && tour.Estimation.ToString().Contains(SearchText)) { TourList.Add(tour); continue; }
                    if (tour.Popularity != null && tour.Popularity.ToString().Contains(SearchText)) { TourList.Add(tour); continue; }
                    if (tour.ChildFriendliness != null && tour.ChildFriendliness.ToString().Contains(SearchText)) { TourList.Add(tour); continue; }
                    for (int a = 0; a < tour.LogList.Count; a++)
                    {
                        var log = tour.LogList[a];
                        if (log != null && log.Date.ToString().Contains(SearchText)) { TourList.Add(tour); break; }
                        if (log != null && log.Duration.ToString().Contains(SearchText)) { TourList.Add(tour); break; }
                        if (log != null && log.Distance.ToString().Contains(SearchText)) { TourList.Add(tour); break; }
                        if (log != null && log.Comment.Contains(SearchText)) { TourList.Add(tour); break; }
                        if (log != null && log.Difficulty.ToString().Contains(SearchText)) { TourList.Add(tour); break; }
                        if (log != null && log.Rating.ToString().Contains(SearchText)) { TourList.Add(tour); break; }
                    }
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

        private void DisplayMainView()
        {
            ViewModel = IoCContainerConfig.Instance.MainViewModel;
            LoadTours();
            SelectedTour = null;
            SelectedLog = null;
        }

        public ICommand ExampleFileTourCommand
        {
            get
            {
                return new RelayCommand(action =>
                {
                    ExampleFileTour();
                });
            }
        }
        public ICommand ExportTourCommand
        {
            get
            {
                return new RelayCommand(action =>
                {
                    ExportTour();
                });
            }
        }
        public ICommand ImportTourCommand
        {
            get
            {
                return new RelayCommand(action =>
                {
                    ImportTour();
                });
            }
        }
        public ICommand GenerateSummarizeReportCommand
        {
            get
            {
                return new RelayCommand(action =>
                {
                    GenerateSummarizeReport();
                });
            }
        }
        public ICommand GenerateTourReportCommand
        {
            get
            {
                return new RelayCommand(action =>
                {
                    GenerateTourReport();
                });
            }
        }

        public ICommand DeleteLogCommand
        {
            get
            {
                return new RelayCommand(action =>
                {
                    DeleteLog();
                });
            }
        }
        public ICommand DeleteTourCommand
        {
            get
            {
                return new RelayCommand(action =>
                {
                    DeleteTour();
                });
            }
        }

        public ICommand DisplayAddTourViewCommand
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

        public ICommand DisplayAddTourLogViewCommand
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

        public ICommand DisplayEditTourViewCommand
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

        public ICommand DisplayEditTourLogViewCommand
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

        private enum FileType
        {
            pdf = 0,
            json = 1
        }
    }
}
