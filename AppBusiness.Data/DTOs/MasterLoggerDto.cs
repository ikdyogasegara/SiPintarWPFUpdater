using System;

namespace AppBusiness.Data.DTOs
{
    public class MasterLoggerDto
    {
        public int? ID { get; set; }
        public int? IdPdam { get; set; }
        public string NamaPdam { get; set; }
        public int? IdUser { get; set; }
        public string Nama { get; set; }
        public string NamaRole { get; set; }
        public bool? Sukses { get; set; }
        public string Tipe { get; set; }
        public string Log { get; set; }
        public string Value { get; set; }
        public DateTime? WaktuUpdate { get; set; }
    }
}
