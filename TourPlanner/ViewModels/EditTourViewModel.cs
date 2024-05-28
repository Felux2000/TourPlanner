using Azure;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TourPlanner.BusinessLogic;
using TourPlanner.BusinessLogic.API.Models;
using TourPlanner.Commands;
using TourPlanner.Models;

namespace TourPlanner.ViewModels
{
    public class EditTourViewModel : BaseViewModel
    {
        public EditTourViewModel(BLHandler blHandler, MainViewModel mainViewModel)
        {
            _blHandler = blHandler;
            _tourToEdit = mainViewModel.SelectedTour;
            SaveChangedTourCommand = new RelayCommand(o => SaveChangedTour());
            CloseEditTourWindow = new RelayCommand(o => CloseWindow());
            LoadTourInformation();
            FormActive = true;
        }

        private BLHandler _blHandler;
        private Tour _tourToEdit;

        public event EventHandler OnRequestClose;

        public ICommand SaveChangedTourCommand { get; set; }
        public ICommand CloseEditTourWindow { get; set; }

        private string editTourName { get; set; }
        private string editTourDescr { get; set; }
        private string editTourFrom { get; set; }
        private string editTourTo { get; set; }
        private string editTourTransportType { get; set; }
        private float editTourDist { get; set; }
        private float editTourEst { get; set; }
        public string editTourImage { get; set; }
        private bool tourLoadSuccessful { get; set; }
        private bool formActive { get; set; }
        private string loadingMessageText { get; set; }
        private Task<bool> tourLoadTask;
        private string lastResponse;
        private ResponseDirectionsModel lastTourDirections;

        public string EditTourName
        {
            get
            {
                return editTourName;
            }

            set
            {
                editTourName = value;
                StartTourLoad();
                OnPropertyChanged();
            }
        }
        public string EditTourDescr
        {
            get
            {
                return editTourDescr;
            }

            set
            {
                editTourDescr = value;
                OnPropertyChanged();
            }
        }
        public string EditTourFrom
        {
            get
            {
                return editTourFrom;
            }

            set
            {
                editTourFrom = value;
                OnPropertyChanged();
            }
        }
        public string EditTourTo
        {
            get
            {
                return editTourTo;
            }

            set
            {
                editTourTo = value;
                OnPropertyChanged();
            }
        }
        public string EditTourTransportType
        {
            get
            {
                return editTourTransportType;
            }

            set
            {
                editTourTransportType = value;
                StartTourLoad();
                OnPropertyChanged();
            }
        }
        public float EditTourDist
        {
            get
            {
                return editTourDist;
            }

            set
            {
                editTourDist = (float)Math.Round(value, 2);
                OnPropertyChanged();
            }
        }
        public float EditTourEst
        {
            get
            {
                return editTourEst;
            }

            set
            {
                editTourEst = value;
                OnPropertyChanged();
            }
        }
        public string EditTourImage
        {
            get
            {
                return editTourImage;
            }

            set
            {
                editTourImage = value;
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

        private void LoadTourInformation()
        {
            EditTourName = _tourToEdit.Name;
            EditTourDescr = _tourToEdit.Description;
            EditTourFrom = _tourToEdit.From;
            EditTourTo = _tourToEdit.To;
            EditTourTransportType = _tourToEdit.TransportType;
            EditTourDist = _tourToEdit.Distance;
            EditTourEst = _tourToEdit.Estimation;
            StartTourLoad();
        }

        public void CloseWindow()
        {
            OnRequestClose(this, new EventArgs());
        }

        public async void SaveChangedTour()
        {
            FormActive = false;
            if (await tourLoadTask && TourLoadSuccessful)
            {
                UpdateTourInformation();
                if (!_blHandler.UpdateTourDb(_tourToEdit))
                {
                    LoadingMessageText = "Tour could not be saved!";
                    MessageBox.Show("Unable to save tour, try again.", "Save error", MessageBoxButton.OK, MessageBoxImage.Error);
                    TourLoadSuccessful = false;
                }
                else
                {
                    OnRequestClose(this, new EventArgs());
                }
            }
            FormActive = true;
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
                sw.Write(lastResponse);
                sw.Write(";");
            }
        }

        private bool ValidForm()
        {
            if (EditTourName == null || EditTourName == string.Empty)
                return false;
            if (EditTourTo == null || EditTourTo == string.Empty)
                return false;
            if (EditTourFrom == null || EditTourFrom == string.Empty)
                return false;
            if (EditTourTransportType == null)
                return false;
            return true;
        }

        private void StartTourLoad()
        {
            if (tourLoadTask == null || tourLoadTask.IsCompleted)
                tourLoadTask = LoadTOur();
        }

        private void UpdateTourInformation()
        {
            _tourToEdit.Name = EditTourName;
            _tourToEdit.Description = EditTourDescr;
            _tourToEdit.From = EditTourFrom;
            _tourToEdit.To = EditTourTo;
            _tourToEdit.TransportType = EditTourTransportType;
            _tourToEdit.Distance = EditTourDist;
            _tourToEdit.Estimation = EditTourEst;
            _tourToEdit.MapJson = lastResponse;
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
                var apiCall = _blHandler.GetTourDetails(EditTourFrom, EditTourTo, EditTourTransportType);

                (lastResponse, lastTourDirections) = await apiCall;
                if (lastResponse != null)
                {
                    LoadingMessageText = string.Empty;
                    WriteJs();
                    WebViewRefreshEventHandler.Invoke(this, null);
                    //delay for webview loading
                    await Task.Delay(50);
                    EditTourDist = (float)lastTourDirections.Features[0].Properties.Summary.Distance;
                    EditTourEst = (float)lastTourDirections.Features[0].Properties.Summary.Duration;
                    TourLoadSuccessful = true;
                    return true;
                }
                else
                {
                    EditTourDist = 0;
                    EditTourEst = 0;
                    LoadingMessageText = "Invalid tour details...";
                    return false;
                }
            }
            else
            {
                EditTourDist = 0;
                EditTourEst = 0;
                return false;
            }
        }
    }
}
