using System;

namespace AppBusiness.Data.DTOs
{
    public class MasterUpahKehadiranDto
    {
        public int? IdPdam { get; set; }
        public int? IdUpahKehadiran { get; set; }
        public string KodeUpahKehadiran { get; set; }
        public string UpahKehadiran { get; set; }
        public bool? FlagPersen { get; set; }
        public decimal? HariKerja { get; set; }
        public decimal? Dinas { get; set; }
        public decimal? Dispensasi { get; set; }
        public decimal? Izin { get; set; }
        public decimal? Sakit1 { get; set; }
        public decimal? Sakit2 { get; set; }
        public decimal? Sakit3 { get; set; }
        public decimal? Alpha { get; set; }
        public decimal? Cuti { get; set; }
        public decimal? Terlambat1 { get; set; }
        public decimal? Terlambat2 { get; set; }
        public decimal? Terlambat3 { get; set; }
        public decimal? Terlambat4 { get; set; }
        public decimal? Psw1 { get; set; }
        public decimal? Psw2 { get; set; }
        public decimal? Psw3 { get; set; }
        public decimal? Psw4 { get; set; }
        public decimal? LupaAbsen1 { get; set; }
        public decimal? LupaAbsen2 { get; set; }
        public decimal? LupaAbsen3 { get; set; }
        public DateTime? WaktuUpdate { get; set; }
    }
}
