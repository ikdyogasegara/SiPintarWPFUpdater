namespace AppBusiness.Data.DTOs
{
    public class ParamMasterKategoriBarangKeluarDto
    {
        public int? IdPdam { get; set; }
        public int? IdKategoriBarangKeluar { get; set; }
        public string Kategori { get; set; }
        public string KodeNomor { get; set; }
        public bool? FlagHapus { get; set; } = false;
        public int? IdUserRequest { get; set; }
    }

    public class ParamMasterKategoriBarangKeluarFilterDto : ParamMasterKategoriBarangKeluarDto
    {
    }
}
