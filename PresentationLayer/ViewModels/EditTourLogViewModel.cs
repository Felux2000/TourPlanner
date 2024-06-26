﻿using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using TourPlanner.BusinessLayer;
using TourPlanner.DataLayer.Exceptions;
using TourPlanner.HelperLayer.Models;
using TourPlanner.HelperLayer.Services;
using TourPlanner.PresentationLayer.Commands;

namespace TourPlanner.PresentationLayer.ViewModels
{
    public class EditTourLogViewModel : BaseViewModel
    {
        public EditTourLogViewModel(BLHandler blHandler, MainViewModel mainViewModel, DialogService dialogService)
        {
            _blHandler = blHandler;
            _logToEdit = mainViewModel.SelectedLog;
            _dialogService = dialogService;
            EditTourLogCommand = new RelayCommand(o => SaveChangedTourLog());
            CloseEditTourLogWindow = new RelayCommand(o => CloseWindow());
            rateColor = new();
            diffColor = new();
            ChangeDiffColor();
            ChangeRateColor();
            LoadLogInformation();
            FormActive = true;
        }

        private BLHandler _blHandler;
        private DialogService _dialogService;
        private TourLog _logToEdit;
        public ICommand EditTourLogCommand { get; set; }
        public ICommand CloseEditTourLogWindow { get; set; }
        private DateTime editLogDate { get; set; }
        private TimeSpan editLogDuration { get; set; }
        private double editLogDist { get; set; }
        private string editLogComment { get; set; }
        private int editLogDiff { get; set; }
        private int editLogRate { get; set; }
        private SolidColorBrush rateColor { get; set; }
        private SolidColorBrush diffColor { get; set; }
        private bool tourLogValid { get; set; }
        private bool formActive { get; set; }
        private string loadingMessageText { get; set; }

        public DateTime EditLogDate
        {
            get
            {
                return editLogDate;
            }

            set
            {
                editLogDate = value;
                ValidateForm();
                OnPropertyChanged();
            }
        }
        public TimeSpan EditLogDuration
        {
            get
            {
                return editLogDuration;
            }

            set
            {
                editLogDuration = value;
                ValidateForm();
                OnPropertyChanged();
            }
        }
        public double EditLogDist
        {
            get
            {
                return editLogDist;
            }

            set
            {
                editLogDist = value;
                ValidateForm();
                OnPropertyChanged();
            }
        }
        public string EditLogComment
        {
            get
            {
                return editLogComment;
            }

            set
            {
                editLogComment = value;
                OnPropertyChanged();
            }
        }
        public int EditLogDiff
        {
            get
            {
                return editLogDiff;
            }

            set
            {
                editLogDiff = value;
                OnPropertyChanged();
                ChangeDiffColor();
            }
        }
        public int EditLogRate
        {
            get
            {
                return editLogRate;
            }

            set
            {
                editLogRate = value;
                OnPropertyChanged();
                ChangeRateColor();
            }
        }

        public SolidColorBrush RateColor
        {
            get
            {
                return rateColor;
            }

            set
            {
                rateColor.Color = value.Color;
                OnPropertyChanged();
            }
        }

        public SolidColorBrush DiffColor
        {
            get
            {
                return diffColor;
            }

            set
            {
                diffColor.Color = value.Color;
                OnPropertyChanged();
            }
        }
        public bool TourLogValid
        {
            get
            {
                return tourLogValid;
            }

            set
            {
                tourLogValid = value;
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

        private void ChangeRateColor()
        {
            int red = 500 - 50 * (EditLogRate - 1);
            int green = 50 * (EditLogRate - 1);
            if (red > 250)
            {
                red = 250;
            }
            if (green > 250)
            {
                green = 250;
            }
            RateColor.Color = Color.FromRgb((byte)red, (byte)green, 20);
        }


        private void ChangeDiffColor()
        {
            int red = 50 * (EditLogDiff - 1);
            int green = 500 - 50 * (EditLogDiff - 1);
            if (red > 250)
            {
                red = 250;
            }
            if (green > 250)
            {
                green = 250;
            }
            DiffColor.Color = Color.FromRgb((byte)red, (byte)green, 20);
        }

        private void LoadLogInformation()
        {
            EditLogDate = _logToEdit.Date;
            EditLogDuration = _logToEdit.Duration;
            EditLogDist = _logToEdit.Distance;
            EditLogComment = _logToEdit.Comment;
            EditLogDiff = _logToEdit.Difficulty;
            EditLogRate = _logToEdit.Rating;
        }

        public void SaveChangedTourLog()
        {
            if (ValidForm())
            {
                FormActive = false;
                UpdateTourLogInformation();
                try
                {
                    _blHandler.UpdateTourLogDb(_logToEdit);
                    OnRequestClose(this, new EventArgs());
                }
                catch (Exception ex) when (ex is DLInvalidEntityException || ex is DLEntityNotFoundException)
                {
                    LoadingMessageText = "Log could not be saved!";
                    TourLogValid = false;
                    _dialogService.ShowMessageBox("Unable to save log, try again.", "Save error");
                }
                catch
                {
                    if (_dialogService.ShowMessageBox("Unable to connect to server", "Fatal error", true) == DialogResult.OK)
                        OnRequestClose(this, new EventArgs());
                }
            }
            FormActive = true;
        }
        private void UpdateTourLogInformation()
        {
            _logToEdit.Date = EditLogDate;
            _logToEdit.Duration = EditLogDuration;
            _logToEdit.Difficulty = EditLogDiff;
            _logToEdit.Distance = (float)EditLogDist;
            _logToEdit.Comment = EditLogComment;
            _logToEdit.Rating = EditLogRate;
        }
        private bool ValidForm()
        {
            if (EditLogDate == null || EditLogDate > DateTime.Now)
                return false;
            if (EditLogDuration == null || EditLogDuration <= TimeSpan.Zero)
                return false;
            if (EditLogDist == null || EditLogDist <= 0)
                return false;
            if ((EditLogDist > 0 && EditLogDuration <= TimeSpan.Zero) || (EditLogDist <= 0 && EditLogDuration > TimeSpan.Zero))
                return false;
            return true;
        }

        private void ValidateForm()
        {
            TourLogValid = false;
            if (ValidForm())
            {
                LoadingMessageText = "Fill out the details to continue...";
                TourLogValid = true;
            }
            else
                LoadingMessageText = "Invalid tour records...";
        }

        public void CloseWindow()
        {
            OnRequestClose(this, new EventArgs());
        }
    }
}
