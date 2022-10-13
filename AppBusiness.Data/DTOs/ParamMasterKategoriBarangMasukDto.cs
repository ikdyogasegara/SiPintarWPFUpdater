namespace AppBusiness.Data.DTOs
{
    public class ParamMasterKategoriBarangMasukDto
    {
        public int? IdPdam { get; set; }
        public int? IdKategoriBarangMasuk { get; set; }
        public string Kategori { get; set; }
        public decimal? Ppn { get; set; } = 0;
        public bool? Flag { get; set; } = false;
        public bool? FlagHapus { get; set; } = false;
        public int? IdUserRequest { get; set; }
    }

    public class ParamMasterKategoriBarangMasukFilterDto : ParamMasterKategoriBarangMasukDto
    {
    }
}
