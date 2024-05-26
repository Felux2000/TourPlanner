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
    public class AddTourLogViewModel : BaseViewModel
    {
        public AddTourLogViewModel(BLHandler blHandler, MainViewModel mainViewModel)
        {
            _blHandler = blHandler;
            _mainViewModel = mainViewModel;
            CreateLogDate = DateTime.Now;
            createLogDiff = 1;
            createLogRate = 1;

            CreateTourLogCommand = new RelayCommand(o => CreateTourLog());
            CloseCreateTourLogWindow = new RelayCommand(o => CloseWindow());

            rateColor = new();
            diffColor = new();
            ChangeRateColor();
            ChangeDiffColor();
        }

        private BLHandler _blHandler;
        private MainViewModel _mainViewModel;
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

        public DateTime CreateLogDate
        {
            get
            {
                return createLogDate;
            }

            set
            {
                createLogDate = value;
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
            //TourLog newTourLog = new TourLog(CreateLogDate, CreateLogDuration, (float)CreateLogDist, CreateLogComment, CreateLogDiff, CreateLogRate);
            //_mainViewModel.AddTourLog(newTourLog);
            OnRequestClose(this, new EventArgs());
        }
        public void CloseWindow()
        {
            OnRequestClose(this, new EventArgs());
        }
    }
}
