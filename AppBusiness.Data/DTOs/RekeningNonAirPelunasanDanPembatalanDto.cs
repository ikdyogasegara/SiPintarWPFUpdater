using System;

namespace AppBusiness.Data.DTOs
{
    public class RekeningNonAirPelunasanDanPembatalanDto
    {
        public int? IdPdam { get; set; }
        public int? IdNonAir { get; set; }
        public int? IdJenisNonAir { get; set; }
        public string KodeJenisNonAir { get; set; }
        public string NamaJenisNonAir { get; set; }
        public string NomorNonAir { get; set; }
        public int? KodePeriode { get; set; }
        public string NamaPeriode { get; set; }
        public int? IdPelangganAir { get; set; }
        public string NoSamb { get; set; }

        public int? IdPelangganLimbah { get; set; }
        public string NomorLimbah { get; set; }

        public int? IdPelangganLltt { get; set; }
        public string NomorLltt { get; set; }

        public string Nama { get; set; }
        public string Alamat { get; set; }
        public string Keterangan { get; set; }
        public decimal? Total { get; set; }
        public DateTime? TanggalMulaiTagih { get; set; }
        public DateTime? TanggalKadaluarsa { get; set; }
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
        public bool? FlagHapus { get; set; }
        public DateTime? WaktuUpdate { get; set; }
        public int? IdUserRequest { get; set; }
    }
}
