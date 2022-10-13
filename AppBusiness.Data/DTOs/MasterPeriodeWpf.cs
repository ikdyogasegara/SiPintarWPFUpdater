using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace AppBusiness.Data.DTOs
{
    public class MasterPeriodeWpf : MasterPeriodeDto, INotifyPropertyChanged
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

        public bool? StatusWpf
        {
            get { return Status; }
            set
            {
                Status = value;
                OnPropertyChanged();
            }
        }


        public DateTime? TglMulaiDenda1Wpf
        {
            get { return TglMulaiDenda1; }
            set
            {
                TglMulaiDenda1 = value;
                OnPropertyChanged();
            }
        }

        public DateTime? TglMulaiDenda2Wpf
        {
            get { return TglMulaiDenda2; }
            set
            {
                TglMulaiDenda2 = value;
                OnPropertyChanged();
            }
        }

        public DateTime? TglMulaiDenda3Wpf
        {
            get { return TglMulaiDenda3; }
            set
            {
                TglMulaiDenda3 = value;
                OnPropertyChanged();
            }
        }

        public DateTime? TglMulaiDenda4Wpf
        {
            get { return TglMulaiDenda4; }
            set
            {
                TglMulaiDenda4 = value;
                OnPropertyChanged();
            }
        }

        public DateTime? TglMulaiDendaPerBulanWpf
        {
            get { return TglMulaiDendaPerBulan; }
            set
            {
                TglMulaiDendaPerBulan = value;
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
