using System;

namespace AppBusiness.Data.DTOs
{
    public class RekeningAirDto : ICloneable
    {
        public int? IdPdam { get; set; }
        public int? IdPelangganAir { get; set; }
        public string NoSamb { get; set; }
        public string NoSeriMeter { get; set; }
        public string NoRekening { get; set; }
        public string Nama { get; set; }
        public string Alamat { get; set; }
        public int? IdPeriode { get; set; }
        public int? KodePeriode { get; set; }
        public string NamaPeriode { get; set; }
        public int? IdGolongan { get; set; }
        public string KodeGolongan { get; set; }
        public string NamaGolongan { get; set; }
        public string Kategori { get; set; }
        public int? IdDiameter { get; set; }
        public string KodeDiameter { get; set; }
        public string NamaDiameter { get; set; }
        public int? IdRayon { get; set; }
        public string KodeRayon { get; set; }
        public string NamaRayon { get; set; }
        public string KodeArea { get; set; }
        public string NamaArea { get; set; }
        public int? IdWilayah { get; set; }
        public string KodeWilayah { get; set; }
        public string NamaWilayah { get; set; }
        public int? IdKelurahan { get; set; }
        public string KodeKelurahan { get; set; }
        public string NamaKelurahan { get; set; }
        public string KodeKecamatan { get; set; }
        public string NamaKecamatan { get; set; }
        public string KodeCabang { get; set; }
        public string NamaCabang { get; set; }
        public int? IdBlok { get; set; }
        public int? IdCabang { get; set; }
        public int? IdKolektif { get; set; }
        public string KodeKolektif { get; set; }
        public string NamaKolektif { get; set; }
        public int? IdAdministrasiLain { get; set; }
        public int? IdPemeliharaanLain { get; set; }
        public int? IdRetribusiLain { get; set; }
        public int? IdStatus { get; set; }
        public string NamaStatus { get; set; }
        public int? IdFlag { get; set; }
        public string NamaFlag { get; set; }
        public DateTime? WaktuHapusSecaraAkuntansi { get; set; }
        public decimal? StanLalu { get; set; }
        public decimal? StanSkrg { get; set; }
        public decimal? StanAngkat { get; set; }
        public decimal? Pakai { get; set; }
        public decimal? PakaiKalkulasi { get; set; }
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
        public decimal? Ppn { get; set; }
        public decimal? Meterai { get; set; }
        public decimal? Rekair { get; set; }
        public decimal? Denda { get; set; }
        public decimal? Total { get; set; }
        public bool? FlagBaca { get; set; } = false;
        public int? MetodeBaca { get; set; }
        public string NamaMetodeBaca { get; set; }
        public DateTime? WaktuBaca { get; set; }
        public string PetugasBaca { get; set; }
        public string Kelainan { get; set; }
        public decimal? StanBaca { get; set; }
        public DateTime? WaktuKirimHasilBaca { get; set; }
        public string Memo { get; set; }
        public string Lampiran { get; set; }
        public bool? Taksasi { get; set; } = false;
        public bool? Taksir { get; set; } = false;
        public int? FlagRequestBacaUlang { get; set; }
        public string RequestBacaUlang { get; set; }
        public DateTime? WaktuUpdateRequestBacaUlang { get; set; }
        public bool? FlagKoreksi { get; set; } = false;
        public DateTime? WaktuKoreksi { get; set; }
        public bool? FlagVerifikasi { get; set; } = false;
        public DateTime? WaktuVerifikasi { get; set; }
        public bool? FlagPublish { get; set; } = false;
        public DateTime? WaktuPublish { get; set; }
        public bool? FlagUpload { get; set; } = false;
        public DateTime? WaktuUpload { get; set; }
        public string NomorTransaksi { get; set; }
        public bool? StatusTransaksi { get; set; }
        public int? TahunTransaksi { get; set; }
        public DateTime? WaktuTransaksi { get; set; }
        public int? IdUser { get; set; }
        public string NamaUser { get; set; }
        public int? IdLoket { get; set; }
        public string KodeLoket { get; set; }
        public string NamaLoket { get; set; }
        public string AlasanBatal { get; set; }
        public DateTime? WaktuUpdate { get; set; }
        public int? IdUserRequest { get; set; }
        public int? IdMerekMeter { get; set; }
        public string NamaMerekMeter { get; set; }
        public int? IdKondisiMeter { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string LatitudeBulanLalu { get; set; }
        public string LongitudeBulanLalu { get; set; }
        public bool? AdaFotoMeter { get; set; } = false;
        public bool? AdaFotoRumah { get; set; } = false;
        public bool? AdaVideo { get; set; } = false;
        public bool? FlagMinimumPakai { get; set; } = false;
        public decimal? PakaiBulanLalu { get; set; }
        public decimal? Pakai2BulanLalu { get; set; }
        public decimal? Pakai3BulanLalu { get; set; }
        public decimal? Pakai4BulanLalu { get; set; }
        public decimal? PersentaseBulanLalu { get; set; }
        public decimal? Persentase2BulanLalu { get; set; }
        public decimal? Persentase3BulanLalu { get; set; }
        public string KelainanBulanLalu { get; set; }
        public string Kelainan2BulanLalu { get; set; }
        public bool IsSelected { get; set; }
        public bool? FlagKoreksiBilling { get; set; }
        public DateTime? TglMulaiDenda1 { get; set; }
        public DateTime? TglMulaiDenda2 { get; set; }
        public DateTime? TglMulaiDenda3 { get; set; }
        public DateTime? TglMulaiDenda4 { get; set; }
        public DateTime? TglMulaiDendaPerBulan { get; set; }

        public decimal? StanSkrgBulanLalu { get; set; } = 0;
        public decimal? StanSkrg2BulanLalu { get; set; } = 0;
        public decimal? StanSkrg3BulanLalu { get; set; } = 0;
        public object Clone()
        {
            return MemberwiseClone();
        }
    }

