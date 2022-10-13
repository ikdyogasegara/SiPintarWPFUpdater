using System;

namespace AppBusiness.Data.DTOs
{
    public class MasterKelurahanDto
    {
        public int? IdPdam { get; set; }
        public int? IdKelurahan { get; set; }
        public string KodeKelurahan { get; set; }
        public string NamaKelurahan { get; set; }
        public int? IdKecamatan { get; set; }
        public string KodeKecamatan { get; set; }
        public string NamaKecamatan { get; set; }
        public int? JumlahJiwa { get; set; } = 0;
        public DateTime? WaktuUpdate { get; set; }
    }
}
