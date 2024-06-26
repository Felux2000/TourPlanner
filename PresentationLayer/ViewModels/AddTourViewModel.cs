﻿using System.IO;
using System.Windows.Forms;
using System.Windows.Input;
using TourPlanner.BusinessLayer;
using TourPlanner.BusinessLayer.API.Models;
using TourPlanner.DataLayer.Exceptions;
using TourPlanner.HelperLayer.Logger;
using TourPlanner.HelperLayer.Models;
using TourPlanner.HelperLayer.Services;
using TourPlanner.PresentationLayer.Commands;

namespace TourPlanner.PresentationLayer.ViewModels
{
    public class AddTourViewModel : BaseViewModel
    {
        public AddTourViewModel(BLHandler blHandler, DialogService dialogService)
        {
            _blHandler = blHandler;
            _dialogService = dialogService;
            CreateTourCommand = new RelayCommand(o => CreateTour());
            CloseCreateTourWindow = new RelayCommand(o => CloseWindow());
            TourLoadSuccessful = false;
            FormActive = true;
            loadingMessageText = "Fill out the details to continue...";
        }

        private BLHandler _blHandler;
        private DialogService _dialogService;
        private static readonly ILoggerWrapper _logger = LoggerFactory.GetLogger();

        public ICommand CreateTourCommand { get; set; }
        public ICommand CloseCreateTourWindow { get; set; }
        private string createTourName { get; set; }
        private string createTourDescr { get; set; }
        private string createTourFrom { get; set; }
        private string createTourTo { get; set; }
        private string createTourTransportType { get; set; }
        private float createTourDist { get; set; }
        private float createTourEst { get; set; }
        private bool tourLoadSuccessful { get; set; }
        private bool formActive { get; set; }
        private string loadingMessageText { get; set; }
        private Task<bool> tourLoadTask;
        private string lastResponse;
        private ResponseDirectionsModel lastTourDirections;

        public string CreateTourName
        {
            get
            {
                return createTourName;
            }

            set
            {
                createTourName = value;
                OnPropertyChanged();
            }
        }
        public string CreateTourDescr
        {
            get
            {
                return createTourDescr;
            }

            set
            {
                createTourDescr = value;
                OnPropertyChanged();
            }
        }
        public string CreateTourFrom
        {
            get
            {
                return createTourFrom;
            }

            set
            {
                createTourFrom = value;
                StartTourLoad();
                OnPropertyChanged();
            }
        }
        public string CreateTourTo
        {
            get
            {
                return createTourTo;
            }

            set
            {
                createTourTo = value;
                StartTourLoad();
                OnPropertyChanged();
            }
        }
        public string CreateTourTransportType
        {
            get
            {
                return createTourTransportType;
            }

            set
            {
                createTourTransportType = value;
                StartTourLoad();
                OnPropertyChanged();
            }
        }
        public float CreateTourDist
        {
            get
            {
                return createTourDist;
            }

            set
            {
                createTourDist = (float)Math.Round(value, 2);
                OnPropertyChanged();
            }
        }
        public float CreateTourEst
        {
            get
            {
                return createTourEst;
            }

            set
            {
                createTourEst = value;
                OnPropertyChanged();
            }
        }
        public bool TourLoadSuccessful
        {
            get
            {
                return tourLoadSuccessful;
            }

            set
            {
                tourLoadSuccessful = value;
                OnPropertyChanged();
            }
        }
        public bool FormActive
        {
            get
            {
                return formActive;
            }

            set
            {
                formActive = value;
                OnPropertyChanged();
            }
        }
        public string LoadingMessageText
        {
            get
            {
                return loadingMessageText;
            }

            set
            {
                loadingMessageText = value;
                OnPropertyChanged();
            }
        }

        public async void CreateTour()
        {
            FormActive = false;
            if (await tourLoadTask && TourLoadSuccessful)
            {
                Tour newTour = new Tour(CreateTourName, CreateTourDescr, CreateTourFrom, CreateTourTo, CreateTourTransportType, createTourDist, createTourEst, lastResponse);
                try
                {
                    _blHandler.SaveTourDb(newTour);
                    OnRequestClose(this, new EventArgs());
                }
                catch (Exception ex) when (ex is DLInvalidEntityException || ex is DLEntityNotFoundException)
                {
                    LoadingMessageText = "Tour could not be saved!";
                    TourLoadSuccessful = false;
                    _dialogService.ShowMessageBox("Invalid tour details.", "Save error");
                }
                catch
                {
                    if (_dialogService.ShowMessageBox("Unable to connect to server.", "Fatal error", true) == DialogResult.OK)
                        OnRequestClose(this, new EventArgs());
                }
            }
            FormActive = true;
        }

        private void WriteJs()
        {
            try
            {
                string jsPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources/WebView/directions.js");
                using (StreamWriter sw = new StreamWriter(jsPath, false))
                {
                    sw.Write($"var directions = ");
                }
                using (StreamWriter sw = new StreamWriter(jsPath, true))
                {
                    sw.Write(lastResponse);
                    sw.Write(";");
                }
            }
            catch (Exception ex)
            {
                _logger.Fatal($"Webview rewrite threw {ex}: {ex.Message}");
                _dialogService.ShowMessageBox("Tour map could not be loaded.", "Fatal error", true);
                OnRequestClose(this, new EventArgs());
            }
        }

        public void CloseWindow()
        {
            OnRequestClose(this, new EventArgs());
        }

        private bool ValidForm()
        {
            if (CreateTourName == null || CreateTourName == string.Empty)
                return false;
            if (CreateTourTo == null || CreateTourTo == string.Empty)
                return false;
            if (CreateTourFrom == null || CreateTourFrom == string.Empty)
                return false;
            if (CreateTourTransportType == null)
                return false;
            return true;
        }

        private void StartTourLoad()
        {
            if (tourLoadTask == null || tourLoadTask.IsCompleted)
                tourLoadTask = LoadTOur();
        }

        private async Task<bool> LoadTOur()
        {
            LoadingMessageText = "Fill out the details to continue...";
            TourLoadSuccessful = false;
            //delay for input
            await Task.Delay(1000);
            if (ValidForm())
            {
                LoadingMessageText = "Tour is loading...";
                var apiCall = _blHandler.GetTourDetails(CreateTourFrom, CreateTourTo, CreateTourTransportType);

                (lastResponse, lastTourDirections) = await apiCall;
                if (lastResponse != null)
                {
                    LoadingMessageText = string.Empty;
                    WriteJs();
                    WebViewRefreshEventHandler.Invoke(this, null);
                    CreateTourDist = (float)lastTourDirections.Features[0].Properties.Summary.Distance;
                    CreateTourEst = (float)lastTourDirections.Features[0].Properties.Summary.Duration;
                    TourLoadSuccessful = true;
                    return true;
                }
                else
                {
                    CreateTourDist = 0;
                    CreateTourEst = 0;
                    LoadingMessageText = "Invalid tour details...";
                    return false;
                }
            }
            else
            {
                CreateTourDist = 0;
                CreateTourEst = 0;
                return false;
            }
        }
    }
}
