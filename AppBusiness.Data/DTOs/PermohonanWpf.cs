using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace AppBusiness.Data.DTOs
{
    public class PermohonanWpf : PermohonanDto, INotifyPropertyChanged
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

        public string NomorSambunganWpf
        {
            get
            {
                if (IdGolongan != null)
                {
                    return NoSamb;
                }
                if (IdTarifLimbah != null)
                {
                    return NomorLimbah;
                }
                if (IdTarifLltt != null)
                {
                    return NomorLltt;
                }

                return "-";
            }
        }

        public string TarifGolonganWpf
        {
            get
            {
                if (IdGolongan != null)
                {
                    return NamaGolongan;
                }
                if (IdTarifLimbah != null)
                {
                    return NamaTarifLimbah;
                }
                if (IdTarifLltt != null)
                {
                    return NamaTarifLltt;
                }

                return "-";
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

        public string KeteranganLapanganWpf
        {
            get { return KeteranganLapangan; }
            set
            {
                KeteranganLapangan = value;
                OnPropertyChanged();
            }
        }

        public string NosambYangDiberikanWpf
        {
            get { return NosambYangDiberikan; }
            set
            {
                NosambYangDiberikan = value;
                OnPropertyChanged();
            }
        }

        public List<PermohonanDetailDto> ParameterWpf
        {
            get { return Parameter; }
            set
            {
                Parameter = value;
                OnPropertyChanged();
            }
        }

        public List<PermohonanPetugasDto> PelaksanaWpf
        {
            get { return Pelaksana; }
            set
            {
                Pelaksana = value;
                OnPropertyChanged();
            }
        }

        public PermohonanRabDto RabWpf
        {
            get
            {
                return RAB;
            }
            set
            {
                RAB = value;
                OnPropertyChanged();
            }
        }

        public PermohonanBaDto BeritaAcaraWpf
        {
            get
            {
                return BeritaAcara;
            }
            set
            {
                BeritaAcara = value;
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

        private string _noHpWpf;
        public string NoHpWpf
        {
            get { return _noHpWpf; }
            set
            {
                _noHpWpf = value;
                OnPropertyChanged();
            }
        }

        private string _noTelpWpf;
        public string NoTelpWpf
        {
            get { return _noTelpWpf; }
            set
            {
                _noTelpWpf = value;
                OnPropertyChanged();
            }
        }

        private string _emailWpf;
        public string EmailWpf
        {
            get { return _emailWpf; }
            set
            {
                _emailWpf = value;
                OnPropertyChanged();
            }
        }

        private bool? _isSelected = false;
        public bool? IsSelected
        {
            get { return _isSelected; }
            set
            {
                _isSelected = value;
                OnPropertyChanged();
            }
        }

        public string AlasanBatalWpf
        {
            get
            {
                if (SPK != null)
                    return SPK.AlasanBatal;
                else
                    return null;
            }
            set
            {
                if (SPK != null)
                    SPK.AlasanBatal = value;

                OnPropertyChanged();
            }
        }

        public string StatusPermohonanWpf
        {
            get { return StatusPermohonanText; }
            set
            {
                StatusPermohonanText = value;
                OnPropertyChanged();
            }
        }

        public int? IdTipePendaftaranSambunganWpf
        {
            get { return IdTipePendaftaranSambungan; }
            set
            {
                IdTipePendaftaranSambungan = value;
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

        public int? IdPekerjaanWpf
        {
            get { return IdPekerjaan; }
            set
            {
                IdPekerjaan = value;
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

        public string NoKtpWpf
        {
            get { return NoKtp; }
            set
            {
                NoKtp = value;
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

        public string AlamatPemilikWpf
        {
            get { return AlamatPemilik; }
            set
            {
                AlamatPemilik = value;
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

        public string NosambDepanWpf
        {
            get { return NosambDepan; }
            set
            {
                NosambDepan = value;
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

        public string NamaCabangWpf
        {
            get { return NamaCabang; }
            set
            {
                NamaCabang = value;
                OnPropertyChanged();
            }
        }

        public string NamaKecamatanWpf
        {
            get { return NamaKecamatan; }
            set
            {
                NamaKecamatan = value;
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

        public int? IdWilayahWpf
        {
            get { return IdWilayah; }
            set
            {
                IdWilayah = value;
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

        public int? IdGolonganWpf
        {
            get { return IdGolongan; }
            set
            {
                IdGolongan = value;
                OnPropertyChanged();
            }
        }

        public string KodeGolonganWpf
        {
            get { return KodeGolongan; }
            set
            {
                KodeGolongan = value;
                OnPropertyChanged();
            }
        }

        public string NamaGolonganWpf
        {
            get { return NamaGolongan; }
            set
            {
                NamaGolongan = value;
                OnPropertyChanged();
            }
        }


        public int? IdDiameterWpf
        {
            get { return IdDiameter; }
            set
            {
                IdDiameter = value;
                OnPropertyChanged();
            }
        }

        public string KodeDiameterWpf
        {
            get { return KodeDiameter; }
            set
            {
                KodeDiameter = value;
                OnPropertyChanged();
            }
        }

        public string NamaDiameterWpf
        {
            get { return NamaDiameter; }
            set
            {
                NamaDiameter = value;
                OnPropertyChanged();
            }
        }

        public int? IdAdministrasiLainWpf
        {
            get { return IdAdministrasiLain; }
            set
            {
                IdAdministrasiLain = value;
                OnPropertyChanged();
            }
        }

        public int? IdPemeliharaanLainWpf
        {
            get { return IdPemeliharaanLain; }
            set
            {
                IdPemeliharaanLain = value;
                OnPropertyChanged();
            }
        }

        public int? IdRetribusiLainWpf
        {
            get { return IdRetribusiLain; }
            set
            {
                IdRetribusiLain = value;
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

        public int? IdKepemilikanWpf
        {
            get { return IdKepemilikan; }
            set
            {
                IdKepemilikan = value;
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

        public int? IdSumberAirWpf
        {
            get { return IdSumberAir; }
            set
            {
                IdSumberAir = value;
                OnPropertyChanged();
            }
        }

        public int? IdDmaWpf
        {
            get { return IdDma; }
            set
            {
                IdDma = value;
                OnPropertyChanged();
            }
        }

        public int? IdDmzWpf
        {
            get { return IdDmz; }
            set
            {
                IdDmz = value;
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

        public int? PenghuniWpf
        {
            get { return Penghuni; }
            set
            {
                Penghuni = value;
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

        public decimal? DayaListrikWpf
        {
            get { return DayaListrik; }
            set
            {
                DayaListrik = value;
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
