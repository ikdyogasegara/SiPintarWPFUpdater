using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace AppBusiness.Data.DTOs
{
    public class PermohonanPelangganLimbahWpf : PermohonanPelangganLimbahDto, INotifyPropertyChanged
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

        public string KeteranganWpf
        {
            get { return Keterangan; }
            set
            {
                Keterangan = value;
                OnPropertyChanged();
            }
        }

        public List<PermohonanPelangganLimbahDetailDto> ParameterWpf
        {
            get { return Parameter; }
            set
            {
                Parameter = value;
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
