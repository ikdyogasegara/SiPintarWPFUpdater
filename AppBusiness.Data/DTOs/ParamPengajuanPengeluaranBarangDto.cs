using System;
using System.Collections.Generic;

namespace AppBusiness.Data.DTOs
{
    public class ParamPengajuanPengeluaranBarangDto
    {
        public int? IdPdam { get; set; }
        public int? IdPengajuanPengeluaran { get; set; }
        public string NomorPengajuanPengeluaran { get; set; }
        public string NomorRegistrasi { get; set; }
        public int? IdGudang { get; set; }
        public int? IdBagianMemintaBarang { get; set; }
        public int? IdKategoriBarangKeluar { get; set; }
        public DateTime? TglPengajuan { get; set; }
        public string DiGunakanUntuk { get; set; }
        public int? IdUser { get; set; }
        public bool? FlagHapus { get; set; } = false;
        public int? IdUserRequest { get; set; }
        public List<ParamPengajuanPengeluaranBarangDetailDto> Detail { get; set; }
    }

    public class ParamPengajuanPengeluaranBarangDetailDto
    {
        public int? IdBarang { get; set; }
        public decimal? Qty { get; set; } = 0;
        public decimal? Harga_Jual { get; set; }
        public string Keterangan { get; set; }
    }

    public class ParamPengajuanPengeluaranBarangFilterDto : ParamPengajuanPengeluaranBarangDto
    {
        public int? IdBarang { get; set; }
        public DateTime? TglPengajuanMulai { get; set; }
        public DateTime? TglPengajuanSampaiDengan { get; set; }
        public bool? FlagSelesai { get; set; }
        public int? IdPeriode { get; set; }
    }

    public class ParamPengajuanPengeluaranBarangKoreksiQtyDikeluarkanDto
    {
        public int? IdPdam { get; set; }
        public int? IdPengajuanPengeluaran { get; set; }
        public int? IdBarang { get; set; }
        public decimal? Qty { get; set; } = 0;
        public int? IdUserRequest { get; set; }
    }

    public class ParamPengajuanPengeluaranBarangTambahBarangDto
    {
        public int? IdPdam { get; set; }
        public int? IdPengajuanPengeluaran { get; set; }
        public int? IdBarang { get; set; }
        public decimal? Qty { get; set; }
        public decimal? Harga_Jual { get; set; }
        public int? IdUser { get; set; }
        public int? IdUserRequest { get; set; }
    }

    public class ParamPengajuanPengeluaranBarangProsesKeDaftarBarangKeluarDto
    {
        public int? IdPdam { get; set; }
        public int? IdPengajuanPengeluaran { get; set; }
        public string NomorBpp { get; set; }
        public int? IdGudang { get; set; }
        public int? IdPeriode { get; set; }
        public DateTime? WaktuDikeluarkan { get; set; }
        public int? IdUserRequest { get; set; }
    }
}
