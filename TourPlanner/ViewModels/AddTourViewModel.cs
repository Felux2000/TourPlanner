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
    internal class AddTourViewModel : INotifyPropertyChanged
    {
        public AddTourViewModel(MainViewModel mainViewModel)
        {
            this.mainViewModel = mainViewModel;
            CreateTourCommand = new RelayCommand(o => CreateTour());
            CloseCreateTourWindow = new RelayCommand(o => CloseWindow());
        }

        private MainViewModel mainViewModel;

        public event EventHandler OnRequestClose;
        public ICommand CreateTourCommand { get; set; }
        public ICommand CloseCreateTourWindow { get; set; }
        private string createTourName { get; set; }
        private string createTourDescr { get; set; }
        private string createTourFrom { get; set; }
        private string createTourTo { get; set; }
        private string createTourTransportType { get; set; }
        private float createTourDist { get; set; }
        private float createTourEst { get; set; }

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
                createTourDist = value;
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

        public void CreateTour()
        {
            createTourDist = 1234;
            createTourEst = 1234;
            Tour newTour = new Tour(CreateTourName, CreateTourDescr, CreateTourFrom, CreateTourTo, CreateTourTransportType, CreateTourDist, CreateTourEst, "/Resources/exampleImage.png");
            mainViewModel.AddTour(newTour);
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
