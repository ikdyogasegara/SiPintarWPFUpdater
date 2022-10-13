using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace AppBusiness.Data.DTOs
{
    public class MasterPelangganGlobalWpf : MasterPelangganGlobalDto, INotifyPropertyChanged
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
            get
            {
                if (MasterPelangganAirDetail is null)
                {
                    return Keterangan;
                }
                else
                {
                    return MasterPelangganAirDetail.Keterangan;
                }
            }
            set
            {
                if (MasterPelangganAirDetail is null)
                {
                    Keterangan = value;
                }
                else
                {
                    MasterPelangganAirDetail.Keterangan = value;
                }

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

        public string NoHpWpf
        {
            get
            {
                if (MasterPelangganAirDetail is null)
                {
                    return NoHp;
                }
                else
                {
                    return MasterPelangganAirDetail.NoHp;
                }
            }
            set
            {
                if (MasterPelangganAirDetail is null)
                {
                    NoHp = value;
                }
                else
                {
                    MasterPelangganAirDetail.NoHp = value;
                }

                OnPropertyChanged();
            }
        }

        public string NoTelpWpf
        {
            get
            {
                if (MasterPelangganAirDetail is null)
                {
                    return NoTelp;
                }
                else
                {
                    return MasterPelangganAirDetail.NoTelp;
                }
            }
            set
            {
                if (MasterPelangganAirDetail is null)
                {
                    NoTelp = value;
                }
                else
                {
                    MasterPelangganAirDetail.NoTelp = value;
                }

                OnPropertyChanged();
            }
        }

        public string NoKtpWpf
        {
            get
            {
                if (MasterPelangganAirDetail is null)
                {
                    return NoKtp;
                }
                else
                {
                    return MasterPelangganAirDetail.NoKtp;
                }
            }
            set
            {
                if (MasterPelangganAirDetail is null)
                {
                    NoKtp = value;
                }
                else
                {
                    MasterPelangganAirDetail.NoKtp = value;
                }

                OnPropertyChanged();
            }
        }

        public string NoKkWpf
        {
            get
            {
                if (MasterPelangganAirDetail != null)
                {
                    return MasterPelangganAirDetail.NoKk;
                }
                return null;
            }

            set
            {
                if (MasterPelangganAirDetail != null)
                {
                    MasterPelangganAirDetail.NoKk = value;
                    OnPropertyChanged();
                }
            }
        }

        public string EmailWpf
        {
            get
            {
                if (MasterPelangganAirDetail is null)
                {
                    return Email;
                }
                else
                {
                    return MasterPelangganAirDetail.Email;
                }
            }
            set
            {
                if (MasterPelangganAirDetail is null)
                {
                    Email = value;
                }
                else
                {
                    MasterPelangganAirDetail.Email = value;
                }

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

        public int? IdTarifLimbahWpf
        {
            get { return IdTarifLimbah; }
            set
            {
                IdTarifLimbah = value;
                OnPropertyChanged();
            }
        }

        public string KodeTarifLimbahWpf
        {
            get { return KodeTarifLimbah; }
            set
            {
                KodeTarifLimbah = value;
                OnPropertyChanged();
            }
        }

        public string NamaTarifLimbahWpf
        {
            get { return NamaTarifLimbah; }
            set
            {
                NamaTarifLimbah = value;
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

        public string KodeTarifLlttWpf
        {
            get { return KodeTarifLltt; }
            set
            {
                KodeTarifLltt = value;
                OnPropertyChanged();
            }
        }

        public string NamaTarifLlttWpf
        {
            get { return NamaTarifLltt; }
            set
            {
                NamaTarifLltt = value;
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

        public int? IdAreaWpf
        {
            get { return IdArea; }
            set
            {
                IdArea = value;
                OnPropertyChanged();
            }
        }

        public string KodeAreaWpf
        {
            get { return KodeArea; }
            set
            {
                KodeArea = value;
                OnPropertyChanged();
            }
        }

        public string NamaAreaWpf
        {
            get { return NamaArea; }
            set
            {
                NamaArea = value;
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

        public int? IdKecamatanWpf
        {
            get { return IdKecamatan; }
            set
            {
                IdKecamatan = value;
                OnPropertyChanged();
            }
        }
        public string KodeKecamatanWpf
        {
            get { return KodeKecamatan; }
            set
            {
                KodeKecamatan = value;
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

        public int? IdCabangWpf
        {
            get { return IdCabang; }
            set
            {
                IdCabang = value;
                OnPropertyChanged();
            }
        }
        public string KodeCabangWpf
        {
            get { return KodeCabang; }
            set
            {
                KodeCabang = value;
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

        public int? IdSumberAirWpf
        {
            get
            {
                if (MasterPelangganAirDetail != null)
                {
                    return MasterPelangganAirDetail.IdSumberAir;
                }

                return null;
            }

            set
            {
                if (MasterPelangganAirDetail != null)
                {
                    MasterPelangganAirDetail.IdSumberAir = value;
                    OnPropertyChanged();
                }
            }
        }

        public string KodeSumberAirWpf
        {
            get
            {
                if (MasterPelangganAirDetail != null)
                {
                    return MasterPelangganAirDetail.KodeSumberAir;
                }
                return null;
            }

            set
            {
                if (MasterPelangganAirDetail != null)
                {
                    MasterPelangganAirDetail.KodeSumberAir = value;
                    OnPropertyChanged();
                }
            }
        }

        public string NamaSumberAirWpf
        {
            get
            {
                if (MasterPelangganAirDetail != null)
                {
                    return MasterPelangganAirDetail.NamaSumberAir;
                }
                return null;
            }

            set
            {
                if (MasterPelangganAirDetail != null)
                {
                    MasterPelangganAirDetail.NamaSumberAir = value;
                    OnPropertyChanged();
                }
            }
        }

        public int? IdAdministrasiLainWpf
        {
            get
            {
                if (MasterPelangganAirDetail != null)
                {
                    return MasterPelangganAirDetail.IdAdministrasiLain;
                }
                return null;
            }

            set
            {
                if (MasterPelangganAirDetail != null)
                {
                    MasterPelangganAirDetail.IdAdministrasiLain = value;
                    OnPropertyChanged();
                }
            }
        }
        public string KodeAdministrasiLainWpf
        {
            get
            {
                if (MasterPelangganAirDetail != null)
                {
                    return MasterPelangganAirDetail.KodeAdministrasiLain;
                }
                return null;
            }

            set
            {
                if (MasterPelangganAirDetail != null)
                {
                    MasterPelangganAirDetail.KodeAdministrasiLain = value;
                    OnPropertyChanged();
                }
            }
        }

        public string NamaAdministrasiLainWpf
        {
            get
            {
                if (MasterPelangganAirDetail != null)
                {
                    return MasterPelangganAirDetail.NamaAdministrasiLain;
                }
                return null;
            }

            set
            {
                if (MasterPelangganAirDetail != null)
                {
                    MasterPelangganAirDetail.NamaAdministrasiLain = value;
                    OnPropertyChanged();
                }
            }
        }

        public int? IdPemeliharaanLainWpf
        {
            get
            {
                if (MasterPelangganAirDetail != null)
                {
                    return MasterPelangganAirDetail.IdPemeliharaanLain;
                }
                return null;
            }

            set
            {
                if (MasterPelangganAirDetail != null)
                {
                    MasterPelangganAirDetail.IdPemeliharaanLain = value;
                    OnPropertyChanged();
                }
            }
        }
        public string KodePemeliharaanLainWpf
        {
            get
            {
                if (MasterPelangganAirDetail != null)
                {
                    return MasterPelangganAirDetail.KodePemeliharaanLain;
                }
                return null;
            }

            set
            {
                if (MasterPelangganAirDetail != null)
                {
                    MasterPelangganAirDetail.KodePemeliharaanLain = value;
                    OnPropertyChanged();
                }
            }
        }

        public string NamaPemeliharaanLainWpf
        {
            get
            {
                if (MasterPelangganAirDetail != null)
                {
                    return MasterPelangganAirDetail.NamaPemeliharaanLain;
                }
                return null;
            }

            set
            {
                if (MasterPelangganAirDetail != null)
                {
                    MasterPelangganAirDetail.NamaPemeliharaanLain = value;
                    OnPropertyChanged();
                }
            }
        }

        public int? IdRetribusiLainWpf
        {
            get
            {
                if (MasterPelangganAirDetail != null)
                {
                    return MasterPelangganAirDetail.IdRetribusiLain;
                }
                return null;
            }

            set
            {
                if (MasterPelangganAirDetail != null)
                {
                    MasterPelangganAirDetail.IdRetribusiLain = value;
                    OnPropertyChanged();
                }
            }
        }
        public string KodeRetribusiLainWpf
        {
            get
            {
                if (MasterPelangganAirDetail != null)
                {
                    return MasterPelangganAirDetail.KodeRetribusiLain;
                }
                return null;
            }

            set
            {
                if (MasterPelangganAirDetail != null)
                {
                    MasterPelangganAirDetail.KodeRetribusiLain = value;
                    OnPropertyChanged();
                }
            }
        }

        public string NamaRetribusiLainWpf
        {
            get
            {
                if (MasterPelangganAirDetail != null)
                {
                    return MasterPelangganAirDetail.NamaRetribusiLain;
                }
                return null;
            }

            set
            {
                if (MasterPelangganAirDetail != null)
                {
                    MasterPelangganAirDetail.NamaRetribusiLain = value;
                    OnPropertyChanged();
                }
            }
        }

        public int? IdStatusWpf
        {
            get { return IdStatus; }
            set
            {
                IdStatus = value;
                OnPropertyChanged();
            }
        }

        public string NamaStatusWpf
        {
            get { return NamaStatus; }
            set
            {
                NamaStatus = value;
                OnPropertyChanged();
            }
        }

        public int? IdFlagWpf
        {
            get { return IdFlag; }
            set
            {
                IdFlag = value;
                OnPropertyChanged();
            }
        }

        public string NamaFlagWpf
        {
            get { return NamaFlag; }
            set
            {
                NamaFlag = value;
                OnPropertyChanged();
            }
        }


        public int? IdKolektifWpf
        {
            get { return IdKolektif; }
            set
            {
                IdKolektif = value;
                OnPropertyChanged();
            }
        }
        public string KodeKolektifWpf
        {
            get { return KodeKolektif; }
            set
            {
                KodeKolektif = value;
                OnPropertyChanged();
            }
        }

        public string NamaKolektifWpf
        {
            get { return NamaKolektif; }
            set
            {
                NamaKolektif = value;
                OnPropertyChanged();
            }
        }

        public int? IdKepemilikanWpf
        {
            get
            {
                if (MasterPelangganAirDetail != null)
                {
                    return MasterPelangganAirDetail.IdKepemilikan;
                }
                return null;
            }

            set
            {
                if (MasterPelangganAirDetail != null)
                {
                    MasterPelangganAirDetail.IdKepemilikan = value;
                    OnPropertyChanged();
                }
            }
        }

        public string NamaKepemilikanWpf
        {
            get
            {
                if (MasterPelangganAirDetail != null)
                {
                    return MasterPelangganAirDetail.NamaKepemilikan;
                }
                return null;
            }

            set
            {
                if (MasterPelangganAirDetail != null)
                {
                    MasterPelangganAirDetail.NamaKepemilikan = value;
                    OnPropertyChanged();
                }
            }
        }

        public int? IdBlokWpf
        {
            get
            {
                if (MasterPelangganAirDetail != null)
                {
                    return MasterPelangganAirDetail.IdBlok;
                }
                return null;
            }

            set
            {
                if (MasterPelangganAirDetail != null)
                {
                    MasterPelangganAirDetail.IdBlok = value;
                    OnPropertyChanged();
                }
            }
        }

        public string NamaBlokWpf
        {
            get
            {
                if (MasterPelangganAirDetail != null)
                {
                    return MasterPelangganAirDetail.NamaBlok;
                }
                return null;
            }

            set
            {
                if (MasterPelangganAirDetail != null)
                {
                    MasterPelangganAirDetail.NamaBlok = value;
                    OnPropertyChanged();
                }
            }
        }

        public int? PenghuniWpf
        {
            get
            {
                if (MasterPelangganAirDetail != null)
                {
                    return MasterPelangganAirDetail.Penghuni;
                }
                return null;
            }

            set
            {
                if (MasterPelangganAirDetail != null)
                {
                    MasterPelangganAirDetail.Penghuni = value;
                    OnPropertyChanged();
                }
            }
        }

        public int? IdJenisBangunanWpf
        {
            get
            {
                if (MasterPelangganAirDetail != null)
                {
                    return MasterPelangganAirDetail.IdJenisBangunan;
                }
                return null;
            }

            set
            {
                if (MasterPelangganAirDetail != null)
                {
                    MasterPelangganAirDetail.IdJenisBangunan = value;
                    OnPropertyChanged();
                }
            }
        }

        public string NamaJenisBangunanWpf
        {
            get
            {
                if (MasterPelangganAirDetail != null)
                {
                    return MasterPelangganAirDetail.NamaJenisBangunan;
                }
                return null;
            }

            set
            {
                if (MasterPelangganAirDetail != null)
                {
                    MasterPelangganAirDetail.NamaJenisBangunan = value;
                    OnPropertyChanged();
                }
            }
        }

        public int? IdPekerjaanWpf
        {
            get
            {
                if (MasterPelangganAirDetail != null)
                {
                    return MasterPelangganAirDetail.IdPekerjaan;
                }
                return null;
            }

            set
            {
                if (MasterPelangganAirDetail != null)
                {
                    MasterPelangganAirDetail.IdPekerjaan = value;
                    OnPropertyChanged();
                }
            }
        }

        public string NamaPekerjaanWpf
        {
            get
            {
                if (MasterPelangganAirDetail != null)
                {
                    return MasterPelangganAirDetail.NamaPekerjaan;
                }
                return null;
            }

            set
            {
                if (MasterPelangganAirDetail != null)
                {
                    MasterPelangganAirDetail.NamaPekerjaan = value;
                    OnPropertyChanged();
                }
            }
        }

        public int? IdPeruntukanWpf
        {
            get
            {
                if (MasterPelangganAirDetail != null)
                {
                    return MasterPelangganAirDetail.IdPeruntukan;
                }
                return null;
            }

            set
            {
                if (MasterPelangganAirDetail != null)
                {
                    MasterPelangganAirDetail.IdPeruntukan = value;
                    OnPropertyChanged();
                }
            }
        }

        public string NamaPeruntukanWpf
        {
            get
            {
                if (MasterPelangganAirDetail != null)
                {
                    return MasterPelangganAirDetail.NamaPeruntukan;
                }
                return null;
            }

            set
            {
                if (MasterPelangganAirDetail != null)
                {
                    MasterPelangganAirDetail.NamaPeruntukan = value;
                    OnPropertyChanged();
                }
            }
        }

        public decimal? DayaListrikWpf
        {
            get
            {
                if (MasterPelangganAirDetail != null)
                {
                    return MasterPelangganAirDetail.DayaListrik;
                }
                return null;
            }

            set
            {
                if (MasterPelangganAirDetail != null)
                {
                    MasterPelangganAirDetail.DayaListrik = value;
                    OnPropertyChanged();
                }
            }
        }

        public decimal? LuasTanahWpf
        {
            get
            {
                if (MasterPelangganAirDetail != null)
                {
                    return MasterPelangganAirDetail.LuasTanah;
                }
                return null;
            }

            set
            {
                if (MasterPelangganAirDetail != null)
                {
                    MasterPelangganAirDetail.LuasTanah = value;
                    OnPropertyChanged();
                }
            }
        }

        public decimal? LuasRumahWpf
        {
            get
            {
                if (MasterPelangganAirDetail != null)
                {
                    return MasterPelangganAirDetail.LuasRumah;
                }
                return null;
            }

            set
            {
                if (MasterPelangganAirDetail != null)
                {
                    MasterPelangganAirDetail.LuasRumah = value;
                    OnPropertyChanged();
                }
            }
        }

        public int? UrutanBacaWpf
        {
            get
            {
                if (MasterPelangganAirDetail != null)
                {
                    return MasterPelangganAirDetail.UrutanBaca;
                }
                return null;
            }

            set
            {
                if (MasterPelangganAirDetail != null)
                {
                    MasterPelangganAirDetail.UrutanBaca = value;
                    OnPropertyChanged();
                }
            }
        }

        public string KodePostWpf
        {
            get
            {
                if (MasterPelangganAirDetail != null)
                {
                    return MasterPelangganAirDetail.KodePost;
                }
                return null;
            }

            set
            {
                if (MasterPelangganAirDetail != null)
                {
                    MasterPelangganAirDetail.KodePost = value;
                    OnPropertyChanged();
                }
            }
        }

        public string NoSeriMeterWpf
        {
            get
            {
                if (MasterPelangganAirDetail != null)
                {
                    return MasterPelangganAirDetail.NoSeriMeter;
                }
                return null;
            }

            set
            {
                if (MasterPelangganAirDetail != null)
                {
                    MasterPelangganAirDetail.NoSeriMeter = value;
                    OnPropertyChanged();
                }
            }
        }

        public int? IdMerekMeterWpf
        {
            get
            {
                if (MasterPelangganAirDetail != null)
                {
                    return MasterPelangganAirDetail.IdMerekMeter;
                }
                return null;
            }

            set
            {
                if (MasterPelangganAirDetail != null)
                {
                    MasterPelangganAirDetail.IdMerekMeter = value;
                    OnPropertyChanged();
                }
            }
        }
        public string KodeMerekMeterWpf
        {
            get
            {
                if (MasterPelangganAirDetail != null)
                {
                    return MasterPelangganAirDetail.KodeMerekMeter;
                }
                return null;
            }

            set
            {
                if (MasterPelangganAirDetail != null)
                {
                    MasterPelangganAirDetail.KodeMerekMeter = value;
                    OnPropertyChanged();
                }
            }
        }

        public string NamaMerekMeterWpf
        {
            get
            {
                if (MasterPelangganAirDetail != null)
                {
                    return MasterPelangganAirDetail.NamaMerekMeter;
                }
                return null;
            }

            set
            {
                if (MasterPelangganAirDetail != null)
                {
                    MasterPelangganAirDetail.NamaMerekMeter = value;
                    OnPropertyChanged();
                }
            }
        }

        public int? IdKondisiMeterWpf
        {
            get
            {
                if (MasterPelangganAirDetail != null)
                {
                    return MasterPelangganAirDetail.IdKondisiMeter;
                }
                return null;
            }

            set
            {
                if (MasterPelangganAirDetail != null)
                {
                    MasterPelangganAirDetail.IdKondisiMeter = value;
                    OnPropertyChanged();
                }
            }
        }
        public string KodeKondisiMeterWpf
        {
            get
            {
                if (MasterPelangganAirDetail != null)
                {
                    return MasterPelangganAirDetail.KodeKondisiMeter;
                }
                return null;
            }

            set
            {
                if (MasterPelangganAirDetail != null)
                {
                    MasterPelangganAirDetail.KodeKondisiMeter = value;
                    OnPropertyChanged();
                }
            }
        }
        public string NamaKondisiMeterWpf
        {
            get
            {
                if (MasterPelangganAirDetail != null)
                {
                    return MasterPelangganAirDetail.NamaKondisiMeter;
                }
                return null;
            }

            set
            {
                if (MasterPelangganAirDetail != null)
                {
                    MasterPelangganAirDetail.NamaKondisiMeter = value;
                    OnPropertyChanged();
                }
            }
        }

        public int? IdDmaWpf
        {
            get
            {
                if (MasterPelangganAirDetail != null)
                {
                    return MasterPelangganAirDetail.IdDma;
                }
                return null;
            }

            set
            {
                if (MasterPelangganAirDetail != null)
                {
                    MasterPelangganAirDetail.IdDma = value;
                    OnPropertyChanged();
                }
            }
        }
        public string KodeDmaWpf
        {
            get
            {
                if (MasterPelangganAirDetail != null)
                {
                    return MasterPelangganAirDetail.KodeDma;
                }
                return null;
            }

            set
            {
                if (MasterPelangganAirDetail != null)
                {
                    MasterPelangganAirDetail.KodeDma = value;
                    OnPropertyChanged();
                }
            }
        }
        public string NamaDmaWpf
        {
            get
            {
                if (MasterPelangganAirDetail != null)
                {
                    return MasterPelangganAirDetail.NamaDma;
                }
                return null;
            }

            set
            {
                if (MasterPelangganAirDetail != null)
                {
                    MasterPelangganAirDetail.NamaDma = value;
                    OnPropertyChanged();
                }
            }
        }

        public int? IdDmzWpf
        {
            get
            {
                if (MasterPelangganAirDetail != null)
                {
                    return MasterPelangganAirDetail.IdDmz;
                }
                return null;
            }

            set
            {
                if (MasterPelangganAirDetail != null)
                {
                    MasterPelangganAirDetail.IdDmz = value;
                    OnPropertyChanged();
                }
            }
        }
        public string KodeDmzWpf
        {
            get
            {
                if (MasterPelangganAirDetail != null)
                {
                    return MasterPelangganAirDetail.KodeDmz;
                }
                return null;
            }

            set
            {
                if (MasterPelangganAirDetail != null)
                {
                    MasterPelangganAirDetail.KodeDmz = value;
                    OnPropertyChanged();
                }
            }
        }
        public string NamaDmzWpf
        {
            get
            {
                if (MasterPelangganAirDetail != null)
                {
                    return MasterPelangganAirDetail.NamaDmz;
                }
                return null;
            }

            set
            {
                if (MasterPelangganAirDetail != null)
                {
                    MasterPelangganAirDetail.NamaDmz = value;
                    OnPropertyChanged();
                }
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

        public string NamaPemilikWpf
        {
            get
            {
                if (MasterPelangganAirDetail != null)
                {
                    return MasterPelangganAirDetail.NamaPemilik;
                }
                return null;
            }

            set
            {
                if (MasterPelangganAirDetail != null)
                {
                    MasterPelangganAirDetail.NamaPemilik = value;
                    OnPropertyChanged();
                }
            }
        }

        public string AlamatPemilikWpf
        {
            get
            {
                if (MasterPelangganAirDetail != null)
                {
                    return MasterPelangganAirDetail.AlamatPemilik;
                }
                return null;
            }

            set
            {
                if (MasterPelangganAirDetail != null)
                {
                    MasterPelangganAirDetail.AlamatPemilik = value;
                    OnPropertyChanged();
                }
            }
        }

        public string FotoRumah1Wpf
        {
            get { return FotoRumah1; }
            set
            {
                FotoRumah1 = value;
                OnPropertyChanged();
            }
        }

        public string FotoRumah2Wpf
        {
            get { return FotoRumah2; }
            set
            {
                FotoRumah2 = value;
                OnPropertyChanged();
            }
        }

        public string FotoRumah3Wpf
        {
            get { return FotoRumah3; }
            set
            {
                FotoRumah3 = value;
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
