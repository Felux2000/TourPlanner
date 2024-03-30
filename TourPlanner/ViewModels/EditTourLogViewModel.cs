using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;
using TourPlanner.Commands;
using TourPlanner.Models;

namespace TourPlanner.ViewModels
{
    class EditTourLogViewModel : INotifyPropertyChanged
    {
        public EditTourLogViewModel(MainViewModel mainViewModel)
        {
            this.mainViewModel = mainViewModel;
            EditTourLogCommand = new RelayCommand(o => SaveChangedTourLog());
            CloseEditTourLogWindow = new RelayCommand(o => CloseWindow());
            rateColor = new();
            diffColor = new();
            ChangeDiffColor();
            ChangeRateColor();
            selectedTour = mainViewModel.SelectedTour;
            LoadLogInformation();
        }

        private MainViewModel mainViewModel;
        public event EventHandler OnRequestClose;
        private TourLog oldLog;
        private Tour selectedTour;
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
            int red = 500 - 50 * EditLogRate;
            int green = 50 * EditLogRate;
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
            int red = 50 * EditLogDiff;
            int green = 500 - 50 * EditLogDiff;
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
            oldLog = mainViewModel.SelectedLog;
            EditLogDate = oldLog.Date;
            EditLogDuration = oldLog.Duration;
            EditLogDist = oldLog.Distance;
            EditLogComment = oldLog.Comment;
            EditLogDiff = oldLog.Difficulty;
            EditLogRate = oldLog.Rating;
        }

        public void SaveChangedTourLog()
        {
            TourLog newLog = new TourLog(EditLogDate, EditLogDuration, (float)EditLogDist, EditLogComment, EditLogDiff, EditLogRate);
            mainViewModel.EditTourLog(selectedTour, oldLog, newLog);
            OnRequestClose(this, new EventArgs());
        }
        public void CloseWindow()
        {
            OnRequestClose(this, new EventArgs());
        }


        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
