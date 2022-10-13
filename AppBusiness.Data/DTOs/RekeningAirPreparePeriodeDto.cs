namespace AppBusiness.Data.DTOs
{
    public class RekeningAirPreparePeriodeDto
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
        public decimal? Total { get; set; } = 0;
        public bool? FlagBaca { get; set; } = false;
        public bool? FlagKoreksi { get; set; } = false;
        public bool? FlagPublish { get; set; } = false;
        public bool? FlagUpload { get; set; } = false;
        public bool? FlagHapus { get; set; } = false;
        public bool? Taksasi { get; set; } = false;
    }
}
