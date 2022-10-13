namespace AppBusiness.Data.DTOs
{
    public class ParamMasterDasarAskesDto
    {
        public int? IdPdam { get; set; }
        public int? IdDasarAskes { get; set; }
        public int? IdPegawai { get; set; }
        public decimal? Jumlah { get; set; } = 0;
        public bool? FlagHapus { get; set; } = false;
        public int? IdUserRequest { get; set; }
    }

    public class ParamMasterDasarAskesFilterDto : ParamMasterDasarAskesDto
    {
        public string NoPegawai { get; set; }
        public string NamaPegawai { get; set; }
    }
}
