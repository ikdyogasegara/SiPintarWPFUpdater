using System;

namespace AppBusiness.Data.DTOs
{
    public class RekapAbsensiDto
    {
        public int? Id { get; set; }
        public int? IdPdam { get; set; }
        public int? IdPegawai { get; set; }
        public string NoPegawai { get; set; }
        public string NamaPegawai { get; set; }
        public int? KodePeriode { get; set; }
        public string NamaPeriode { get; set; }
        public int? IdKodeGaji { get; set; }
        public string KodeGaji { get; set; }
        public string Deskripsi { get; set; }
        public int? HariKerja { get; set; }
        public int? Izin { get; set; }
        public int? Sakit { get; set; }
        public int? Alpha { get; set; }
        public int? Cuti { get; set; }
        public int? Dispensasi { get; set; }
        public int? Terlambat { get; set; }
        public int? AkumulasiTerlambat { get; set; }
        public int? Mendahului { get; set; }
        public DateTime? TglAwal { get; set; }
        public DateTime? TglAkhir { get; set; }
        public DateTime? Waktuupdate { get; set; }
    }
}
