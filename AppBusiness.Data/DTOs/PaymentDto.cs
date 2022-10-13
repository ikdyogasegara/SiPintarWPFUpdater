using System;
using System.Collections.Generic;

namespace AppBusiness.Data.DTOs
{
    public class PaymentDto
    {
        public PelangganAirDto PelangganAir { get; set; }
        public PelangganLimbahDto PelangganLimbah { get; set; }
        public PelangganLlttDto PelangganLltt { get; set; }
        public NonPelangganDto NonPelanggan { get; set; }
        public List<TagihanRekeningAirDto> RekeningAir { get; set; }
        public List<TagihanRekeningLimbahDto> RekeningLimbah { get; set; }
        public List<TagihanRekeningLlttDto> RekeningLltt { get; set; }
        public List<TagihanRekeningNonAirDto> RekeningNonAir { get; set; }
        public List<TagihanAngsuranDto> AngsuranNonAir { get; set; }
        public List<TagihanAngsuranDto> AngsuranAir { get; set; }
    }

    public class PelangganAirDto
    {
        public string NoSamb { get; set; }
        public string Nama { get; set; }
        public string Alamat { get; set; }
        public string KodeGolongan { get; set; }
        public string NamaGolongan { get; set; }
        public string NamaStatus { get; set; }
        public string NamaWilayah { get; set; }
        public string NamaArea { get; set; }
        public string NamaRayon { get; set; }
        public string NamaKelurahan { get; set; }
    }

    public class PelangganLimbahDto
    {
        public string NomorLimbah { get; set; }
        public string Nama { get; set; }
        public string Alamat { get; set; }
        public string KodeTarifLimbah { get; set; }
        public string NamaTarifLimbah { get; set; }
        public string NamaStatus { get; set; }
        public string NamaWilayah { get; set; }
        public string NamaArea { get; set; }
        public string NamaRayon { get; set; }
        public string NamaKelurahan { get; set; }
    }

    public class PelangganLlttDto
    {
        public string NomorLltt { get; set; }
        public string Nama { get; set; }
        public string Alamat { get; set; }
        public string KodeTarifLltt { get; set; }
        public string NamaTarifLltt { get; set; }
        public string NamaStatus { get; set; }
        public string NamaWilayah { get; set; }
        public string NamaArea { get; set; }
        public string NamaRayon { get; set; }
        public string NamaKelurahan { get; set; }
    }

    public class NonPelangganDto
    {
        public string NomorNonAir { get; set; }
        public string Nama { get; set; }
        public string Alamat { get; set; }
        public string NamaWilayah { get; set; }
        public string NamaArea { get; set; }
        public string NamaRayon { get; set; }
        public string StatusText { get; set; }
    }

    public class TagihanRekeningAirDto
    {
        public int? IdPelangganAir { get; set; }
        public int? IdPeriode { get; set; }
        public int? KodePeriode { get; set; }
        public string NamaPeriode { get; set; }
        public string NoSamb { get; set; }
        public string Nama { get; set; }
        public string Alamat { get; set; }
        public string KodeGolongan { get; set; }
        public string KodeDiameter { get; set; }
        public string KodeRayon { get; set; }
        public string KodeWilayah { get; set; }
        public string Kelainan { get; set; }
        public string NamaStatus { get; set; }
        public int? IdFlag { get; set; }
        public decimal? StanLalu { get; set; }
        public decimal? StanSkrg { get; set; }
        public decimal? StanAngkat { get; set; }
        public decimal? Pakai { get; set; }
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
        public decimal? DendaTunggakan1 { get; set; } = 0;
        public decimal? DendaTunggakan2 { get; set; } = 0;
        public decimal? DendaTunggakan3 { get; set; } = 0;
        public decimal? DendaTunggakan4 { get; set; } = 0;
        public decimal? DendaTunggakanPerBulan { get; set; } = 0;

    }

    public class TagihanRekeningLimbahDto
    {
        public int? IdPelangganLimbah { get; set; }
        public int? IdPeriode { get; set; }
        public int? KodePeriode { get; set; }
        public string NamaPeriode { get; set; }
        public string NomorLimbah { get; set; }
        public string Nama { get; set; }
        public string Alamat { get; set; }
        public string KodeTarifLimbah { get; set; }
        public string KodeRayon { get; set; }
        public string KodeWilayah { get; set; }
        public decimal? Biaya { get; set; }
        public string NamaStatus { get; set; }
    }

    public class TagihanRekeningLlttDto
    {
        public int? IdPelangganLltt { get; set; }
        public int? IdPeriode { get; set; }
        public int? KodePeriode { get; set; }
        public string NamaPeriode { get; set; }
        public string NomorLltt { get; set; }
        public string Nama { get; set; }
        public string Alamat { get; set; }
        public string KodeTarifLltt { get; set; }
        public string KodeRayon { get; set; }
        public string KodeWilayah { get; set; }
        public decimal? Biaya { get; set; }
    }

    public class TagihanRekeningNonAirDto
    {
        public int? IdNonAir { get; set; }
        public string NomorNonAir { get; set; }
        public int? IdJenisNonAir { get; set; }
        public string NamaJenisNonAir { get; set; }
        public int? IdPeriode { get; set; }
        public int? KodePeriode { get; set; }
        public string NamaPeriode { get; set; }
        public string Nama { get; set; }
        public string Alamat { get; set; }
        public string Keterangan { get; set; }
        public decimal? Total { get; set; }
        public int? IdPelangganAir { get; set; }
        public int? IdPelangganLimbah { get; set; }
        public int? IdPelangganLltt { get; set; }
        public DateTime? TanggalMulaiTagih { get; set; }
    }

    public class TagihanAngsuranDto
    {
        public int? Id { get; set; }
        public int? IdAngsuran { get; set; }
        public int? IdNonAir { get; set; }
        public string NomorNonAir { get; set; }
        public int? IdJenisNonAir { get; set; }
        public string NamaJenisNonAir { get; set; }
        public int? IdPeriode { get; set; }
        public int? KodePeriode { get; set; }
        public string NamaPeriode { get; set; }
        public string Nama { get; set; }
        public string Alamat { get; set; }
        public string Keterangan { get; set; }
        public int? Termin { get; set; }
        public decimal? Total { get; set; }
        public DateTime? TanggalMulaiTagih { get; set; }
    }
}
