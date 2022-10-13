using System;
using System.Collections.Generic;

namespace AppBusiness.Data.DTOs
{
    public class ParamBarangPenyesuaianDto
    {
        public int? IdPdam { get; set; }
        public string NomorTransaksi { get; set; }
        public DateTime? WaktuTransaksi { get; set; }
        public string Kategori { get; set; }
        public string Catatan { get; set; }
        public int? IdGudang { get; set; }
        public int? IdUser { get; set; }
        public int? IdUserRequest { get; set; }
        public List<ParamBarangPenyesuaianDetailDto> Detail { get; set; }
    }

    public class ParamBarangPenyesuaianDetailDto
    {
        public int? Id { get; set; }
        public int? IdBarang { get; set; }
        public decimal? Qty { get; set; } = 0;
        public decimal? Harga { get; set; } = 0;
        public DateTime? WaktuDiterima { get; set; }
    }

    public class ParamBarangPenyesuaianFilterDto : ParamBarangPenyesuaianDto
    {
        public int? IdBarangPenyesuaian { get; set; }
        public int? IdBarang { get; set; }
        public string KodeBarang { get; set; }
        public string NamaBarang { get; set; }
        public DateTime? WaktuTransaksiMulai { get; set; }
        public DateTime? WaktuTransaksiSampaiDengan { get; set; }
    }

    public class ParamBarangPenyesuaianDeleteDto
    {
        public int? IdBarangPenyesuaian { get; set; }
        public int? IdPdam { get; set; }
        public int? IdUserRequest { get; set; }
    }
}
