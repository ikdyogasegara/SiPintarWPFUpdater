using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace AppBusiness.Data.DTOs
{
    public class TagihanRekeningAirWpf : TagihanRekeningAirDto, INotifyPropertyChanged
    {
        private bool _isUpdating { get; set; }
        public bool IsUpdating
        {
            get { return _isUpdating; }
            set
            {
                _isUpdating = value;
                OnPropertyChanged();
            }
        }

        private bool _isSelected { get; set; } = true;
        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                _isSelected = value;
                OnPropertyChanged();
            }
        }

        public decimal? DendaWpf
        {
            get { return Denda; }
            set
            {
                Denda = value;
                OnPropertyChanged();
            }
        }

        public decimal? TotalWpf
        {
            get { return Total; }
            set
            {
                Total = value;
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
