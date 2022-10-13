using System;

namespace AppBusiness.Data.DTOs
{
    public class PermohonanPelangganAirSpkCetakDto
    {
        public string NomorSpk { get; set; }
        public DateTime TanggalSpk { get; set; } = DateTime.Now;
        public string NamaUser { get; set; }
        public string FotoBukti1 { get; set; }
        public string FotoBukti2 { get; set; }
        public string FotoBukti3 { get; set; }
        public string AlasanBatal { get; set; }
    }
}
