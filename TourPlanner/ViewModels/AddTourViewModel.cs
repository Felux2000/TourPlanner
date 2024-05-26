using Azure;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TourPlanner.BusinessLogic;
using TourPlanner.BusinessLogic.API.Models;
using TourPlanner.Commands;
using TourPlanner.Models;

namespace TourPlanner.ViewModels
{
    public class AddTourViewModel : BaseViewModel
    {
        public AddTourViewModel(BLHandler blHandler, MainViewModel mainViewModel)
        {
            _blHandler = blHandler;
            _mainViewModel = mainViewModel;
            CreateTourCommand = new RelayCommand(o => CreateTour());
            CloseCreateTourWindow = new RelayCommand(o => CloseWindow());
            TourLoadSuccessful = false;
            loadingMessageText = "Fill out the details to continue...";
        }

        private BLHandler _blHandler;
        private MainViewModel _mainViewModel;

        public event EventHandler OnRequestClose;
        public ICommand CreateTourCommand { get; set; }
        public ICommand CloseCreateTourWindow { get; set; }
        private string createTourName { get; set; }
        private string createTourDescr { get; set; }
        private string createTourFrom { get; set; }
        private string createTourTo { get; set; }
        private string createTourTransportType { get; set; }
        private string createTourDist { get; set; }
        private string createTourEst { get; set; }
        private bool tourLoadSuccessful { get; set; }
        private string loadingMessageText { get; set; }
        private Task<bool> tourLoadTask;

        public string CreateTourName
        {
            get
            {
                return createTourName;
            }

            set
            {
                createTourName = value;
                StartTourLoad();
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
        public string CreateTourDist
        {
            get
            {
                return createTourDist;
            }

            set
            {
                createTourDist = value;
                OnPropertyChanged();
            }
        }
        public string CreateTourEst
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

        public void CreateTour()
        {
            Task.Delay(1200).ContinueWith(async _ =>
            {
                if (await tourLoadTask)
                {
                    //save in database
                    OnRequestClose(this, new EventArgs());
                }
            });
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
            {
                tourLoadTask = LoadTOur();
            }
        }

        private async Task<bool> LoadTOur()
        {
            LoadingMessageText = "Fill out the details to continue...";
            if (ValidForm())
            {
                var apiCall = _blHandler.ApiHandler.GetDirections(CreateTourFrom, CreateTourTo, CreateTourTransportType);
                LoadingMessageText = "Tour is loading...";

                ResponseDirectionsModel response = await apiCall;
                if (response != null)
                {
                    CreateTourDist = $"{Math.Round(response.Features[0].Properties.Summary.Distance / 1000, 2)} km";
                    CreateTourEst = $"{TimeSpan.FromSeconds(response.Features[0].Properties.Summary.Duration).ToString(@"hh\:mm")} h";
                    TourLoadSuccessful = true;
                    return true;
                }
                else
                {
                    CreateTourDist = $"0 km";
                    CreateTourEst = $"{TimeSpan.FromSeconds(0).ToString(@"hh\:mm")} h";
                    LoadingMessageText = "Invalid tour details...";
                    TourLoadSuccessful = false;
                    return false;
                }
            }
            else
            {
                CreateTourDist = $"0 km";
                CreateTourEst = $"{TimeSpan.FromSeconds(0).ToString(@"hh\:mm")} h";
                TourLoadSuccessful = false;
                return false;
            }
        }
    }
}
