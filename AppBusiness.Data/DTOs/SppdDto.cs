using System;
using System.Collections.Generic;

namespace AppBusiness.Data.DTOs
{
    public class SppdDto
    {
        public int? IdPdam { get; set; }
        public int? IdSppd { get; set; }
        public string NoSpt { get; set; }
        public string NoSppd { get; set; }
        public string Dasar { get; set; }
        public string Keperluan { get; set; }
        public string TempatBerangkat { get; set; }
        public string TempatTujuan { get; set; }
        public DateTime? TglBerangkat { get; set; }
        public DateTime? TglKembali { get; set; }
        public int? LamaDinas { get; set; }
        public string Transportasi { get; set; }
        public string BebanAnggaran { get; set; }
        public DateTime? WaktuInput { get; set; }
        public string Keterangan { get; set; }
        public DateTime? Waktuupdate { get; set; }
        public List<SppdPesertaDto> Peserta { get; set; }
    }
}
