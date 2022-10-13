namespace AppBusiness.Data.DTOs
{
    public class ParamMasterUpahKehadiranDto
    {
        public int? IdPdam { get; set; }
        public int? IdUpahKehadiran { get; set; }
        public string KodeUpahKehadiran { get; set; }
        public string UpahKehadiran { get; set; }
        public bool? FlagPersen { get; set; } = false;
        public decimal? HariKerja { get; set; } = 0;
        public decimal? Dinas { get; set; } = 0;
        public decimal? Dispensasi { get; set; } = 0;
        public decimal? Izin { get; set; } = 0;
        public decimal? Sakit1 { get; set; } = 0;
        public decimal? Sakit2 { get; set; } = 0;
        public decimal? Sakit3 { get; set; } = 0;
        public decimal? Alpha { get; set; } = 0;
        public decimal? Cuti { get; set; } = 0;
        public decimal? Terlambat1 { get; set; } = 0;
        public decimal? Terlambat2 { get; set; } = 0;
        public decimal? Terlambat3 { get; set; } = 0;
        public decimal? Terlambat4 { get; set; } = 0;
        public decimal? Psw1 { get; set; } = 0;
        public decimal? Psw2 { get; set; } = 0;
        public decimal? Psw3 { get; set; } = 0;
        public decimal? Psw4 { get; set; } = 0;
        public decimal? LupaAbsen1 { get; set; } = 0;
        public decimal? LupaAbsen2 { get; set; } = 0;
        public decimal? LupaAbsen3 { get; set; } = 0;
        public bool? FlagHapus { get; set; } = false;
        public int? IdUserRequest { get; set; }
    }

    public class ParamMasterUpahKehadiranFilterDto : ParamMasterUpahKehadiranDto
    {
    }
}
