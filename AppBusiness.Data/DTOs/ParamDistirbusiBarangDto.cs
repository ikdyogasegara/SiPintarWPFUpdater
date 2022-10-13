using System;

namespace AppBusiness.Data.DTOs
{
    public class ParamDistribusiBarangDto
    {
        public int? IdPdam { get; set; }
        public int? IdBarangMasuk { get; set; }
        public DateTime? WaktuTransaksi { get; set; }
        public string NomorTransaksi { get; set; }
        public string NomorPengajuanPembelian { get; set; }
        public string NomorSuratJalan { get; set; }
        public string NomorLpb { get; set; }
        public int? Periode { get; set; }
        public int? PeriodeVoucher { get; set; }
        public int? IdUser { get; set; }
        public int? IdWilayah { get; set; }
        public string DiGunakanUntuk { get; set; }
        public decimal? TotalAmount_Dengan_Ppn { get; set; } = 0;
        public decimal? TotalAmount_Tanpa_Ppn { get; set; } = 0;
        public bool? FlagHapus { get; set; } = false;
        public int? IdUserRequest { get; set; }
    }

    public class ParamDistribusiBarangFilterDto : ParamDistribusiBarangDto
    {
        public DateTime? WaktuTransaksiMulai { get; set; }
        public DateTime? WaktuTransaksiSampaiDengan { get; set; }
        public int? IdBarang { get; set; }
        public string KodeBarang { get; set; }
        public string NamaBarang { get; set; }
        public int? IdGudang { get; set; }
        public bool? IncludeStock { get; set; } = false;
    }

    public class ParamDistribusiBarangDraftDto
    {
        public int? IdPdam { get; set; }
        public int? IdBarang { get; set; }
        public int? IdGudang { get; set; }
        public decimal? Qty { get; set; } = 0;
        public int? IdUserRequest { get; set; }
    }

    public class ParamDistribusiBarangHapusDto
    {
        public int? IdPdam { get; set; }
        public int? IdBarang { get; set; }
        public int? IdUserRequest { get; set; }
    }

    public class ParamDistribusiProsesDto
    {
        public int? IdPdam { get; set; }
        public int? IdGudang { get; set; }
        public int? IdGudang_Tujuan { get; set; }
        public int? IdUserRequest { get; set; }
    }
}
