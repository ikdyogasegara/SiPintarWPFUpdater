using System;

namespace AppBusiness.Data.DTOs
{
    public class MasterKecamatanDto
    {
        public int? IdPdam { get; set; }
        public int? IdKecamatan { get; set; }
        public string KodeKecamatan { get; set; }
        public string NamaKecamatan { get; set; }
        public int? IdCabang { get; set; }
        public string KodeCabang { get; set; }
        public string NamaCabang { get; set; }
        public DateTime? WaktuUpdate { get; set; }
    }
}
