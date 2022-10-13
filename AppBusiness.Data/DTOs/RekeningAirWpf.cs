using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace AppBusiness.Data.DTOs
{
    public class RekeningAirWpf : RekeningAirDto, INotifyPropertyChanged
    {
        public bool IsAktif { get; set; } = true;
        public bool IsVerifikasi { get; set; }
        public bool IsUnverifikasi { get; set; } = true;

        private bool _isUpdatingVerifikasi;

        public bool IsUpdatingVerifikasi
        {
            get { return _isUpdatingVerifikasi; }
            set
            {
                _isUpdatingVerifikasi = value;
                OnPropertyChanged();
            }
        }

        public bool? FlagVerifikasiWpf
        {
            get { return FlagVerifikasi; }
            set
            {
                FlagVerifikasi = value;
                OnPropertyChanged();
            }
        }
        public DateTime? WaktuVerifikasiWpf
        {
            get { return WaktuVerifikasi; }
            set
            {
                WaktuVerifikasi = value;
                OnPropertyChanged();
            }
        }

        private bool _isUpdatingPublish;
        public bool IsUpdatingPublish
        {
            get { return _isUpdatingPublish; }
            set
            {
                _isUpdatingPublish = value;
                OnPropertyChanged();
            }
        }

        public bool? FlagBacaWpf
        {
            get { return FlagBaca; }
            set
            {
                FlagBaca = value;
                OnPropertyChanged();
            }
        }

        public int? FlagRequestBacaUlangWpf
        {
            get { return FlagRequestBacaUlang; }
            set
            {
                FlagRequestBacaUlang = value;
                OnPropertyChanged();
            }
        }

        public bool? FlagPublishWpf
        {
            get { return FlagPublish; }
            set
            {
                FlagPublish = value;
                OnPropertyChanged();
            }
        }

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

        public decimal? StanLaluWpf
        {
            get { return StanLalu; }
            set
            {
                StanLalu = value;
                OnPropertyChanged();
            }
        }

        public decimal? StanSkrgWpf
        {
            get { return StanSkrg; }
            set
            {
                StanSkrg = value;
                OnPropertyChanged();
            }
        }

        public decimal? StanAngkatWpf
        {
            get { return StanAngkat; }
            set
            {
                StanAngkat = value;
                OnPropertyChanged();
            }
        }

        public decimal? PakaiWpf
        {
            get { return Pakai; }
            set
            {
                Pakai = value;
                OnPropertyChanged();
            }
        }

        public decimal? PakaiKalkulasiWpf
        {
            get { return PakaiKalkulasi; }
            set
            {
                PakaiKalkulasi = value;
                OnPropertyChanged();
            }
        }

        public decimal? BiayaPemakaianWpf
        {
            get { return BiayaPemakaian; }
            set
            {
                BiayaPemakaian = value;
                OnPropertyChanged();
            }
        }

        public decimal? AdministrasiWpf
        {
            get { return Administrasi; }
            set
            {
                Administrasi = value;
                OnPropertyChanged();
            }
        }

        public decimal? PemeliharaanWpf
        {
            get { return Pemeliharaan; }
            set
            {
                Pemeliharaan = value;
                OnPropertyChanged();
            }
        }

        public decimal? RetribusiWpf
        {
            get { return Retribusi; }
            set
            {
                Retribusi = value;
                OnPropertyChanged();
            }
        }

        public decimal? PelayananWpf
        {
            get { return Pelayanan; }
            set
            {
                Pelayanan = value;
                OnPropertyChanged();
            }
        }

        public decimal? AirLimbahWpf
        {
            get { return AirLimbah; }
            set
            {
                AirLimbah = value;
                OnPropertyChanged();
            }
        }

        public decimal? DendaPakai0Wpf
        {
            get { return DendaPakai0; }
            set
            {
                DendaPakai0 = value;
                OnPropertyChanged();
            }
        }

        public decimal? AdministrasiLainWpf
        {
            get { return AdministrasiLain; }
            set
            {
                AdministrasiLain = value;
                OnPropertyChanged();
            }
        }

        public decimal? PemeliharaanLainWpf
        {
            get { return PemeliharaanLain; }
            set
            {
                PemeliharaanLain = value;
                OnPropertyChanged();
            }
        }

        public decimal? RetribusiLainWpf
        {
            get { return RetribusiLain; }
            set
            {
                RetribusiLain = value;
                OnPropertyChanged();
            }
        }

        public decimal? PpnWpf
        {
            get { return Ppn; }
            set
            {
                Ppn = value;
                OnPropertyChanged();
            }
        }

        public decimal? MeteraiWpf
        {
            get { return Meterai; }
            set
            {
                Meterai = value;
                OnPropertyChanged();
            }
        }

        public decimal? RekairWpf
        {
            get { return Rekair; }
            set
            {
                Rekair = value;
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

        public string PetugasBacaWpf
        {
            get { return PetugasBaca; }
            set
            {
                PetugasBaca = value;
                OnPropertyChanged();
            }
        }

        public string KelainanWpf
        {
            get { return Kelainan; }
            set
            {
                Kelainan = value;
                OnPropertyChanged();
            }
        }

        public bool? TaksirWpf
        {
            get { return Taksir; }
            set
            {
                Taksir = value;
                OnPropertyChanged();
            }
        }

        public bool? TaksasiWpf
        {
            get { return Taksasi; }
            set
            {
                Taksasi = value;
                OnPropertyChanged();
            }
        }

        public bool? FlagKoreksiWpf
        {
            get { return FlagKoreksi; }
            set
            {
                FlagKoreksi = value;
                OnPropertyChanged();
            }
        }

        public DateTime? WaktuKoreksiWpf
        {
            get { return WaktuKoreksi; }
            set
            {
                WaktuKoreksi = value;
                OnPropertyChanged();
            }
        }

        public bool? FlagUploadWpf
        {
            get { return FlagUpload; }
            set
            {
                FlagUpload = value;
                OnPropertyChanged();
            }
        }

        public DateTime? WaktuUploadWpf
        {
            get { return WaktuUpload; }
            set
            {
                WaktuUpload = value;
                OnPropertyChanged();
            }
        }

        public DateTime? WaktuPublishWpf
        {
            get { return WaktuPublish; }
            set
            {
                WaktuPublish = value;
                OnPropertyChanged();
            }
        }

        public string LampiranWpf
        {
            get { return Lampiran; }
            set
            {
                Lampiran = value;
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
