using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using AppBusiness.Data.Helpers;

namespace AppBusiness.Data.DTOs
{
    public class PermohonanKoreksiRekeningWpf : PermohonanPelangganAirKoreksiRekeningDto, INotifyPropertyChanged
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

        public string StatusVerifikasiTextWpf
        {
            get { return StatusVerifikasiText; }
            set
            {
                StatusVerifikasiText = value;
                OnPropertyChanged();
            }
        }

        public int? StatusVerifikasiLapanganWpf
        {
            get { return StatusVerifikasiLapangan; }
            set
            {
                StatusVerifikasiLapangan = value;
                OnPropertyChanged();

                StatusVerifikasiTextWpf = AppExtensionsHelpers.GetStatusVerifikasiUsulanKoreksiText(new PermohonanPelangganAirKoreksiRekening()
                {
                    StatusVerifikasiLapangan = StatusVerifikasiLapanganWpf,
                    StatusVerifikasiPusat = StatusVerifikasiPusatWpf
                });
            }
        }

        public string KeteranganStatusVerifikasiLapanganWpf
        {
            get { return KeteranganStatusVerifikasiLapangan; }
            set
            {
                KeteranganStatusVerifikasiLapangan = value;
                OnPropertyChanged();
            }
        }

        public DateTime? WaktuStatusVerifikasiLapanganWpf
        {
            get { return WaktuStatusVerifikasiLapangan; }
            set
            {
                WaktuStatusVerifikasiLapangan = value;
                OnPropertyChanged();
            }
        }

        public int? StatusVerifikasiPusatWpf
        {
            get { return StatusVerifikasiPusat; }
            set
            {
                StatusVerifikasiPusat = value;
                OnPropertyChanged();

                StatusVerifikasiTextWpf = AppExtensionsHelpers.GetStatusVerifikasiUsulanKoreksiText(new PermohonanPelangganAirKoreksiRekening()
                {
                    StatusVerifikasiLapangan = StatusVerifikasiLapanganWpf,
                    StatusVerifikasiPusat = StatusVerifikasiPusatWpf
                });
            }
        }

        public string KeteranganStatusVerifikasiPusatWpf
        {
            get { return KeteranganStatusVerifikasiPusat; }
            set
            {
                KeteranganStatusVerifikasiPusat = value;
                OnPropertyChanged();
            }
        }

        public DateTime? WaktuStatusVerifikasiPusatWpf
        {
            get { return WaktuStatusVerifikasiPusat; }
            set
            {
                WaktuStatusVerifikasiPusat = value;
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
