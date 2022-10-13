using System;

namespace AppBusiness.Data.DTOs
{
    public class RekeningAirAngsuranDto
    {
        public int? IdPdam { get; set; }
        public int? IdAngsuran { get; set; }
        public string NoAngsuran { get; set; }
        public int? IdJenisNonAir { get; set; }
        public string NamaJenisNonAir { get; set; }
        public int? IdPelangganAir { get; set; }
        public string NoSamb { get; set; }
        public string Nama { get; set; }
        public string Alamat { get; set; }
        public string NoTelp { get; set; }
        public string NoHp { get; set; }
        public DateTime? WaktuDaftar { get; set; }
        public int? JumlahTermin { get; set; }
        public decimal? JumlahAngsuranPokok { get; set; }
        public decimal? JumlahAngsuranBunga { get; set; }
        public decimal? JumlahUangMuka { get; set; }
        public decimal? Total { get; set; }
        public int? IdUser { get; set; }
        public string NamaUser { get; set; }
        public DateTime? TglMulaiTagihPertama { get; set; }
        public string NoBeritaAcara { get; set; }
        public DateTime? TglBeritaAcara { get; set; }
        public bool? FlagPublish { get; set; }
        public DateTime? WaktuPublish { get; set; }
        public bool? FlagLunas { get; set; }
        public DateTime? WaktuLunas { get; set; }
        public DateTime? WaktuUpdate { get; set; }
    }

    public class CetakRekeningAirAngsuranDto
    {
        public string NoAngsuran { get; set; }
        public string NamaJenisNonAir { get; set; }
        public string NoSamb { get; set; }
        public string Nama { get; set; }
        public string Alamat { get; set; }
        public string NoTelp { get; set; }
        public string NoHp { get; set; }
        public DateTime WaktuDaftar { get; set; } = DateTime.Now;
        public int JumlahTermin { get; set; }
        public decimal JumlahAngsuranPokok { get; set; } = decimal.Zero;
        public decimal JumlahAngsuranBunga { get; set; } = decimal.Zero;
        public decimal JumlahUangMuka { get; set; } = decimal.Zero;
        public decimal Total { get; set; } = decimal.Zero;
        public string NamaUser { get; set; }
        public DateTime TglMulaiTagihPertama { get; set; } = DateTime.Now;
        public string NoBeritaAcara { get; set; }
        public DateTime TglBeritaAcara { get; set; } = DateTime.Now;
        public bool FlagPublish { get; set; }
        public DateTime WaktuPublish { get; set; } = DateTime.Now;
        public bool FlagLunas { get; set; }
        public DateTime WaktuLunas { get; set; } = DateTime.Now;
        public DateTime WaktuUpdate { get; set; } = DateTime.Now;
    }
}
