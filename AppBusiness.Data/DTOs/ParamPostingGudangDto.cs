namespace AppBusiness.Data.DTOs
{
    public class ParamPostingGudangRekapStockBarangDto
    {
        public int? IdPdam { get; set; }
        public int? IdPeriode { get; set; }
        public bool? RekapStockdanPembelian { get; set; }
        public bool? HanyaPembelian { get; set; }
        public int? IdUserRequest { get; set; }
    }

    public class ParamPostingGudangKartuStockBarangDto
    {
        public int? IdPdam { get; set; }
        public int? IdPeriode { get; set; }
        public int? IdUserRequest { get; set; }
    }
}