    public class RekeningAirDetailDto
    {
        public int? IdPdam { get; set; }
        public int? IdPelangganAir { get; set; }
        public int? IdPeriode { get; set; }
        public decimal? Blok1 { get; set; } = 0;
        public decimal? Blok2 { get; set; } = 0;
        public decimal? Blok3 { get; set; } = 0;
        public decimal? Blok4 { get; set; } = 0;
        public decimal? Blok5 { get; set; } = 0;
        public decimal? Prog1 { get; set; } = 0;
        public decimal? Prog2 { get; set; } = 0;
        public decimal? Prog3 { get; set; } = 0;
        public decimal? Prog4 { get; set; } = 0;
        public decimal? Prog5 { get; set; } = 0;
        public decimal? Tarif1 { get; set; } = 0;
        public decimal? Tarif2 { get; set; } = 0;
        public decimal? Tarif3 { get; set; } = 0;
        public decimal? Tarif4 { get; set; } = 0;
        public decimal? Tarif5 { get; set; } = 0;
        public int? IdUserRequest { get; set; }
        public decimal? Ratarata3bulan { get; set; } = 0;
        public decimal? PersentasePemakaian3bulan { get; set; } = 0;
        public RekeningAirDetailRiwayatDto Riwayat { get; set; }
    }

    public class RekeningAirDetailRiwayatDto
    {
        public string NamaBulanLalu { get; set; }
        public decimal? StanLaluBulanLalu { get; set; } = 0;
        public decimal? StanSkrgBulanLalu { get; set; } = 0;
        public decimal? StanAngkatBulanLalu { get; set; } = 0;
        public decimal? PakaiBulanLalu { get; set; } = 0;
        public decimal? BiayaPemakaianBulanLalu { get; set; } = 0;
        public string Nama2BulanLalu { get; set; }
        public decimal? StanLalu2BulanLalu { get; set; } = 0;
        public decimal? StanSkrg2BulanLalu { get; set; } = 0;
        public decimal? StanAngkat2BulanLalu { get; set; } = 0;
        public decimal? Pakai2BulanLalu { get; set; } = 0;
        public decimal? BiayaPemakaian2BulanLalu { get; set; } = 0;
        public string Nama3BulanLalu { get; set; }
        public decimal? StanLalu3BulanLalu { get; set; } = 0;
        public decimal? StanSkrg3BulanLalu { get; set; } = 0;
        public decimal? StanAngkat3BulanLalu { get; set; } = 0;
        public decimal? Pakai3BulanLalu { get; set; } = 0;
        public decimal? BiayaPemakaian3BulanLalu { get; set; } = 0;
    }
}
