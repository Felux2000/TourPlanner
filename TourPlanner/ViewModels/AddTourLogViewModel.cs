using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TourPlanner.Commands;
using TourPlanner.Models;

namespace TourPlanner.ViewModels
{
    internal class AddTourLogViewModel : INotifyPropertyChanged
    {
        public AddTourLogViewModel(MainViewModel mainViewModel)
        {
            this.mainViewModel = mainViewModel;
            CreateTourLogCommand = new RelayCommand(o => CreateTourLog());
            CloseCreateTourLogWindow = new RelayCommand(o => CloseWindow());
        }

        private MainViewModel mainViewModel;
        public event EventHandler OnRequestClose;
        public ICommand CreateTourLogCommand { get; set; }
        public ICommand CloseCreateTourLogWindow { get; set; }
        private DateTime createLogDate { get; set; }
        private TimeSpan createLogDuration { get; set; }
        private float createLogDist { get; set; }
        private string createLogComment { get; set; }
        private int createLogDiff { get; set; }
        private int createLogRate { get; set; }

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
        public float CreateLogDist
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
            }
        }

        public void CreateTourLog()
        {
            TourLog newTourLog = new TourLog(CreateLogDate, CreateLogDuration, CreateLogDist, CreateLogComment, CreateLogDiff, CreateLogRate);
            mainViewModel.AddTourLog(newTourLog);
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
