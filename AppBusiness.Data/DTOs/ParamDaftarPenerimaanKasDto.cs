using System;
using System.ComponentModel.DataAnnotations;

namespace AppBusiness.Data.DTOs
{
    public class ParamDaftarPenerimaanKasDto
    {
        public int? IdPdam { get; set; }
        public int? IdDaftarPenerimaanKas { get; set; }
        public int? IdPeriode { get; set; }
        public string NomorTransaksi { get; set; }
        public int? IdWilayah { get; set; }
        public int? IdPerkiraan3Debet { get; set; }
        public string Uraian { get; set; }
        public DateTime? WaktuInput { get; set; }
        public decimal? JumlahNominal { get; set; }
        public int? IdPerkiraan3Kredit { get; set; }
        public int? IdLoket { get; set; }
        public int? Kategori { get; set; }

        public int? SumberData { get; set; }

        public int? IdUserRequest { get; set; }

        public bool? FlagHapus { get; set; } = false;
    }
    public class ParamDaftarPenerimaanKasFilterDto : ParamDaftarPenerimaanKasDto
    {
        public string KodePerkiraan { get; set; }
        public string NamaPerkiraan { get; set; }

        public DateTime? TanggalMulai { get; set; }
        public DateTime? TanggalSampaiDengan { get; set; }
        public DateTime? WaktuUpdate { get; set; }

    }
}
