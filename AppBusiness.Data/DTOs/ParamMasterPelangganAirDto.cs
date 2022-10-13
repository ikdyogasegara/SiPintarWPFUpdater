using System;
using System.Collections.Generic;

namespace AppBusiness.Data.DTOs
{
    public class ParamMasterPelangganAirDto
    {
        public int? IdPdam { get; set; }
        public int? IdPelangganAir { get; set; }
        public string NoSamb { get; set; }
        public string NoRekening { get; set; }
        public string Nama { get; set; }
        public string Alamat { get; set; }
        public string Rt { get; set; }
        public string Rw { get; set; }
        public int? IdGolongan { get; set; }
        public int? IdDiameter { get; set; }
        public int? IdRayon { get; set; }
        public int? IdKelurahan { get; set; }
        public int? IdKolektif { get; set; }
        public int? IdStatus { get; set; }
        public int? IdFlag { get; set; }
        public int? IdSumberAir { get; set; }
        public int? IdDma { get; set; }
        public int? IdDmz { get; set; }
        public int? IdBlok { get; set; }
        public int? IdMerekMeter { get; set; }
        public int? IdKondisiMeter { get; set; }
        public int? IdAdministrasiLain { get; set; }
        public int? IdPemeliharaanLain { get; set; }
        public int? IdRetribusiLain { get; set; }
        public int? IdPekerjaan { get; set; }
        public int? IdJenisBangunan { get; set; }
        public int? IdPeruntukan { get; set; }
        public int? IdKepemilikan { get; set; }
        public string NoSegel { get; set; }
        public string NoHp { get; set; }
        public string NoTelp { get; set; }
        public string NoKtp { get; set; }
        public string NoKk { get; set; }
        public string NoSeriMeter { get; set; }
        public DateTime? TglMeter { get; set; }
        public string Email { get; set; }
        public int? Penghuni { get; set; } = 0;
        public string NamaPemilik { get; set; }
        public string AlamatPemilik { get; set; }
        public string KodePost { get; set; }
        public decimal? DayaListrik { get; set; }
        public decimal? LuasTanah { get; set; }
        public decimal? LuasRumah { get; set; }
        public int? UrutanBaca { get; set; } = 0;
        public decimal? StanAwalPasang { get; set; }
        public string NoPendaftaran { get; set; }
        public DateTime? TglDaftar { get; set; }
        public string NoRab { get; set; }
        public string NoBaPemasangan { get; set; }
        public DateTime? TglPasang { get; set; }
        public string Keterangan { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string AlamatMap { get; set; }
        public string FotoRumah1 { get; set; }
        public string FotoRumah2 { get; set; }
        public string FotoRumah3 { get; set; }
        public bool? FlagHapus { get; set; } = false;
        public int? IdUserRequest { get; set; }
        public string SumberPerubahan { get; set; }
    }


    public class ParamMasterPelangganAirFilterDto : ParamMasterPelangganAirDto
    {
        public int? IdArea { get; set; }
        public string KodeArea { get; set; }
        public string NamaArea { get; set; }
        public int? IdWilayah { get; set; }
        public string KodeWilayah { get; set; }
        public string NamaWilayah { get; set; }
        public string KodeKelurahan { get; set; }
        public string NamaKelurahan { get; set; }
        public int? IdKecamatan { get; set; }
        public string KodeKecamatan { get; set; }
        public string NamaKecamatan { get; set; }
        public int? IdCabang { get; set; }
        public string KodeCabang { get; set; }
        public string NamaCabang { get; set; }
        public string KodeKolektif { get; set; }
        public string NamaKolektif { get; set; }
        public string NamaStatus { get; set; }
        public string NamaFlag { get; set; }
        public int? PeriodeDaftarMulai { get; set; }
        public int? PeriodeDaftarSampaiDengan { get; set; }
        public int? PeriodePasangMulai { get; set; }
        public int? PeriodePasangSampaiDengan { get; set; }
        public DateTime? WaktuUpdate { get; set; }
        public bool? ForMap { get; set; } = false;

        public bool? ForSambungKembali { get; set; } = false;
        public bool? ForTutupSementara { get; set; } = false;
        public bool? ForTutupTotal { get; set; } = false;
    }
}
