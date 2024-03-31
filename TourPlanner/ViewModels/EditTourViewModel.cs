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
            this.tourToEdit = mainViewModel.SelectedTour;
            SaveChangedTourCommand = new RelayCommand(o => SaveChangedTour());
            CloseEditTourWindow = new RelayCommand(o => CloseWindow());
            LoadTourInformation();
        }

        private MainViewModel mainViewModel;
        private Tour tourToEdit;

        public event EventHandler OnRequestClose;

        public ICommand SaveChangedTourCommand { get; set; }
        public ICommand CloseEditTourWindow { get; set; }

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
            EditTourName = tourToEdit.Name;
            EditTourDescr = tourToEdit.Description;
            EditTourFrom = tourToEdit.From;
            EditTourTo = tourToEdit.To;
            EditTourTransportType = tourToEdit.TransportType;
            EditTourDist = tourToEdit.Distance;
            EditTourEst = tourToEdit.Estimation;
            EditTourImage = tourToEdit.Image;
        }

        public void SaveChangedTour()
        {
            Tour editedTour = new(EditTourName, EditTourDescr, EditTourFrom, EditTourTo, EditTourTransportType,EditTourDist, EditTourEst,EditTourImage);
            mainViewModel.EditTour(editedTour);
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
