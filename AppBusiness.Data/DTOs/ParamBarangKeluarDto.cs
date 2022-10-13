using System;
using System.Collections.Generic;

namespace AppBusiness.Data.DTOs
{
    public class ParamBarangKeluarDto
    {
        public int? IdPdam { get; set; }
        public int? IdBarangKeluar { get; set; }
        public DateTime? WaktuTransaksi { get; set; }
        public string NomorTransaksi { get; set; }
        public string NomorPengajuanPembelian { get; set; }
        public int? IdPeriode { get; set; }
        public int? IdUser { get; set; }
        public int? IdGudang { get; set; }
        public int? IdKategoriBarangKeluar { get; set; }
        public int? IdBagianMemintaBarang { get; set; }
        public string DiGunakanUntuk { get; set; }
        public decimal? TotalAmount { get; set; } = 0;
        public bool? FlagHapus { get; set; } = false;
        public int? IdUserRequest { get; set; }
        public List<ParamBarangKeluarDetailDto> Detail { get; set; }
    }

    public class ParamBarangKeluarDetailDto
    {
        public int? IdBarang { get; set; }
        public int? IdGudang { get; set; }
        public decimal? Qty { get; set; } = 0;
        public decimal? Harga_Jual { get; set; } = 0;
        public decimal? Amount { get; set; } = 0;
        public DateTime? WaktuDikeluarkan { get; set; }
        public int? IdKeperluan { get; set; }
    }

    public class ParamBarangKeluarFilterDto : ParamBarangKeluarDto
    {
        public DateTime? WaktuTransaksiMulai { get; set; }
        public DateTime? WaktuTransaksiSampaiDengan { get; set; }
        public string KodeBarang { get; set; }
        public string NamaBarang { get; set; }
        public bool? FlagVerifikasi { get; set; }
        public int? FlagVerifikasi_Selesai { get; set; }
        public int? IdKeperluan { get; set; }
    }

    public class ParamBarangKeluarSetTglVoucherDto
    {
        public int? IdPdam { get; set; }
        public int? IdBarangKeluar { get; set; }
        public DateTime? TglVoucher { get; set; }
        public int? IdUserRequest { get; set; }
    }

    public class ParamBarangKeluarHapusBarangDto
    {
        public int? IdPdam { get; set; }
        public int? IdBarangKeluar { get; set; }
        public int? IdBarang { get; set; }
        public int? IdUserRequest { get; set; }
    }

    public class ParamBarangKeluarVerifikasiDto
    {
        public int? IdPdam { get; set; }
        public int? IdBarangKeluar { get; set; }
        public int? IdKeperluan { get; set; }
        public int? IdUserRequest { get; set; }
    }

    public class ParamBarangKeluarUnVerifikasiDto
    {
        public int? IdPdam { get; set; }
        public int? IdBarangKeluar { get; set; }
        public int? IdUserRequest { get; set; }
    }


    public class ParamBarangKeluarVerifikasiPerBarangDto
    {
        public int? IdPdam { get; set; }
        public int? IdBarangKeluar { get; set; }
        public int? IdUserRequest { get; set; }
        public List<ParamIdKeperluanDto> Detail { get; set; }
    }

    public class ParamIdKeperluanDto
    {
        public int? IdBarang { get; set; }
        public int? IdKeperluan { get; set; }
    }

    public class ParamBarangKeluarTambahDto
    {
        public int? IdPdam { get; set; }
        public int? IdBarangKeluar { get; set; }
        public int? IdBarang { get; set; }
        public decimal? Qty { get; set; } = 0;
        public decimal? Harga_Jual { get; set; } = 0;
        public DateTime? WaktuDiKeluarkan { get; set; }
        public int? IdUserRequest { get; set; }
    }
}
