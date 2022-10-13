using System;

namespace AppBusiness.Data.DTOs
{
    public class RekeningAirPiutangDto
    {
        public int? IdPdam { get; set; }
        public int? IdRekeningAir { get; set; }
        public int? IdPelangganAir { get; set; }
        public string NoSamb { get; set; }
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
        public int? IdAdministrasiLain { get; set; }
        public int? IdPemeliharaanLain { get; set; }
        public int? IdRetribusiLain { get; set; }
        public int? IdRayon { get; set; }
        public string KodeRayon { get; set; }
        public string NamaRayon { get; set; }
        public string KodeArea { get; set; }
        public string NamaArea { get; set; }
        public string KodeWilayah { get; set; }
        public string NamaWilayah { get; set; }
        public int? IdKelurahan { get; set; }
        public string KodeKelurahan { get; set; }
        public string NamaKelurahan { get; set; }
        public string KodeKecamatan { get; set; }
        public string NamaKecamatan { get; set; }
        public string KodeCabang { get; set; }
        public string NamaCabang { get; set; }
        public int? IdKolektif { get; set; }
        public string KodeKolektif { get; set; }
        public string NamaKolektif { get; set; }
        public int? IdStatus { get; set; }
        public string NamaStatus { get; set; }
        public int? IdFlag { get; set; }
        public string NamaFlag { get; set; }
        public string StatusHapusSecaraAkuntansi { get; set; }
        public DateTime? WaktuHapusSecaraAkuntansi { get; set; }
        public bool? FlagBaca { get; set; }
        public DateTime? WaktuBaca { get; set; }
        public string PetugasBaca { get; set; }
        public string Kelainan { get; set; }
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
        public DateTime? TglMulaiDenda1 { get; set; }
        public DateTime? TglMulaiDenda2 { get; set; }
        public DateTime? TglMulaiDenda3 { get; set; }
        public DateTime? TglMulaiDenda4 { get; set; }
        public DateTime? TglMulaiDendaPerBulan { get; set; }
        public DateTime? WaktuUpdate { get; set; }
        public int? IdUserRequest { get; set; }
    }
}
