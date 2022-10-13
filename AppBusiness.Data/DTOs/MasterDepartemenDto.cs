using System;

namespace AppBusiness.Data.DTOs
{
    public class MasterDepartemenDto
    {
        public int? IdPdam { get; set; }
        public int? IdDepartemen { get; set; }
        public string KodeDepartemen { get; set; }
        public string Departemen { get; set; }
        public int? Urutan { get; set; }
        public int? Flag { get; set; }
        public DateTime? WaktuUpdate { get; set; }
    }
}
