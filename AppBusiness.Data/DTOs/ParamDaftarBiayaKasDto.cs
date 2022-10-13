using System;

namespace AppBusiness.Data.DTOs
{
    public class ParamDaftarBiayaKasDto
    {
        public int? IdPdam { get; set; }
        public int? IdDaftarBiayaKas { get; set; }
        public int? IdLoket { get; set; }
        public int? IdPeriode { get; set; }
        public int? KodePeriode { get; set; }
        public string? NamaPeriode { get; set; }
        public string? NomorTransaksi { get; set; }
        public int? IdWilayah { get; set; }
        public string? KodeWilayah { get; set; }
        public string? NamaWilayah { get; set; }
        public int? IdPerkiraan3Debet { get; set; }
        public string? KodePerkiraan3Debet { get; set; }
        public string? NamaPerkiraan3Debet { get; set; }
        public string? Uraian { get; set; }
        public DateTime WaktuInput { get; set; }
        public decimal? JumlahNominal { get; set; }
        public int? IdJenisHutang { get; set; }
        public string? JenisHutang { get; set; }
        public int? IdPerkiraan3Kredit { get; set; }
        public string? KodePerkiraan3Kredit { get; set; }
        public string? NamaPerkiraan3Kredit { get; set; }
        public int? Kategori { get; set; }
        public int? SumberData { get; set; }
        public bool? FlagHapus { get; set; } = false;
        public int? IdUserRequest { get; set; }
    }

    public class ParamDaftarBiayaKasFilterDto : ParamDaftarBiayaKasDto
    {
        public DateTime? WaktuInputStart { get; set; }
        public DateTime? WaktuInputEnd { get; set; }
    }
}
