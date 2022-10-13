namespace AppBusiness.Data.DTOs
{
    public class ParamMasterDasarLainnyaDto
    {
        public int? IdPdam { get; set; }
        public int? IdDasarLainnya { get; set; }
        public int? IdPegawai { get; set; }
        public decimal? Jumlah { get; set; } = 0;
        public bool? FlagHapus { get; set; } = false;
        public int? IdUserRequest { get; set; }
    }

    public class ParamMasterDasarLainnyaFilterDto : ParamMasterDasarLainnyaDto
    {
        public string NoPegawai { get; set; }
        public string NamaPegawai { get; set; }
    }
}
