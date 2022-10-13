using System;

namespace AppBusiness.Data.DTOs
{
    public class MasterDivisiDto
    {
        public int? IdPdam { get; set; }
        public int? IdDivisi { get; set; }
        public string Divisi { get; set; }
        public int? Urutan { get; set; }
        public int? IdDivisiAtas { get; set; }
        public string DivisiAtas { get; set; }
        public int? IdDepartemen { get; set; }
        public string KodeDepartemen { get; set; }
        public string Departemen { get; set; }
        public string KodePer { get; set; }
        public DateTime? WaktuUpdate { get; set; }
    }
}
