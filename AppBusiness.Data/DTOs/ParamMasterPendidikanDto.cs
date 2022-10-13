namespace AppBusiness.Data.DTOs
{
    public class ParamMasterPendidikanDto
    {
        public int? IdPdam { get; set; }
        public int? IdPendidikan { get; set; }
        public string Pendidikan { get; set; }
        public int? Urutan { get; set; }
        public int? IdGolonganPegawaiMax { get; set; }
        public int? GolMaxIndek { get; set; } = 0;
        public int? IdGolonganPegawaiMin { get; set; }
        public int? GolMinIndek { get; set; } = 0;
        public bool? FlagHapus { get; set; } = false;
        public int? IdUserRequest { get; set; }
    }

    public class ParamMasterPendidikanFilterDto : ParamMasterPendidikanDto
    {
    }
}
