namespace AppBusiness.Data.DTOs
{
    public class ParamMasterBarangDto
    {
        public int? IdPdam { get; set; }
        public int? IdBarang { get; set; }
        public string KodeBarang { get; set; }
        public string NamaBarang { get; set; }
        public int? IdSatuanBarang { get; set; }
        public int? IdJenisBarang { get; set; }
        public int? IdDiameterBarang { get; set; }
        public int? IdKodeAkun { get; set; }
        public string SeriBarang { get; set; }
        public int? MinQty { get; set; }
        public int? MaxQty { get; set; }
        public decimal? HargaBeli { get; set; }
        public decimal? HargaJual { get; set; }
        public string Loker { get; set; }
        public string Foto { get; set; }
        public bool? Status { get; set; } = true;
        public bool? FlagHapus { get; set; } = false;
        public int? IdUserRequest { get; set; }
    }

    public class ParamMasterBarangFilterDto : ParamMasterBarangDto
    {
        public int? IdTipeBarang { get; set; }
        public bool? LihatStock { get; set; }
        public int? IdGudang { get; set; }
    }
}
