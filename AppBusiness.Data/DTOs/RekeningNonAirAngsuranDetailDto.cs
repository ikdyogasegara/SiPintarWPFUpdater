using System;

namespace AppBusiness.Data.DTOs
{
    public class RekeningNonAirAngsuranDetailDto
    {
        public int? Id { get; set; }
        public int? IdPdam { get; set; }
        public int? IdAngsuran { get; set; }
        public int? IdNonAir { get; set; }
        public int? IdPelangganAir { get; set; }
        public int? IdPelangganLimbah { get; set; }
        public int? IdPelangganLltt { get; set; }
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
        public decimal? Total { get; set; }
        public bool? StatusTransaksi { get; set; }
        public DateTime? WaktuTransaksi { get; set; }
        public int? IdUser { get; set; }
        public string NamaUserTransaksi { get; set; }
        public int? IdLoket { get; set; }
        public string NamaLoketTransaksi { get; set; }
        public DateTime? WaktuUpdate { get; set; }
    }

    public class RekeningAngsuranNonAirKalkulasiDetailDto
    {
        public int? IdPdam { get; set; }
        public int? Termin { get; set; }
        public int? IdNonAir { get; set; }
        public int? IdPelangganAir { get; set; }
        public int? IdPelangganLimbah { get; set; }
        public int? IdPelangganLltt { get; set; }
        public int? IdRayon { get; set; }
        public int? IdKelurahan { get; set; }
        public int? IdGolongan { get; set; }
        public int? IdDiameter { get; set; }
        public DateTime? TglMulaiTagih { get; set; }
        public decimal? Total { get; set; } = 0;
    }
}
