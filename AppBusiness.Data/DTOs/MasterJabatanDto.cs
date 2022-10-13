using System;

namespace AppBusiness.Data.DTOs
{
    public class MasterJabatanDto
    {
        public int? IdPdam { get; set; }
        public int? IdJabatan { get; set; }
        public string Jabatan { get; set; }
        public int? Urutan { get; set; }
        public DateTime? WaktuUpdate { get; set; }
    }
}
