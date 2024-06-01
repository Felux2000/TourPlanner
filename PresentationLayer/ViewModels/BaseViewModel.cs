﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TourPlanner.PresentationLayer.ViewModels
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        public EventHandler OnRequestClose;
        public event PropertyChangedEventHandler PropertyChanged;
        public EventHandler WebViewRefreshEventHandler;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
