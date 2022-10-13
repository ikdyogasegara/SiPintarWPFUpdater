using System;

namespace AppBusiness.Data.DTOs
{
    public class MasterJenisTunjanganDto
    {
        public int? IdPdam { get; set; }
        public int? IdJenisTunjangan { get; set; }
        public string KodeJenisTunjangan { get; set; }
        public string NamaJenisTunjangan { get; set; }
        public int? Urutan { get; set; } = 0;
        public string Tipe { get; set; }
        public DateTime? WaktuUpdate { get; set; }
    }
}
