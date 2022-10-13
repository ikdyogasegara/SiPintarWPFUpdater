using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace AppBusiness.Data.DTOs
{
    public class DetailRabWpf : DetailRabDto, INotifyPropertyChanged
    {
        private bool _isUpdatingKoreksi;
        public bool IsUpdatingKoreksi
        {
            get { return _isUpdatingKoreksi; }
            set
            {
                _isUpdatingKoreksi = value;
                OnPropertyChanged();
            }
        }

        public bool? FlagBiayaDibebankanKePdamWpf
        {
            get { return FlagBiayaDibebankanKePdam; }
            set
            {
                FlagBiayaDibebankanKePdam = value;
                OnPropertyChanged();
            }
        }

        public bool? FlagDialihkanKevendorWpf
        {
            get { return FlagDialihkanKevendor; }
            set
            {
                FlagDialihkanKevendor = value;
                OnPropertyChanged();
            }
        }

        #region INotifyPropertyChanged Members
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
