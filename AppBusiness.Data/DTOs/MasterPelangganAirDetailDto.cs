using System;

namespace AppBusiness.Data.DTOs
{
    public class MasterPelangganAirDetailDto
    {
        public int? IdPdam { get; set; }
        public int? IdPelangganAir { get; set; }
        public int? IdSumberAir { get; set; }
        public string KodeSumberAir { get; set; }
        public string NamaSumberAir { get; set; }
        public int? IdDma { get; set; }
        public string KodeDma { get; set; }
        public string NamaDma { get; set; }
        public int? IdDmz { get; set; }
        public string KodeDmz { get; set; }
        public string NamaDmz { get; set; }
        public int? IdBlok { get; set; }
        public string KodeBlok { get; set; }
        public string NamaBlok { get; set; }
        public int? IdMerekMeter { get; set; }
        public string KodeMerekMeter { get; set; }
        public string NamaMerekMeter { get; set; }
        public int? IdKondisiMeter { get; set; }
        public string KodeKondisiMeter { get; set; }
        public string NamaKondisiMeter { get; set; }
        public int? IdAdministrasiLain { get; set; }
        public string KodeAdministrasiLain { get; set; }
        public string NamaAdministrasiLain { get; set; }
        public int? IdPemeliharaanLain { get; set; }
        public string KodePemeliharaanLain { get; set; }
        public string NamaPemeliharaanLain { get; set; }
        public int? IdRetribusiLain { get; set; }
        public string KodeRetribusiLain { get; set; }
        public string NamaRetribusiLain { get; set; }
        public int? IdPekerjaan { get; set; }
        public string NamaPekerjaan { get; set; }
        public int? IdJenisBangunan { get; set; }
        public string NamaJenisBangunan { get; set; }
        public int? IdPeruntukan { get; set; }
        public string NamaPeruntukan { get; set; }
        public int? IdKepemilikan { get; set; }
        public string NamaKepemilikan { get; set; }
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
        public decimal? DayaListrik { get; set; } = 0;
        public decimal? LuasTanah { get; set; } = 0;
        public decimal? LuasRumah { get; set; } = 0;
        public int? UrutanBaca { get; set; } = 0;
        public decimal? StanAwalPasang { get; set; } = 0;
        public string NoPendaftaran { get; set; }
        public DateTime? TglDaftar { get; set; }
        public string NoRab { get; set; }
        public string NoBaPemasangan { get; set; }
        public DateTime? TglPasang { get; set; }
        public string Keterangan { get; set; }
        public DateTime? WaktuUpdate { get; set; }
        public int? IdUserRequest { get; set; }
    }
}
