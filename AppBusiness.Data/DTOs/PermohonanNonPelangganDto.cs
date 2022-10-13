using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace AppBusiness.Data.DTOs
{
    public class PermohonanNonPelangganDto
    {
        public int? IdPdam { get; set; }
        public int? IdPermohonan { get; set; }
        public int? IdTipePermohonan { get; set; }
        public string StatusPermohonanText { get; set; }
        public string KodeTipePermohonan { get; set; }
        public string NamaTipePermohonan { get; set; }
        public string NomorPermohonan { get; set; }
        public DateTime? WaktuPermohonan { get; set; }
        public bool? FlagPendaftaran { get; set; } = false;
        public int? IdTipePendaftaranSambungan { get; set; }
        public string NamaTipePendaftaranSambungan { get; set; }
        public string Nama { get; set; }
        public string Alamat { get; set; }

        public int? IdGolongan { get; set; }
        public string KodeGolongan { get; set; }
        public string NamaGolongan { get; set; }
        public int? IdDiameter { get; set; }
        public string KodeDiameter { get; set; }
        public string NamaDiameter { get; set; }
        public int? IdRayon { get; set; }
        public string KodeRayon { get; set; }
        public string NamaRayon { get; set; }
        public int? IdArea { get; set; }
        public string KodeArea { get; set; }
        public string NamaArea { get; set; }
        public int? IdWilayah { get; set; }
        public string KodeWilayah { get; set; }
        public string NamaWilayah { get; set; }
        public int? IdKelurahan { get; set; }
        public string KodeKelurahan { get; set; }
        public string NamaKelurahan { get; set; }
        public int? IdKecamatan { get; set; }
        public string KodeKecamatan { get; set; }
        public string NamaKecamatan { get; set; }
        public int? IdCabang { get; set; }
        public string KodeCabang { get; set; }
        public string NamaCabang { get; set; }
        public string Rt { get; set; }
        public string Rw { get; set; }
        public int? IdBlok { get; set; }
        public string NamaBlok { get; set; }
        public string NoHp { get; set; }
        public string NoTelp { get; set; }
        public string Email { get; set; }
        public string NoKtp { get; set; }
        public string NoKk { get; set; }
        public string KodePost { get; set; }
        public decimal? DayaListrik { get; set; } = 0;
        public decimal? LuasTanah { get; set; } = 0;
        public decimal? LuasRumah { get; set; } = 0;
        public int? IdPeruntukan { get; set; }
        public string NamaPeruntukan { get; set; }
        public int? IdJenisBangunan { get; set; }
        public string NamaJenisBangunan { get; set; }
        public int? IdKepemilikan { get; set; }
        public string NamaKepemilikan { get; set; }
        public int? IdPekerjaan { get; set; }
        public string NamaPekerjaan { get; set; }
        public string Keterangan { get; set; }
        public string NosambYangDiberikan { get; set; }
        public string NosambDepan { get; set; }
        public string NosambBelakang { get; set; }
        public string NosambKiri { get; set; }
        public string NosambKanan { get; set; }
        public int? Penghuni { get; set; } = 0;
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string AlamatMap { get; set; }
        public int? IdUser { get; set; }
        public string NamaUser { get; set; }
        public string NamaUserLogin { get; set; }
        public int? IdNonAir { get; set; }
        public string FotoKtp { get; set; }
        public string FotoKk { get; set; }
        public string FotoSuratPernyataan { get; set; }
        public string FotoImb { get; set; }
        public string FotoBukti1 { get; set; }
        public string FotoBukti2 { get; set; }
        public string FotoBukti3 { get; set; }
        public bool? FlagVerifikasi { get; set; }
        public DateTime? WaktuVerifikasi { get; set; }
        public int? IdKolektif { get; set; }
        public int? IdSumberAir { get; set; }
        public int? IdDma { get; set; }
        public int? IdDmz { get; set; }
        public int? IdMerekMeter { get; set; }
        public int? IdKondisiMeter { get; set; }
        public int? IdAdministrasiLain { get; set; }
        public int? IdPemeliharaanLain { get; set; }
        public int? IdRetribusiLain { get; set; }
        public string NoSeriMeter { get; set; }
        public DateTime? TglMeter { get; set; }
        public int? UrutanBaca { get; set; }
        public string NamaPemilik { get; set; }
        public string AlamatPemilik { get; set; }
        public bool? FlagLanjutKeLanggananLimbah { get; set; }
        public decimal? StanAwalPasang { get; set; }
        public List<PermohonanNonPelangganDetailDto> Parameter { get; set; }
        public RekeningNonAirViewDto NonAirReguler { get; set; }
        public PermohonanNonPelangganSpkDto SPK { get; set; }
        public PermohonanNonPelangganBaDto BeritaAcara { get; set; }
        public List<PermohonanNonPelangganPetugasDto> Pelaksana { get; set; }
        public PermohonanNonPelangganRabDto RAB { get; set; }
        public DateTime? WaktuUpdate { get; set; }
        public int StatusPermohonan { get; set; }
        public bool? FlagUsulan { get; set; }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
