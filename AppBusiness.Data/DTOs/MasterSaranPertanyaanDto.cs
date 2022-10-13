using System;

namespace AppBusiness.Data.DTOs
{
    public class MasterSaranPertanyaanDto
    {
        public int? IdPertanyaan { get; set; }
        public string Pertanyaan { get; set; }
        public DateTime? WaktuCreate { get; set; }
        public DateTime? WaktuUpdate { get; set; }
        public virtual bool IsSelected { get; set; }
    }
}
