using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace AppBusiness.Data.DTOs
{
    public class RekeningAirPiutangWpf : RekeningAirPiutangDto, INotifyPropertyChanged
    {
        public bool IsSelected { get; set; }

        public bool IsSelectedWpf
        {
            get { return IsSelected; }
            set
            {
                IsSelected = value;
                OnPropertyChanged();
            }
        }

        public bool IsInputKoreksi { get; set; }

        public bool IsInputKoreksiWpf
        {
            get { return IsInputKoreksi; }
            set
            {
                IsInputKoreksi = value;
                OnPropertyChanged();
            }
        }

        public decimal? StanLalu_Usulan { get; set; } = 0;

        public decimal? StanLalu_UsulanWpf
        {
            get { return StanLalu_Usulan; }
            set
            {
                StanLalu_Usulan = value;
                OnPropertyChanged();
            }
        }

        public decimal? StanSkrg_Usulan { get; set; } = 0;

        public decimal? StanSkrg_UsulanWpf
        {
            get { return StanSkrg_Usulan; }
            set
            {
                StanSkrg_Usulan = value;
                OnPropertyChanged();
            }
        }

        public bool FlagHanyaAbonemen { get; set; }

        public bool FlagHanyaAbonemenWpf
        {
            get { return FlagHanyaAbonemen; }
            set
            {
                FlagHanyaAbonemen = value;
                OnPropertyChanged();
            }
        }

        public decimal? StanAngkat_Usulan { get; set; } = 0;

        public decimal? StanAngkat_UsulanWpf
        {
            get { return StanAngkat_Usulan; }
            set
            {
                StanAngkat_Usulan = value;
                OnPropertyChanged();
            }
        }

        public decimal? Pakai_Usulan { get; set; } = 0;

        public decimal? Pakai_UsulanWpf
        {
            get { return Pakai_Usulan; }
            set
            {
                Pakai_Usulan = value;
                OnPropertyChanged();
            }
        }

        public decimal? BiayaPemakaian_Usulan { get; set; } = 0;

        public decimal? BiayaPemakaian_UsulanWpf
        {
            get { return BiayaPemakaian_Usulan; }
            set
            {
                BiayaPemakaian_Usulan = value;
                OnPropertyChanged();
            }
        }

        public decimal? Administrasi_Usulan { get; set; } = 0;

        public decimal? Administrasi_UsulanWpf
        {
            get { return Administrasi_Usulan; }
            set
            {
                Administrasi_Usulan = value;
                OnPropertyChanged();
            }
        }

        public decimal? Pemeliharaan_Usulan { get; set; } = 0;

        public decimal? Pemeliharaan_UsulanWpf
        {
            get { return Pemeliharaan_Usulan; }
            set
            {
                Pemeliharaan_Usulan = value;
                OnPropertyChanged();
            }
        }

        public decimal? Retribusi_Usulan { get; set; } = 0;

        public decimal? Retribusi_UsulanWpf
        {
            get { return Retribusi_Usulan; }
            set
            {
                Retribusi_Usulan = value;
                OnPropertyChanged();
            }
        }

        public decimal? Pelayanan_Usulan { get; set; } = 0;

        public decimal? Pelayanan_UsulanWpf
        {
            get { return Pelayanan_Usulan; }
            set
            {
                Pelayanan_Usulan = value;
                OnPropertyChanged();
            }
        }

        public decimal? AirLimbah_Usulan { get; set; } = 0;

        public decimal? AirLimbah_UsulanWpf
        {
            get { return AirLimbah_Usulan; }
            set
            {
                AirLimbah_Usulan = value;
                OnPropertyChanged();
            }
        }

        public decimal? DendaPakai0_Usulan { get; set; } = 0;

        public decimal? DendaPakai0_UsulanWpf
        {
            get { return DendaPakai0_Usulan; }
            set
            {
                DendaPakai0_Usulan = value;
                OnPropertyChanged();
            }
        }

        public decimal? AdministrasiLain_Usulan { get; set; } = 0;

        public decimal? AdministrasiLain_UsulanWpf
        {
            get { return AdministrasiLain_Usulan; }
            set
            {
                AdministrasiLain_Usulan = value;
                OnPropertyChanged();
            }
        }

        public decimal? PemeliharaanLain_Usulan { get; set; } = 0;

        public decimal? PemeliharaanLain_UsulanWpf
        {
            get { return PemeliharaanLain_Usulan; }
            set
            {
                PemeliharaanLain_Usulan = value;
                OnPropertyChanged();
            }
        }

        public decimal? RetribusiLain_Usulan { get; set; } = 0;

        public decimal? RetribusiLain_UsulanWpf
        {
            get { return RetribusiLain_Usulan; }
            set
            {
                RetribusiLain_Usulan = value;
                OnPropertyChanged();
            }
        }

        public decimal? Meterai_Usulan { get; set; } = 0;

        public decimal? Meterai_UsulanWpf
        {
            get { return Meterai_Usulan; }
            set
            {
                Meterai_Usulan = value;
                OnPropertyChanged();
            }
        }

        public decimal? Ppn_Usulan { get; set; } = 0;

        public decimal? Ppn_UsulanWpf
        {
            get { return Ppn_Usulan; }
            set
            {
                Ppn_Usulan = value;
                OnPropertyChanged();
            }
        }

        public decimal? Rekair_Usulan { get; set; } = 0;

        public decimal? Rekair_UsulanWpf
        {
            get { return Rekair_Usulan; }
            set
            {
                Rekair_Usulan = value;
                OnPropertyChanged();
            }
        }

        public decimal? Denda_Usulan { get; set; } = 0;

        public decimal? Denda_UsulanWpf
        {
            get { return Denda_Usulan; }
            set
            {
                Denda_Usulan = value;
                OnPropertyChanged();
            }
        }

        public decimal? Total_Usulan { get; set; } = 0;

        public decimal? Total_UsulanWpf
        {
            get { return Total_Usulan; }
            set
            {
                Total_Usulan = value;
                OnPropertyChanged();
            }
        }

        private Uri _fotoBukti1Uri;
        public Uri FotoBukti1Uri
        {
            get => _fotoBukti1Uri;
            set
            {
                _fotoBukti1Uri = value;
                OnPropertyChanged();
            }
        }

        private bool _isNewFotoBukti1;
        public bool IsNewFotoBukti1
        {
            get => _isNewFotoBukti1;
            set
            {
                _isNewFotoBukti1 = value;
                OnPropertyChanged();
            }
        }

        private bool _isFotoBukti1FormChecked;
        public bool IsFotoBukti1FormChecked
        {
            get => _isFotoBukti1FormChecked;
            set
            {
                _isFotoBukti1FormChecked = value;
                OnPropertyChanged();
            }
        }

        private Uri _fotoBukti2Uri;
        public Uri FotoBukti2Uri
        {
            get => _fotoBukti2Uri;
            set
            {
                _fotoBukti2Uri = value;
                OnPropertyChanged();
            }
        }

        private bool _isNewFotoBukti2;
        public bool IsNewFotoBukti2
        {
            get => _isNewFotoBukti2;
            set
            {
                _isNewFotoBukti2 = value;
                OnPropertyChanged();
            }
        }

        private bool _isFotoBukti2FormChecked;
        public bool IsFotoBukti2FormChecked
        {
            get => _isFotoBukti2FormChecked;
            set
            {
                _isFotoBukti2FormChecked = value;
                OnPropertyChanged();
            }
        }

        private Uri _fotoBukti3Uri;
        public Uri FotoBukti3Uri
        {
            get => _fotoBukti3Uri;
            set
            {
                _fotoBukti3Uri = value;
                OnPropertyChanged();
            }
        }

        private bool _isNewFotoBukti3;
        public bool IsNewFotoBukti3
        {
            get => _isNewFotoBukti3;
            set
            {
                _isNewFotoBukti3 = value;
                OnPropertyChanged();
            }
        }

        private bool _isFotoBukti3FormChecked;
        public bool IsFotoBukti3FormChecked
        {
            get => _isFotoBukti3FormChecked;
            set
            {
                _isFotoBukti3FormChecked = value;
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
