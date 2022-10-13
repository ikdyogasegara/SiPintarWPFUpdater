using System;
using System.Collections.Generic;

namespace AppBusiness.Data.DTOs
{
    public class MasterUserDto
    {
        public int? IdUser { get; set; }
        public int? IdPdam { get; set; }
        public string Nama { get; set; }
        public string NamaUser { get; set; }
        public string PasswordUser { get; set; }
        public bool? Aktif { get; set; }
        public string NoIdentitas { get; set; }
        public int? IdRole { get; set; }
        public string NamaRole { get; set; }
        public int? IdModule { get; set; }
        public int? IdLoket { get; set; }
        public string KodeLoket { get; set; }
        public string NamaLoket { get; set; }
        public List<MasterUserRoleAccessDto> Akses { get; set; }
        public DateTime? WaktuUpdate { get; set; }
        public bool? IncludeAkses { get; set; }
    }
}
