using System;

namespace AppBusiness.Data.DTOs
{
    public class DaftarPenerimaanKasDto
    {
        public int? IdPdam { get; set; }
        public int? IdDaftarPenerimaanKas { get; set; }

        public int? IdPeriode { get; set; }
        public string NomorTransaksi { get; set; }

        public int? IdWilayah { get; set; }

        public string KodeWilayah { get; set; }

        public string NamaWilayah { get; set; }

        public int? IdPerkiraan3Debet { get; set; }

        public string NamaPerkiraan3Debet { get; set; }

        public string Uraian { get; set; }

        public DateTime? WaktuInput { get; set; }

        public decimal? JumlahNominal { get; set; }

        public int? IdPerkiraan3Kredit { get; set; }

        public string KodePerkiraanKredit { get; set; }
        public string NamaPerkiraan3Kredit { get; set; }

        public int? Kategori { get; set; }
        public int? SumberData { get; set; }

        public bool? FlagHapus { get; set; }
        public DateTime? WaktuUpdate { get; set; }
    }

    public class DaftarPenerimaanKasDataGridDto : DaftarPenerimaanKasDto
    {
        public string StatusPostingText { get; set; }
    }
}
