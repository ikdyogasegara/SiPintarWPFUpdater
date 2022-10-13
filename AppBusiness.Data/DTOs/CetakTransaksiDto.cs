using System;

namespace AppBusiness.Data.DTOs
{
    public class CetakTransaksiRekeningAirDto
    {
        public int IdPelangganAir { get; set; }
        public int IdPeriode { get; set; }
        public int KodePeriode { get; set; }
        public string NamaPeriode { get; set; }
        public string NoSamb { get; set; }
        public string NoRekening { get; set; }
        public string Nama { get; set; }
        public string Alamat { get; set; }
        public string KodeGolongan { get; set; }
        public string NamaGolongan { get; set; }
        public string KodeDiameter { get; set; }
        public string NamaDiameter { get; set; }
        public string KodeRayon { get; set; }
        public string NamaRayon { get; set; }
        public string KodeWilayah { get; set; }
        public string NamaWilayah { get; set; }
        public int IdUser { get; set; }
        public string NamaKasir { get; set; }
        public int IdLoket { get; set; }
        public string KodeLoket { get; set; }
        public string NamaLoket { get; set; }
        public string NomorTransaksi { get; set; }
        public DateTime WaktuTransaksi { get; set; }
        public decimal StanLalu { get; set; } = decimal.Zero;
        public decimal StanSkrg { get; set; } = decimal.Zero;
        public decimal StanAngkat { get; set; } = decimal.Zero;
        public decimal Pakai { get; set; } = decimal.Zero;
        public decimal BiayaPemakaian { get; set; } = decimal.Zero;
        public decimal Administrasi { get; set; } = decimal.Zero;
        public decimal Pemeliharaan { get; set; } = decimal.Zero;
        public decimal Retribusi { get; set; } = decimal.Zero;
        public decimal Pelayanan { get; set; } = decimal.Zero;
        public decimal AirLimbah { get; set; } = decimal.Zero;
        public decimal DendaPakai0 { get; set; } = decimal.Zero;
        public decimal AdministrasiLain { get; set; } = decimal.Zero;
        public decimal PemeliharaanLain { get; set; } = decimal.Zero;
        public decimal RetribusiLain { get; set; } = decimal.Zero;
        public decimal Meterai { get; set; } = decimal.Zero;
        public decimal Rekair { get; set; } = decimal.Zero;
        public decimal Denda { get; set; } = decimal.Zero;
        public decimal Ppn { get; set; } = decimal.Zero;
        public decimal Total { get; set; } = decimal.Zero;
    }

    public class CetakTransaksiRekeningLimbahDto
    {
        public int IdPelangganLimbah { get; set; }
        public int IdPeriode { get; set; }
        public int KodePeriode { get; set; }
        public string NamaPeriode { get; set; }
        public string NomorLimbah { get; set; }
        public string Nama { get; set; }
        public string Alamat { get; set; }
        public string KodeTarifLimbah { get; set; }
        public string NamaTarifLimbah { get; set; }
        public string KodeRayon { get; set; }
        public string NamaRayon { get; set; }
        public string KodeWilayah { get; set; }
        public string NamaWilayah { get; set; }
        public int IdUser { get; set; }
        public string NamaKasir { get; set; }
        public int IdLoket { get; set; }
        public string KodeLoket { get; set; }
        public string NamaLoket { get; set; }
        public string NomorTransaksi { get; set; }
        public DateTime WaktuTransaksi { get; set; }
        public decimal Biaya { get; set; } = decimal.Zero;
    }

    public class CetakTransaksiRekeningLlttDto
    {
        public int IdPelangganLltt { get; set; }
        public int IdPeriode { get; set; }
        public int KodePeriode { get; set; }
        public string NamaPeriode { get; set; }
        public string NomorLltt { get; set; }
        public string Nama { get; set; }
        public string Alamat { get; set; }
        public string KodeTarifLltt { get; set; }
        public string NamaTarifLltt { get; set; }
        public string KodeRayon { get; set; }
        public string NamaRayon { get; set; }
        public string KodeWilayah { get; set; }
        public string NamaWilayah { get; set; }
        public int IdUser { get; set; }
        public string NamaKasir { get; set; }
        public int IdLoket { get; set; }
        public string KodeLoket { get; set; }
        public string NamaLoket { get; set; }
        public string NomorTransaksi { get; set; }
        public DateTime WaktuTransaksi { get; set; }
        public decimal Biaya { get; set; } = decimal.Zero;
    }

    public class CetakTransaksiRekeningNonAirDto
    {
        public int IdNonAir { get; set; }
        public string NomorNonAir { get; set; }
        public int IdJenisNonAir { get; set; }
        public string NamaJenisNonAir { get; set; }
        public string NoSamb { get; set; }
        public string KodeWilayah { get; set; }
        public string NamaWilayah { get; set; }
        public int IdUser { get; set; }
        public string NamaKasir { get; set; }
        public int IdLoket { get; set; }
        public string KodeLoket { get; set; }
        public string NamaLoket { get; set; }
        public string Nama { get; set; }
        public string Alamat { get; set; }
        public string Keterangan { get; set; }
        public decimal Total { get; set; } = decimal.Zero;
        public DateTime TanggalMulaiTagih { get; set; }
        public string NomorTransaksi { get; set; }
        public DateTime WaktuTransaksi { get; set; }
    }

    public class CetakTransaksiRekeningAirAngsuranDto
    {
        public int Id { get; set; }
        public string Nama { get; set; }
        public string Alamat { get; set; }
        public string Keterangan { get; set; }
        public int IdAngsuran { get; set; }
        public int Termin { get; set; }
        public decimal Total { get; set; } = decimal.Zero;
        public DateTime TanggalMulaiTagih { get; set; }
        public string NomorTransaksi { get; set; }
        public DateTime WaktuTransaksi { get; set; }
    }

    public class CetakTransaksiRekeningNonAirAngsuranDto
    {
        public int Id { get; set; }
        public int IdNonAir { get; set; }
        public int IdJenisNonAir { get; set; }
        public string NamaJenisNonAir { get; set; }
        public string Nama { get; set; }
        public string Alamat { get; set; }
        public string Keterangan { get; set; }
        public int IdAngsuran { get; set; }
        public int Termin { get; set; }
        public decimal Total { get; set; } = decimal.Zero;
        public DateTime TanggalMulaiTagih { get; set; }
        public string NomorTransaksi { get; set; }
        public DateTime WaktuTransaksi { get; set; }
    }
}
