using System;

namespace AppBusiness.Data.DTOs
{
    public class TagihanGlobalDto
    {
        public string JenisTagihan { get; set; }
        public int? IdPelangganAir { get; set; }
        public int? IdPelangganLimbah { get; set; }
        public int? IdPelangganLltt { get; set; }
        public int? IdNonAir { get; set; }
        public int? IdPeriode { get; set; }
        public int? KodePeriode { get; set; }
        public string NamaPeriode { get; set; }
        public string NoSamb { get; set; }
        public string NomorLimbah { get; set; }
        public string NomorLltt { get; set; }
        public string NomorNonAir { get; set; }
        public string NamaJenisNonAir { get; set; }

        public string Nama { get; set; }
        public string Alamat { get; set; }
        public string KodeGolongan { get; set; }
        public string KodeDiameter { get; set; }
        public string KodeRayon { get; set; }
        public string KodeWilayah { get; set; }
        public string Kelainan { get; set; }
        public string NamaStatus { get; set; }
        public decimal? StanLalu { get; set; } = 0;
        public decimal? StanSkrg { get; set; } = 0;
        public decimal? StanAngkat { get; set; } = 0;
        public decimal? Pakai { get; set; } = 0;
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
        public decimal? Ppn { get; set; } = 0;
        public decimal? Meterai { get; set; } = 0;
        public decimal? Rekair { get; set; } = 0;
        public decimal? Denda { get; set; } = 0;
        public decimal? Total { get; set; } = 0;
        public string Keterangan { get; set; }
        public DateTime? TanggalMulaiTagih { get; set; }

    }
}
