using System;

namespace AppBusiness.Data.DTOs
{
    public class RekeningLimbahPelunasanDanPembatalanDto
    {
        public int? IdPdam { get; set; }
        public int? IdPelangganLimbah { get; set; }
        public string NomorLimbah { get; set; }
        public string Nama { get; set; }
        public string Alamat { get; set; }
        public int? IdPelangganAir { get; set; }
        public string NoSamb { get; set; }
        public int? IdPeriode { get; set; }
        public int? KodePeriode { get; set; }
        public string NamaPeriode { get; set; }
        public int? IdTarifLimbah { get; set; }
        public string KodeTarifLimbah { get; set; }
        public string NamaTarifLimbah { get; set; }
        public int? IdStatus { get; set; }
        public string NamaStatus { get; set; }
        public int? IdFlag { get; set; }
        public string NamaFlag { get; set; }
        public int? HapusSecaraAkuntansi { get; set; }
        public DateTime? WaktuHapusSecaraAkuntansi { get; set; }
        public decimal? Biaya { get; set; }
        public bool? FlagPublish { get; set; }
        public DateTime? WaktuPublish { get; set; }
        public bool? FlagUpload { get; set; }
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
        public bool? FlagHapus { get; set; } = false;
        public DateTime? WaktuUpdate { get; set; }
        public int? IdUserRequest { get; set; }
    }
}
