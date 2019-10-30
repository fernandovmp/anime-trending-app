using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace AnimeTrendingApp.ViewModels
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string property = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        protected bool SetProperty<T>(ref T attribute, T value, [CallerMemberName] string property = "")
        {
            if(EqualityComparer<T>.Default.Equals(attribute, value))
            {
                return false;
            }
            attribute = value;
            OnPropertyChanged(property);
            return true;
        }
    }
}
