using System;

namespace AppBusiness.Data.DTOs
{
    public class RiwayatPelunasanDanPembatalanWpf
    {
        public int? IdPdam { get; set; }

        public int? IdPelangganAir { get; set; }
        public string NoSamb { get; set; }

        public int? IdPelangganLimbah { get; set; }
        public string NomorLimbah { get; set; }

        public int? IdPelangganLltt { get; set; }
        public string NomorLltt { get; set; }

        public int? IdNonAir { get; set; }
        public string NomorNonAir { get; set; }
        public string KodeJenisNonAir { get; set; }
        public string NamaJenisNonAir { get; set; }

        public string Nama { get; set; }
        public string Alamat { get; set; }
        public int? IdPeriode { get; set; }
        public int? KodePeriode { get; set; }
        public string NamaPeriode { get; set; }

        public int? IdGolongan { get; set; }
        public string KodeGolongan { get; set; }
        public string NamaGolongan { get; set; }

        public int? IdTarifLimbah { get; set; }
        public string KodeTarifLimbah { get; set; }
        public string NamaTarifLimbah { get; set; }

        public int? IdTarifLltt { get; set; }
        public string KodeTarifLltt { get; set; }
        public string NamaTarifLltt { get; set; }

        public string Kategori { get; set; }
        public int? IdDiameter { get; set; }
        public string KodeDiameter { get; set; }
        public string NamaDiameter { get; set; }
        public int? IdStatus { get; set; }
        public string NamaStatus { get; set; }
        public int? IdFlag { get; set; }
        public string NamaFlag { get; set; }
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
        public decimal? Meterai { get; set; }
        public decimal? Rekair { get; set; }
        public decimal? Denda { get; set; }

        public decimal? Total { get; set; }

        public decimal? Biaya { get; set; }

        public string NomorTransaksi { get; set; }
        public bool? StatusTransaksi { get; set; }
        public int? TahunTransaksi { get; set; }
        public DateTime? WaktuTransaksi { get; set; }
        public string PetugasBaca { get; set; }
        public int? IdUser { get; set; }
        public string NamaUser { get; set; }
        public int? IdLoket { get; set; }
        public string KodeLoket { get; set; }
        public string NamaLoket { get; set; }
        public string AlasanBatal { get; set; }
        public DateTime? WaktuUpdate { get; set; }
        public int? IdUserRequest { get; set; }
    }
}
