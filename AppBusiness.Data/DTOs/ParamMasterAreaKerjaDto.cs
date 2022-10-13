namespace AppBusiness.Data.DTOs
{
    public class ParamMasterAreaKerjaDto
    {
        public int? IdPdam { get; set; }
        public int? IdAreaKerja { get; set; }
        public string KodeAreaKerja { get; set; }
        public string AreaKerja { get; set; }
        public int? Urutan { get; set; } = 0;
        public string KodePerGaji { get; set; }
        public string KodePerTunj { get; set; }
        public string KodePerSppd { get; set; }
        public string KodePerTunj2 { get; set; }
        public string KodePerRep { get; set; }
        public string KodePerPph { get; set; }
        public string KodePerNonPeg { get; set; }
        public bool? FlagHapus { get; set; } = false;
        public int? IdUserRequest { get; set; }
    }

    public class ParamMasterAreaKerjaFilterDto : ParamMasterAreaKerjaDto
    {
    }
}
