using System;

namespace AppBusiness.Data.DTOs
{
    public class ParamMasterDivisiDto
    {
        public int? IdPdam { get; set; }
        public int? IdDivisi { get; set; }
        public string Divisi { get; set; }
        public int? Urutan { get; set; } = 0;
        public int? IdDivisiAtas { get; set; }
        public int? IdDepartemen { get; set; }
        public string KodePer { get; set; }
        public bool? FlagHapus { get; set; } = false;
        public DateTime? WaktuUpdate { get; set; }
        public int? IdUserRequest { get; set; }
    }

    public class ParamMasterDivisiFilterDto : ParamMasterDivisiDto
    {
    }
}
