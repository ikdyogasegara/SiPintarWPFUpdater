using System;
using System.Collections.Generic;

namespace AppBusiness.Data.DTOs
{
    public class ParamBarangHibahDto
    {
        public int? IdPdam { get; set; }
        public string NomorTransaksi { get; set; }
        public DateTime? WaktuTransaksi { get; set; }
        public string OpsiHibah { get; set; }
        public string Catatan { get; set; }
        public int? IdGudang { get; set; }
        public int? IdUser { get; set; }
        public int? IdUserRequest { get; set; }
        public List<ParamBarangHibahDetailDto> Detail { get; set; }
    }

    public class ParamBarangHibahDetailDto
    {
        public int? IdBarang { get; set; }
        public decimal? Qty { get; set; } = 0;
        public decimal? Harga { get; set; } = 0;
    }

    public class ParamBarangHibahFilterDto : ParamBarangHibahDto
    {
        public int? IdBarangHibah { get; set; }
        public int? IdBarang { get; set; }
        public string KodeBarang { get; set; }
        public string NamaBarang { get; set; }
        public DateTime? WaktuTransaksiMulai { get; set; }
        public DateTime? WaktuTransaksiSampaiDengan { get; set; }
    }

    public class ParamBarangHibahDeleteDto
    {
        public int? IdBarangHibah { get; set; }
        public int? IdPdam { get; set; }
        public int? IdUserRequest { get; set; }
    }
}
