using System;

namespace AppBusiness.Data.DTOs
{
    public class SppdPesertaBiayaDto : ICloneable
    {
        public int? IdBiayaSppd { get; set; }
        public string Deskripsi { get; set; }
        public decimal? Biaya { get; set; } = 0;
        public int? Qty { get; set; }
        public decimal? Jumlah { get; set; } = 0;
        public string Keterangan { get; set; }
        public DateTime? Waktuupdate { get; set; }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
