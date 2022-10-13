using System;

namespace AppBusiness.Data.DTOs
{
    public class RekeningAirAngsuranDetailDto
    {
        public int? Id { get; set; }
        public int? IdPdam { get; set; }
        public int? IdAngsuran { get; set; }
        public int? IdPeriode { get; set; }
        public int? KodePeriode { get; set; }
        public string NamaPeriode { get; set; }
        public int? IdPelangganAir { get; set; }
        public int? Termin { get; set; }
        public string Keterangan { get; set; }
        public int? IdRayon { get; set; }
        public string KodeRayon { get; set; }
        public string NamaRayon { get; set; }
        public string KodeWilayah { get; set; }
        public string NamaWilayah { get; set; }
        public int? IdKelurahan { get; set; }
        public string KodeKelurahan { get; set; }
        public string NamaKelurahan { get; set; }
        public int? IdGolongan { get; set; }
        public string KodeGolongan { get; set; }
        public string NamaGolongan { get; set; }
        public int? IdDiameter { get; set; }
        public string KodeDiameter { get; set; }
        public string NamaDiameter { get; set; }
        public DateTime? TglMulaiTagih { get; set; }
        public decimal? BiayaPemakaian { get; set; }
        public decimal? Administrasi { get; set; }
        public decimal? Pemeliharaan { get; set; }
        public decimal? Retribusi { get; set; }
        public decimal? Pelayanan { get; set; }
        public decimal? AirLimbah { get; set; }
        public decimal? DendaPakai0 { get; set; }
        public decimal? AdministrasiLain { get; set; }
        public decimal? PemeliharaanLain { get; set; }
        public decimal? RetribusiLain { get; set; }
        public decimal? Meterai { get; set; }
        public decimal? RekAir { get; set; }
        public decimal? Denda { get; set; }
        public decimal? Ppn { get; set; }
        public decimal? Total { get; set; }
        public bool? StatusTransaksi { get; set; }
        public DateTime? WaktuTransaksi { get; set; }
        public int? IdUser { get; set; }
        public string NamaUserTransaksi { get; set; }
        public int? IdLoket { get; set; }
        public string NamaLoketTransaksi { get; set; }
        public DateTime? WaktuUpdate { get; set; }
    }

    public class RekeningAngsuranAirKalkulasiDetailDto
    {
        public int? IdPdam { get; set; }
        public string Uraian { get; set; }
        public int? Termin { get; set; }
        public int? IdPelangganAir { get; set; }
        public int? IdPeriode { get; set; }
        public string PeriodeText { get; set; }
        public int? IdRayon { get; set; }
        public int? IdKelurahan { get; set; }
        public int? IdGolongan { get; set; }
        public int? IdDiameter { get; set; }
        public DateTime? TglMulaiTagih { get; set; }
        public decimal? BiayaPemakaian { get; set; } = 0;
        public decimal? Administrasi { get; set; } = 0;
        public decimal? Pemeliharaan { get; set; } = 0;
        public decimal? Retribusi { get; set; } = 0;
        public decimal? Pelayanan { get; set; } = 0;
        public decimal? AirLimbah { get; set; } = 0;
        public decimal? DendaPakai0 { get; set; } = 0;
        public decimal? AdministrasiLain { get; set; } = 0;
        public decimal? PemeliharaanLain { get; set; } = 0;
        public decimal? RetribusiLain { get; set; } = 0;
        public decimal? Meterai { get; set; } = 0;
        public decimal? RekAir { get; set; } = 0;
        public decimal? Denda { get; set; } = 0;
        public decimal? Ppn { get; set; } = 0;
        public decimal? Total { get; set; } = 0;
    }
}
