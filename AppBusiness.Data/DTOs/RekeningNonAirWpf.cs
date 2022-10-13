using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace AppBusiness.Data.DTOs
{
    public class RekeningNonAirWpf : RekeningNonAirDto, INotifyPropertyChanged
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

        private bool _isSelected = true;
        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                _isSelected = value;
                OnPropertyChanged();
            }
        }

        public string NamaWpf
        {
            get { return Nama; }
            set
            {
                Nama = value;
                OnPropertyChanged();
            }
        }

        public string AlamatWpf
        {
            get { return Alamat; }
            set
            {
                Alamat = value;
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

        public int? IdGolonganWpf
        {
            get { return IdGolongan; }
            set
            {
                IdGolongan = value;
                OnPropertyChanged();
            }
        }

        public int? IdTarifLimbahWpf
        {
            get { return IdTarifLimbah; }
            set
            {
                IdTarifLimbah = value;
                OnPropertyChanged();
            }
        }

        public int? IdTarifLlttWpf
        {
            get { return IdTarifLltt; }
            set
            {
                IdTarifLltt = value;
                OnPropertyChanged();
            }
        }

        public string KodeTarifWpf
        {
            get { return KodeTarif; }
            set
            {
                KodeTarif = value;
                OnPropertyChanged();
            }
        }

        public string NamaTarifWpf
        {
            get { return NamaTarif; }
            set
            {
                NamaTarif = value;
                OnPropertyChanged();
            }
        }

        public int? IdRayonWpf
        {
            get { return IdRayon; }
            set
            {
                IdRayon = value;
                OnPropertyChanged();
            }
        }

        public string KodeRayonWpf
        {
            get { return KodeRayon; }
            set
            {
                KodeRayon = value;
                OnPropertyChanged();
            }
        }

        public string NamaRayonWpf
        {
            get { return NamaRayon; }
            set
            {
                NamaRayon = value;
                OnPropertyChanged();
            }
        }
        public string KodeWilayahWpf
        {
            get { return KodeWilayah; }
            set
            {
                KodeWilayah = value;
                OnPropertyChanged();
            }
        }

        public string NamaWilayahWpf
        {
            get { return NamaWilayah; }
            set
            {
                NamaWilayah = value;
                OnPropertyChanged();
            }
        }

        public int? IdKelurahanWpf
        {
            get { return IdKelurahan; }
            set
            {
                IdKelurahan = value;
                OnPropertyChanged();
            }
        }

        public string KodeKelurahanWpf
        {
            get { return KodeKelurahan; }
            set
            {
                KodeKelurahan = value;
                OnPropertyChanged();
            }
        }

        public string NamaKelurahanWpf
        {
            get { return NamaKelurahan; }
            set
            {
                NamaKelurahan = value;
                OnPropertyChanged();
            }
        }

        public DateTime? TanggalMulaiTagihWpf
        {
            get { return TanggalMulaiTagih; }
            set
            {
                TanggalMulaiTagih = value;
                OnPropertyChanged();
            }
        }

        public DateTime? WaktuTransaksiWpf
        {
            get
            {
                if (StatusTransaksi == true)
                    return WaktuTransaksi;
                else
                {
                    return null;
                }
            }
        }

        public bool? StatusTransaksiWpf
        {
            get
            {
                if (StatusTransaksi == true)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public string StatusTransaksiWpfText
        {
            get
            {
                if (StatusTransaksi == true)
                {
                    return "Lunas";
                }
                else
                {
                    return "Belum Lunas";
                }
            }
        }

        public List<RekeningNonAirDetailDto> DetailWpf
        {
            get { return Detail; }
            set
            {
                Detail = value;
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
