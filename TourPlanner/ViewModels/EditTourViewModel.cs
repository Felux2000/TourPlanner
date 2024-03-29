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
    class EditTourViewModel : INotifyPropertyChanged
    {
        public EditTourViewModel(MainViewModel mainViewModel)
        {
            this.mainViewModel = mainViewModel;
            SaveChangedTourCommand = new RelayCommand(o => SaveChangedTour());
            CloseEditTourWindow = new RelayCommand(o => CloseWindow());
            LoadTourInformation();
        }

        private MainViewModel mainViewModel;

        public event EventHandler OnRequestClose;

        public ICommand SaveChangedTourCommand { get; set; }
        public ICommand CloseEditTourWindow { get; set; }

        private Tour oldTour;
        private string editTourName { get; set; }
        private string editTourDescr { get; set; }
        private string editTourFrom { get; set; }
        private string editTourTo { get; set; }
        private string editTourTransportType { get; set; }
        private float editTourDist { get; set; }
        private float editTourEst { get; set; }
        public string editTourImage { get; set; }

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
        public float EditTourDist
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

        private void LoadTourInformation()
        {
            oldTour = mainViewModel.SelectedTour;
            EditTourName = oldTour.Name;
            EditTourDescr = oldTour.Description;
            EditTourFrom = oldTour.From;
            EditTourTo = oldTour.To;
            EditTourTransportType = oldTour.TransportType;
            EditTourDist = oldTour.Distance;
            EditTourEst = oldTour.Estimation;
            EditTourImage = oldTour.Image;
        }

        public void SaveChangedTour()
        {
            Tour newTour = oldTour;
            newTour.Description = EditTourDescr;
            newTour.TransportType = EditTourTransportType;
            mainViewModel.EditTour(oldTour, newTour);
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
