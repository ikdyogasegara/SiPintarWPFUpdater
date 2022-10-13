using System;
using System.Collections.Generic;

namespace AppBusiness.Data.DTOs
{
    public class ParamBarangMasukDto
    {
        public int? IdPdam { get; set; }
        public int? IdBarangMasuk { get; set; }
        public DateTime? WaktuTransaksi { get; set; }
        public string NomorTransaksi { get; set; }
        public string NomorPengajuanPembelian { get; set; }
        public string NomorSuratJalan { get; set; }
        public string NomorLpb { get; set; }
        public int? IdPeriode { get; set; }
        public int? IdPeriode_Voucher { get; set; }
        public int? IdUser { get; set; }
        public int? IdWilayah { get; set; }
        public string DiGunakanUntuk { get; set; }
        public decimal? TotalAmount { get; set; } = 0;
        public decimal? TotalAmount_Tanpa_Ppn { get; set; } = 0;
        public bool? FlagHapus { get; set; } = false;
        public int? IdUserRequest { get; set; }
        public List<ParamBarangMasukDetailDto> Detail { get; set; }
    }

    public class ParamBarangMasukDetailDto
    {
        public int? IdBarang { get; set; }
        public int? IdGudang { get; set; }
        public int? IdKategoriBarangMasuk { get; set; }
        public decimal? Ppn { get; set; } = 0;
        public decimal? Qty { get; set; } = 0;
        public decimal? Qty_Stock { get; set; } = 0;
        public decimal? Harga { get; set; } = 0;
        public decimal? Jumlah { get; set; } = 0;
        public decimal? Harga_Tanpa_Ppn { get; set; } = 0;
        public decimal? Jumlah_Tanpa_Ppn { get; set; } = 0;
        public DateTime? WaktuDiterima { get; set; }
    }

    public class ParamBarangMasukFilterDto : ParamBarangMasukDto
    {
        public DateTime? WaktuTransaksiMulai { get; set; }
        public DateTime? WaktuTransaksiSampaiDengan { get; set; }
        public string KodeBarang { get; set; }
        public string NamaBarang { get; set; }
    }

    public class ParamBarangMasukSetTglVoucherDto
    {
        public int? IdPdam { get; set; }
        public int? IdBarangMasuk { get; set; }
        public DateTime? TglVoucher { get; set; }
        public int? IdUserRequest { get; set; }
    }

    public class ParamBarangMasukHapusBarangDto
    {
        public int? IdPdam { get; set; }
        public int? IdBarangMasuk { get; set; }
        public int? IdBarang { get; set; }
        public int? IdUserRequest { get; set; }
    }

    public class ParamBarangMasukKoreksiDto
    {
        public int? IdPdam { get; set; }
        public int? IdBarangMasuk { get; set; }
        public int? IdBarang { get; set; }
        public int? IdKategoriBarangMasuk { get; set; }
        public decimal? Qty { get; set; } = 0;
        public decimal? Harga_Tanpa_Ppn { get; set; } = 0;
        public int? IdUserRequest { get; set; }
    }

    public class ParamBarangMasukTambahDto
    {
        public int? IdPdam { get; set; }
        public int? IdBarangMasuk { get; set; }
        public int? IdBarang { get; set; }
        public int? IdKategoriBarangMasuk { get; set; }
        public decimal? Qty { get; set; } = 0;
        public decimal? Harga_Tanpa_Ppn { get; set; } = 0;
        public DateTime? WaktuDiterima { get; set; }
        public int? IdUserRequest { get; set; }
    }
}
