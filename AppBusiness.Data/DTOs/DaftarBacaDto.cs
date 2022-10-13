namespace AppBusiness.Data.DTOs
{
    public class DaftarBacaDto
    {
        public int? IdPdam { get; set; }
        public int? IdPelangganAir { get; set; }
        public string NoSamb { get; set; }
        public string NoRekening { get; set; }
        public string Nama { get; set; }
        public string Alamat { get; set; }
        public string NoSeriMeter { get; set; }
        public string NoHp { get; set; }
        public string NoTelp { get; set; }
        public int? IdPeriode { get; set; }
        public int? IdGolongan { get; set; }
        public int? IdDiameter { get; set; }
        public int? IdRayon { get; set; }
        public int? IdWilayah { get; set; }
        public int? IdKelurahan { get; set; }
        public int? IdBlok { get; set; }
        public int? IdKolektif { get; set; }
        public int? IdAdministrasiLain { get; set; }
        public int? IdPemeliharaanLain { get; set; }
        public int? IdRetribusiLain { get; set; }
        public int? IdStatus { get; set; }
        public int? IdFlag { get; set; }
        public int? IdMerekMeter { get; set; }
        public int? IdKondisiMeter { get; set; }
        public bool? FlagRequestBacaUlang { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public decimal? StanLalu { get; set; } = 0;
        public decimal? StanAngkat { get; set; } = 0;
        public int? UrutanBaca { get; set; }
        public string JadwalBaca { get; set; }
        public decimal? LuasRumah { get; set; } = 0;
        public string KelainanBulanLalu { get; set; }
        public string Kelainan2BulanLalu { get; set; }
        public decimal? PakaiBulanLalu { get; set; } = 0;
        public decimal? Pakai2BulanLalu { get; set; } = 0;
        public decimal? Pakai3BulanLalu { get; set; } = 0;
        public decimal? Pakai4BulanLalu { get; set; } = 0;
        public decimal PersentaseBulanLalu { get; set; } = 0;
        public decimal? Persentase2BulanLalu { get; set; } = 0;
        public decimal? Persentase3BulanLalu { get; set; } = 0;
    }
}
