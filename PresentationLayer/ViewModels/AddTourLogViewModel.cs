using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using TourPlanner.BusinessLayer;
using TourPlanner.PresentationLayer.Commands;
using TourPlanner.HelperLayer.Models;

namespace TourPlanner.PresentationLayer.ViewModels
{
    public class AddTourLogViewModel : BaseViewModel
    {
        public AddTourLogViewModel(BLHandler blHandler, MainViewModel mainViewModel)
        {
            _blHandler = blHandler;
            _tourToEdit = mainViewModel.SelectedTour;
            CreateLogDate = DateTime.Now;
            createLogDiff = 1;
            createLogRate = 1;

            CreateTourLogCommand = new RelayCommand(o => CreateTourLog());
            CloseCreateTourLogWindow = new RelayCommand(o => CloseWindow());

            rateColor = new();
            diffColor = new();
            ChangeRateColor();
            ChangeDiffColor();
            loadingMessageText = "Fill out the details to continue...";
            FormActive = true;
        }

        private BLHandler _blHandler;
        private Tour _tourToEdit;
        public event EventHandler OnRequestClose;
        public ICommand CreateTourLogCommand { get; set; }
        public ICommand CloseCreateTourLogWindow { get; set; }
        private DateTime createLogDate { get; set; }
        private TimeSpan createLogDuration { get; set; }
        private double createLogDist { get; set; }
        private string createLogComment { get; set; }
        private int createLogDiff { get; set; }
        private int createLogRate { get; set; }
        private SolidColorBrush rateColor { get; set; }
        private SolidColorBrush diffColor { get; set; }
        private bool tourLogValid { get; set; }
        private bool formActive { get; set; }
        private string loadingMessageText { get; set; }

        public DateTime CreateLogDate
        {
            get
            {
                return createLogDate;
            }

            set
            {
                createLogDate = value;
                ValidateForm();
                OnPropertyChanged();
            }
        }
        public TimeSpan CreateLogDuration
        {
            get
            {
                return createLogDuration;
            }

            set
            {
                createLogDuration = value;
                ValidateForm();
                OnPropertyChanged();
            }
        }
        public double CreateLogDist
        {
            get
            {
                return createLogDist;
            }

            set
            {
                createLogDist = value;
                ValidateForm();
                OnPropertyChanged();
            }
        }
        public string CreateLogComment
        {
            get
            {
                return createLogComment;
            }

            set
            {
                createLogComment = value;
                OnPropertyChanged();
            }
        }
        public int CreateLogDiff
        {
            get
            {
                return createLogDiff;
            }

            set
            {
                createLogDiff = value;
                OnPropertyChanged();
                ChangeDiffColor();
            }
        }
        public int CreateLogRate
        {
            get
            {
                return createLogRate;
            }

            set
            {
                createLogRate = value;
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
            int red = 500 - 50 * (CreateLogRate - 1);
            int green = 50 * (CreateLogRate - 1);
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
            int red = 50 * (CreateLogDiff - 1);
            int green = 500 - 50 * (CreateLogDiff - 1);
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

        public void CreateTourLog()
        {
            if (ValidForm())
            {
                FormActive = false;
                if (!_blHandler.SaveTourLogDb(_tourToEdit, new TourLog(CreateLogDate, CreateLogDuration, (float)CreateLogDist, CreateLogComment, CreateLogDiff, CreateLogRate)))
                {
                    LoadingMessageText = "Log could not be saved!";
                    MessageBox.Show("Unable to save log, try again.", "Save error", MessageBoxButton.OK, MessageBoxImage.Error);
                    TourLogValid = false;
                }
                else
                {
                    OnRequestClose(this, new EventArgs());
                }
            }
            FormActive = true;
        }

        private bool ValidForm()
        {
            if (CreateLogDate == null || CreateLogDate > DateTime.Now)
                return false;
            if (CreateLogDuration == null || CreateLogDuration <= TimeSpan.Zero)
                return false;
            if (CreateLogDist == null || CreateLogDist <= 0)
                return false;
            if ((CreateLogDist > 0 && CreateLogDuration <= TimeSpan.Zero) || (CreateLogDist <= 0 && CreateLogDuration > TimeSpan.Zero))
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
