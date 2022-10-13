using System;

namespace AppBusiness.Data.DTOs
{
    public class SummaryHistoriPermohonanDto
    {
        public int? IdPdam { get; set; }
        public string Kategori { get; set; }
        public int? IdPermohonan { get; set; }
        public string NomorPermohonan { get; set; }
        public DateTime? WaktuPermohonan { get; set; }
        public string KodeTipePermohonan { get; set; }
        public string NamaTipePermohonan { get; set; }
        public string NomorSpk { get; set; }
        public DateTime? TanggalSpk { get; set; }
        public string NomorRab { get; set; }
        public DateTime? TanggalRab { get; set; }
        public string NomorBa { get; set; }
        public DateTime? TanggalBa { get; set; }
        public string NamaUser { get; set; }
        public string FotoBuktiPermohonan1 { get; set; }
        public string FotoBuktiPermohonan2 { get; set; }
        public string FotoBuktiPermohonan3 { get; set; }
        public string FotoBuktiSPK1 { get; set; }
        public string FotoBuktiSPK2 { get; set; }
        public string FotoBuktiSPK3 { get; set; }
        public string FotoDenah1 { get; set; }
        public string FotoDenah2 { get; set; }
        public string FotoBuktiRAB1 { get; set; }
        public string FotoBuktiRAB2 { get; set; }
        public string FotoBuktiRAB3 { get; set; }
        public string FotoBuktiBA1 { get; set; }
        public string FotoBuktiBA2 { get; set; }
        public string FotoBuktiBA3 { get; set; }
    }
}
