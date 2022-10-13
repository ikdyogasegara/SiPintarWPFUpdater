using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace AppBusiness.Data.DTOs
{
    public class PermohonanNonPelangganWpf : PermohonanNonPelangganDto, INotifyPropertyChanged
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

        public string FlagUi { get; set; }

        public string KeteranganWpf
        {
            get { return Keterangan; }
            set
            {
                Keterangan = value;
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

        public string NomorPermohonanWpf
        {
            get { return NomorPermohonan; }
            set
            {
                NomorPermohonan = value;
                OnPropertyChanged();
            }
        }

        public DateTime? WaktuPermohonanWpf
        {
            get { return WaktuPermohonan; }
            set
            {
                WaktuPermohonan = value;
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

        public string RtWpf
        {
            get { return Rt; }
            set
            {
                Rt = value;
                OnPropertyChanged();
            }
        }

        public string RwWpf
        {
            get { return Rw; }
            set
            {
                Rw = value;
                OnPropertyChanged();
            }
        }

        public string NoHpWpf
        {
            get { return NoHp; }
            set
            {
                NoHp = value;
                OnPropertyChanged();
            }
        }

        public string NoTelpWpf
        {
            get { return NoTelp; }
            set
            {
                NoTelp = value;
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

        public int? IdBlokWpf
        {
            get { return IdBlok; }
            set
            {
                IdBlok = value;
                OnPropertyChanged();
            }
        }

        public string NamaBlokWpf
        {
            get { return NamaBlok; }
            set
            {
                NamaBlok = value;
                OnPropertyChanged();
            }
        }

        public int? PenghuniWpf
        {
            get { return Penghuni; }
            set
            {
                Penghuni = value;
                OnPropertyChanged();
            }
        }

        public int? IdJenisBangunanWpf
        {
            get { return IdJenisBangunan; }
            set
            {
                IdJenisBangunan = value;
                OnPropertyChanged();
            }
        }

        public string NamaJenisBangunanWpf
        {
            get { return NamaJenisBangunan; }
            set
            {
                NamaJenisBangunan = value;
                OnPropertyChanged();
            }
        }

        public int? IdPekerjaanWpf
        {
            get { return IdPekerjaan; }
            set
            {
                IdPekerjaan = value;
                OnPropertyChanged();
            }
        }

        public string NamaPekerjaanWpf
        {
            get { return NamaPekerjaan; }
            set
            {
                NamaPekerjaan = value;
                OnPropertyChanged();
            }
        }

        public string NosambBelakangWpf
        {
            get { return NosambBelakang; }
            set
            {
                NosambBelakang = value;
                OnPropertyChanged();
            }
        }

        public string NosambDepanWpf
        {
            get { return NosambDepan; }
            set
            {
                NosambDepan = value;
                OnPropertyChanged();
            }
        }

        public string NosambKananWpf
        {
            get { return NosambKanan; }
            set
            {
                NosambKanan = value;
                OnPropertyChanged();
            }
        }

        public string NosambKiriWpf
        {
            get { return NosambKiri; }
            set
            {
                NosambKiri = value;
                OnPropertyChanged();
            }
        }


        public string NoKtpWpf
        {
            get { return NoKtp; }
            set
            {
                NoKtp = value;
                OnPropertyChanged();
            }
        }

        public string NoKkWpf
        {
            get { return NoKk; }
            set
            {
                NoKk = value;
                OnPropertyChanged();
            }
        }

        public string EmailWpf
        {
            get { return Email; }
            set
            {
                Email = value;
                OnPropertyChanged();
            }
        }

        public decimal? LuasTanahWpf
        {
            get { return LuasTanah; }
            set
            {
                LuasTanah = value;
                OnPropertyChanged();
            }
        }

        public decimal? LuasRumahWpf
        {
            get { return LuasRumah; }
            set
            {
                LuasRumah = value;
                OnPropertyChanged();
            }
        }

        public int? IdKepemilikanWpf
        {
            get { return IdKepemilikan; }
            set
            {
                IdKepemilikan = value;
                OnPropertyChanged();
            }
        }

        public string NamaKepemilikanWpf
        {
            get { return NamaKepemilikan; }
            set
            {
                NamaKepemilikan = value;
                OnPropertyChanged();
            }
        }

        public int? IdPeruntukanWpf
        {
            get { return IdPeruntukan; }
            set
            {
                IdPeruntukan = value;
                OnPropertyChanged();
            }
        }

        public string NamaPeruntukanWpf
        {
            get { return NamaPeruntukan; }
            set
            {
                NamaPeruntukan = value;
                OnPropertyChanged();
            }
        }

        public int? IdSumberAirWpf
        {
            get { return IdSumberAir; }
            set
            {
                IdSumberAir = value;
                OnPropertyChanged();
            }
        }

        public string KodePostWpf
        {
            get { return KodePost; }
            set
            {
                KodePost = value;
                OnPropertyChanged();
            }
        }

        public decimal? DayaListrikWpf
        {
            get { return DayaListrik; }
            set
            {
                DayaListrik = value;
                OnPropertyChanged();
            }
        }

        public string NamaPemilikWpf
        {
            get { return NamaPemilik; }
            set
            {
                NamaPemilik = value;
                OnPropertyChanged();
            }
        }

        public string AlamatPemilikWpf
        {
            get { return AlamatPemilik; }
            set
            {
                AlamatPemilik = value;
                OnPropertyChanged();
            }
        }

        public string LatitudeWpf
        {
            get { return Latitude; }
            set
            {
                Latitude = value;
                OnPropertyChanged();
            }
        }

        public string LongitudeWpf
        {
            get { return Longitude; }
            set
            {
                Longitude = value;
                OnPropertyChanged();
            }
        }

        public string AlamatMapWpf
        {
            get { return AlamatMap; }
            set
            {
                AlamatMap = value;
                OnPropertyChanged();
            }
        }

        public string FotoSuratPernyataanWpf
        {
            get { return FotoSuratPernyataan; }
            set
            {
                FotoSuratPernyataan = value;
                OnPropertyChanged();
            }
        }

        public string FotoKkWpf
        {
            get { return FotoKk; }
            set
            {
                FotoKk = value;
                OnPropertyChanged();
            }
        }

        public string FotoKtpWpf
        {
            get { return FotoKtp; }
            set
            {
                FotoKtp = value;
                OnPropertyChanged();
            }
        }

        public string FotoImbWpf
        {
            get { return FotoImb; }
            set
            {
                FotoImb = value;
                OnPropertyChanged();
            }
        }

        public List<PermohonanNonPelangganDetailDto> ParameterWpf
        {
            get { return Parameter; }
            set
            {
                Parameter = value;
                OnPropertyChanged();
            }
        }

        public bool? FlagLanjutKeLanggananLimbahWpf
        {
            get { return FlagLanjutKeLanggananLimbah; }
            set
            {
                FlagLanjutKeLanggananLimbah = value;
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
