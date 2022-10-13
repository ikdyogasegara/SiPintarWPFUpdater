namespace AppBusiness.Data.DTOs
{
    public class ParamMasterDasarPensiunDto
    {
        public int? IdPdam { get; set; }
        public int? IdDasarPensiun { get; set; }
        public int? IdPegawai { get; set; }
        public decimal? Jumlah { get; set; } = 0;
        public bool? FlagHapus { get; set; } = false;
        public int? IdUserRequest { get; set; }
    }

    public class ParamMasterDasarPensiunFilterDto : ParamMasterDasarPensiunDto
    {
        public string NoPegawai { get; set; }
        public string NamaPegawai { get; set; }
    }
}
