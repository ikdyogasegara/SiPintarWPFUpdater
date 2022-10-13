using System;
using System.Collections.Generic;

namespace AppBusiness.Data.DTOs
{
    public class PermohonanPelangganAirSpkDto
    {
        public int? IdPdam { get; set; }
        public int? IdPermohonan { get; set; }
        public string NomorSpk { get; set; }
        public DateTime? TanggalSpk { get; set; }
        public int? IdUser { get; set; }
        public string NamaUser { get; set; }
        public string FotoBukti1 { get; set; }
        public string FotoBukti2 { get; set; }
        public string FotoBukti3 { get; set; }
        public bool? FlagBatal { get; set; }
        public int? IdAlasanBatal { get; set; }
        public string AlasanBatal { get; set; }
        public DateTime? WaktuUpdate { get; set; }
        public List<PermohonanPelangganAirRabDetailDto> SppbDariSpk { get; set; }
    }
}
