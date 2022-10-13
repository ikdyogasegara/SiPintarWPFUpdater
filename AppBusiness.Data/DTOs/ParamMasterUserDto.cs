using System;

namespace AppBusiness.Data.DTOs
{
    public class ParamMasterUserDto
    {
        public int? IdPdam { get; set; }
        public int? IdUser { get; set; }
        public string Nama { get; set; }
        public string NamaUser { get; set; }
        public string Password { get; set; }
        public bool? Aktif { get; set; }
        public string NoIdentitas { get; set; }
        public int? IdRole { get; set; }
        public int? IdLoket { get; set; }
    }

    public class ParamMasterUserFilterDto : ParamMasterUserDto
    {
        public bool IncludeAkses { get; set; }
        public string NamaRole { get; set; }
        public int? IdModule { get; set; }
        public DateTime? WaktuUpdate { get; set; }
    }
}
