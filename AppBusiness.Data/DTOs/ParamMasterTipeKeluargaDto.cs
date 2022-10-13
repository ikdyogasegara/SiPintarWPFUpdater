namespace AppBusiness.Data.DTOs
{
    public class ParamMasterTipeKeluargaDto
    {
        public int? IdPdam { get; set; }
        public int? IdTipeKeluarga { get; set; }
        public string KodeTipeKeluarga { get; set; }
        public bool? FlagKawin { get; set; } = false;
        public int? JumlahAnak { get; set; } = 0;
        public string Uraian { get; set; }
        public bool? FlagPersenPangan { get; set; } = false;
        public decimal? NominalPangan { get; set; } = 0;
        public bool? FlagPersenKel { get; set; } = false;
        public decimal? NominalKel { get; set; } = 0;
        public decimal? Ptkp { get; set; } = 0;
        public bool? FlagHapus { get; set; } = false;
        public int? IdUserRequest { get; set; }
    }

    public class ParamMasterTipeKeluargaFilterDto : ParamMasterTipeKeluargaDto
    {
    }
}
