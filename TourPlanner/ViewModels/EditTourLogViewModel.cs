using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;
using TourPlanner.BusinessLogic;
using TourPlanner.Commands;
using TourPlanner.Models;

namespace TourPlanner.ViewModels
{
    public class EditTourLogViewModel : BaseViewModel
    {
        public EditTourLogViewModel(BLHandler blHandler, MainViewModel mainViewModel)
        {
            _blHandler = blHandler;
            _mainViewModel = mainViewModel;
            logToEdit = mainViewModel.SelectedLog;
            EditTourLogCommand = new RelayCommand(o => SaveChangedTourLog());
            CloseEditTourLogWindow = new RelayCommand(o => CloseWindow());
            rateColor = new();
            diffColor = new();
            ChangeDiffColor();
            ChangeRateColor();
            LoadLogInformation();
        }

        private BLHandler _blHandler;
        private MainViewModel _mainViewModel;
        public event EventHandler OnRequestClose;
        private TourLog logToEdit;
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

        public DateTime EditLogDate
        {
            get
            {
                return editLogDate;
            }

            set
            {
                editLogDate = value;
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
            EditLogDate = logToEdit.Date;
            EditLogDuration = logToEdit.Duration;
            EditLogDist = logToEdit.Distance;
            EditLogComment = logToEdit.Comment;
            EditLogDiff = logToEdit.Difficulty;
            EditLogRate = logToEdit.Rating;
        }

        public void SaveChangedTourLog()
        {
            TourLog editedTourLog = new TourLog(EditLogDate, EditLogDuration, (float)EditLogDist, EditLogComment, EditLogDiff, EditLogRate);
            _mainViewModel.EditTourLog(editedTourLog);
            OnRequestClose(this, new EventArgs());
        }
        public void CloseWindow()
        {
            OnRequestClose(this, new EventArgs());
        }
    }
}
