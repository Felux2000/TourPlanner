using Azure;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    public class EditTourViewModel : BaseViewModel
    {
        public EditTourViewModel(BLHandler blHandler, MainViewModel mainViewModel)
        {
            _blHandler = blHandler;
            _mainViewModel = mainViewModel;
            _tourToEdit = mainViewModel.SelectedTour;
            SaveChangedTourCommand = new RelayCommand(o => SaveChangedTour());
            CloseEditTourWindow = new RelayCommand(o => CloseWindow());
            LoadTourInformation();
        }

        private BLHandler _blHandler;
        private MainViewModel _mainViewModel;
        private Tour _tourToEdit;

        public event EventHandler OnRequestClose;

        public ICommand SaveChangedTourCommand { get; set; }
        public ICommand CloseEditTourWindow { get; set; }

        private string editTourName { get; set; }
        private string editTourDescr { get; set; }
        private string editTourFrom { get; set; }
        private string editTourTo { get; set; }
        private string editTourTransportType { get; set; }
        private string editTourDist { get; set; }
        private string editTourEst { get; set; }
        public string editTourImage { get; set; }
        private bool tourLoadSuccessful { get; set; }
        private string loadingMessageText { get; set; }
        private Task<bool> tourLoadTask;

        public string EditTourName
        {
            get
            {
                return editTourName;
            }

            set
            {
                editTourName = value;
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
                OnPropertyChanged();
            }
        }
        public string EditTourDist
        {
            get
            {
                return editTourDist;
            }

            set
            {
                editTourDist = value;
                OnPropertyChanged();
            }
        }
        public string EditTourEst
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
            EditTourDist = $"{Math.Round(_tourToEdit.Distance / 1000, 2)} km";
            EditTourEst = $"{TimeSpan.FromSeconds(_tourToEdit.Estimation).ToString(@"hh\:mm")} h";
            //EditTourImage = _tourToEdit.Image;
        }

        public void SaveChangedTour()
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
            {
                tourLoadTask = LoadTOur();
            }
        }

        private async Task<bool> LoadTOur()
        {
            LoadingMessageText = "Fill out the details to continue...";
            if (ValidForm())
            {
                var apiCall = _blHandler.ApiHandler.GetDirections(EditTourFrom, EditTourTo, EditTourTransportType);
                LoadingMessageText = "Tour is loading...";

                ResponseDirectionsModel response = await apiCall;
                if (response != null)
                {
                    EditTourDist = $"{Math.Round(response.Features[0].Properties.Summary.Distance / 1000, 2)} km";
                    EditTourEst = $"{TimeSpan.FromSeconds(response.Features[0].Properties.Summary.Duration).ToString(@"hh\:mm")} h";
                    TourLoadSuccessful = true;
                    return true;
                }
                else
                {
                    EditTourDist = $"0 km";
                    EditTourEst = $"{TimeSpan.FromSeconds(0).ToString(@"hh\:mm")} h";
                    LoadingMessageText = "Invalid tour details...";
                    TourLoadSuccessful = false;
                    return false;
                }
            }
            else
            {
                EditTourDist = $"0 km";
                EditTourEst = $"{TimeSpan.FromSeconds(0).ToString(@"hh\:mm")} h";
                TourLoadSuccessful = false;
                return false;
            }
        }
    }
}
