using System;

namespace AppBusiness.Data.DTOs
{
    public class SetoranLoketDto
    {
        public int? IdPdam { get; set; }
        public int? IdSetoranLoket { get; set; }
        public DateTime? TglPenerimaan { get; set; }
        public int? IdLoket { get; set; }
        public string KodeLoket { get; set; }
        public string NamaLoket { get; set; }
        public decimal? Penerimaan { get; set; }
        public decimal? Setoran { get; set; }
        public decimal? Selisih { get; set; }
        public DateTime? TglSetor { get; set; }
        public int? IdBank { get; set; }
        public string NamaBank { get; set; }
        public string Keterangan { get; set; }
        public int? IdUser { get; set; }
        public string Nama { get; set; }
        public string Foto { get; set; }
        public DateTime? WaktuUpdate { get; set; }
    }
}
