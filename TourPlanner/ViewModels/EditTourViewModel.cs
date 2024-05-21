using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TourPlanner.BusinessLogic;
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
            EditTourName = _tourToEdit.Name;
            EditTourDescr = _tourToEdit.Description;
            EditTourFrom = _tourToEdit.From;
            EditTourTo = _tourToEdit.To;
            EditTourTransportType = _tourToEdit.TransportType;
            EditTourDist = _tourToEdit.Distance;
            EditTourEst = _tourToEdit.Estimation;
            EditTourImage = _tourToEdit.Image;
        }

        public void SaveChangedTour()
        {
            Tour editedTour = new(EditTourName, EditTourDescr, EditTourFrom, EditTourTo, EditTourTransportType,EditTourDist, EditTourEst,EditTourImage);
            _mainViewModel.EditTour(editedTour);
            OnRequestClose(this, new EventArgs());
        }

        public void CloseWindow()
        {
            OnRequestClose(this, new EventArgs());
        }
    }
}
