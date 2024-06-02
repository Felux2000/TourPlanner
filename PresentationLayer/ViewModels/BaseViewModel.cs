using System.ComponentModel;
using System.Runtime.CompilerServices;

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
