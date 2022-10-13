namespace AppBusiness.Data.DTOs
{
    public class ParamMasterJadwalBacaDto
    {
        public int? IdPdam { get; set; }
        public int? IdJadwalBaca { get; set; }
        public int? IdPetugasBaca { get; set; }
        public int? IdRayon { get; set; }
        public int? TanggalMulaiBaca { get; set; } = 0;
        public int? ToleransiMinus { get; set; } = 0;
        public int? ToleransiPlus { get; set; } = 0;
        public bool? FlagHapus { get; set; } = false;
        public int? IdUserRequest { get; set; }
    }

    public class ParamMasterJadwalBacaFilterDto : ParamMasterJadwalBacaDto
    {
    }
}
