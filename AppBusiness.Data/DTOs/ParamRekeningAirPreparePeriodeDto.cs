using System;

namespace AppBusiness.Data.DTOs
{
    public class ParamRekeningAirPreparePeriodeDto
    {
        public int? IdPdam { get; set; }
        public int? IdPelangganAir { get; set; }
        public int? IdPeriode { get; set; }
        public int? IdGolongan { get; set; }
        public int? IdDiameter { get; set; }
        public int? IdRayon { get; set; }
        public int? IdKelurahan { get; set; }
        public int? IdKolektif { get; set; }
        public int? IdAdministrasiLain { get; set; }
        public int? IdPemeliharaanLain { get; set; }
        public int? IdRetribusiLain { get; set; }
        public int? IdStatus { get; set; }
        public int? IdFlag { get; set; }
        public decimal? StanLalu { get; set; } = 0;
        public decimal? StanSkrg { get; set; } = 0;
        public decimal? StanAngkat { get; set; } = 0;
        public decimal? Pakai { get; set; } = 0;
        public decimal? PakaiKalkulasi { get; set; } = 0;
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
        public decimal? Rekair { get; set; } = 0;
        public decimal? Denda { get; set; } = 0;
        public decimal? Ppn { get; set; } = 0;
        public decimal? Total { get; set; } = 0;
        public bool? FlagBaca { get; set; } = false;
        public bool? FlagKoreksi { get; set; } = false;
        public bool? FlagPublish { get; set; } = false;
        public bool? FlagUpload { get; set; } = false;
        public bool? FlagHapus { get; set; } = false;
        public bool? Taksasi { get; set; } = false;
        public bool? Taksir { get; set; } = false;
        public bool? FlagRequestBacaUlang { get; set; } = false;
        public bool? FlagVerifikasi { get; set; } = false;
        public bool? AdaFotoMeter { get; set; } = false;
        public bool? AdaFotoRumah { get; set; } = false;
        public bool? AdaVideo { get; set; } = false;
        public bool? FlagMinimumPakai { get; set; } = false;
        public decimal? PakaiBulanLalu { get; set; } = 0;
        public decimal? Pakai2BulanLalu { get; set; } = 0;
        public decimal? Pakai3BulanLalu { get; set; } = 0;
        public decimal? Pakai4BulanLalu { get; set; } = 0;
        public decimal? PersentaseBulanLalu { get; set; } = 0;
        public decimal? Persentase2BulanLalu { get; set; } = 0;
        public decimal? Persentase3BulanLalu { get; set; } = 0;
        public string KelainanBulanLalu { get; set; }
        public string Kelainan2BulanLalu { get; set; }
        public bool? FlagAngsur { get; set; } = false;
        public int? HapusSecaraAkuntansi { get; set; } = 0;
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string LatitudeBulanLalu { get; set; }
        public string LongitudeBulanLalu { get; set; }
        public bool? FlagKoreksiBilling { get; set; } = false;
        public DateTime? TglMulaiDenda1 { get; set; }
        public DateTime? TglMulaiDenda2 { get; set; }
        public DateTime? TglMulaiDenda3 { get; set; }
        public DateTime? TglMulaiDenda4 { get; set; }
        public DateTime? TglMulaiDendaPerBulan { get; set; }

    }
}
