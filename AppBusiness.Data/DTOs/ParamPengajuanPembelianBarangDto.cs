using System;
using System.Collections.Generic;

namespace AppBusiness.Data.DTOs
{
    public class ParamPengajuanPembelianBarangDto
    {
        public int? IdPdam { get; set; }
        public int? IdPengajuanPembelian { get; set; }
        public string NomorPengajuanPembelian { get; set; }
        public int? IdGudang { get; set; }
        public DateTime? TglPengajuan { get; set; }
        public string DiGunakanUntuk { get; set; }
        public int? IdKategoriBarangMasuk { get; set; }
        public int? IdUser { get; set; }
        public bool? FlagHapus { get; set; } = false;
        public int? IdUserRequest { get; set; }
        public List<ParamPengajuanPembelianBarangDetailDto> Detail { get; set; }
    }

    public class ParamPengajuanPembelianBarangDetailDto
    {
        public int? IdBarang { get; set; }
        public decimal? Qty { get; set; } = 0;
        public decimal? Stock { get; set; } = 0;
    }

    public class ParamPengajuanPembelianBarangFilterDto : ParamPengajuanPembelianBarangDto
    {
        public int? IdBarang { get; set; }
        public DateTime? TglPengajuanMulai { get; set; }
        public DateTime? TglPengajuanSampaiDengan { get; set; }
        public bool? FlagSelesai { get; set; }
    }

    public class ParamPengajuanPembelianBarangTentukanHargaDto
    {
        public int? IdPdam { get; set; }
        public int? IdPengajuanPembelian { get; set; }
        public int? IdBarang { get; set; }
        public int? IdKategoriBarangMasuk { get; set; }
        public decimal? Qty { get; set; } = 0;
        public decimal? Harga { get; set; } = 0;
        public int? IdUserRequest { get; set; }
    }

    public class ParamPengajuanPembelianBarangVerifikasiUnVerifikasiDto
    {
        public int? IdPdam { get; set; }
        public int? IdPengajuanPembelian { get; set; }
        public int? IdUserRequest { get; set; }
    }

    public class ParamPengajuanPembelianBarangOPDto
    {
        public int? IdPdam { get; set; }
        public int? IdPengajuanPembelian { get; set; }
        public DateTime? Tgl_Voucher { get; set; }
        public string Order_Pembelian_Nomor { get; set; }
        public DateTime? Order_Pembelian_Tgl_Laporan { get; set; }
        public int? Order_Pembelian_IdSuplier { get; set; }
        public int? Order_Pembelian_Rating_Suplier { get; set; }
        public int? IdUserRequest { get; set; }
    }

    public class ParamPengajuanPembelianBarangSuratPerjanjianDto
    {
        public int? IdPdam { get; set; }
        public int? IdPengajuanPembelian { get; set; }
        public DateTime? Tgl_Voucher { get; set; }
        public string Surat_Perjanjian_Nomor { get; set; }
        public DateTime? Surat_Perjanjian_Tgl_Laporan { get; set; }
        public int? Surat_Perjanjian_IdSuplier { get; set; }
        public int? Surat_Perjanjian_Rating_Suplier { get; set; }
        public string Surat_Perjanjian_Berita_Acara { get; set; }
        public int? IdUserRequest { get; set; }
    }

    public class ParamPengajuanPembelianBarangDiterimaTempDto
    {
        public int? IdPdam { get; set; }
        public int? IdPengajuanPembelian { get; set; }
        public int? IdBarang { get; set; }
        public decimal? Qty { get; set; } = 0;
        public DateTime? WaktuDiterima { get; set; }
        public int? IdUserRequest { get; set; }
    }

    public class ParamPengajuanPembelianBarangKembalikanKeDpbTempDto
    {
        public int? IdPdam { get; set; }
        public int? IdPengajuanPembelian { get; set; }
        public int? IdBarang { get; set; }
        public int? IdUserRequest { get; set; }
    }

    public class ParamPengajuanPembelianBarangTambahBarangDto
    {
        public int? IdPdam { get; set; }
        public int? IdPengajuanPembelian { get; set; }
        public int? IdBarang { get; set; }
        public int? Qty { get; set; }
        public int? Stock { get; set; }
        public int? IdUser { get; set; }
        public int? IdUserRequest { get; set; }
    }

    public class ParamPengajuanPembelianBarangProsesKePersediaanDto
    {
        public int? IdPdam { get; set; }
        public int? IdPengajuanPembelian { get; set; }
        public DateTime? WaktuBarangMasuk { get; set; }
        public string NomorLpb { get; set; }
        public string NomorSuratJalan { get; set; }
        public int? IdGudang { get; set; }
        public int? IdPeriode { get; set; }
        public int? IdPeriode_Voucher { get; set; }
        public int? IdUserRequest { get; set; }
    }

    public class ParamPengajuanPembelianBarangTanggalVoucherDto
    {
        public int? IdPdam { get; set; }
        public int? IdPengajuanPembelian { get; set; }
        public DateTime? Tgl_Voucher { get; set; }
        public int? IdUserRequest { get; set; }
    }

    public class ParamPengajuanPembelianBarangBatalkanTanggalVoucherDto
    {
        public int? IdPdam { get; set; }
        public int? IdPengajuanPembelian { get; set; }
        public int? IdUserRequest { get; set; }
    }
}
