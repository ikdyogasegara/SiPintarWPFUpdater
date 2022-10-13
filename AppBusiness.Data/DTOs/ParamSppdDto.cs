using System;
using System.Collections.Generic;

namespace AppBusiness.Data.DTOs
{
    public class ParamSppdDto
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
        public int? LamaDinas { get; set; } = 0;
        public string Transportasi { get; set; }
        public string BebanAnggaran { get; set; }
        public string Keterangan { get; set; }
        public bool? FlagHapus { get; set; } = false;
        public List<ParamSppdPesertaDto> Peserta { get; set; }
        public List<ParamSppdPesertaBiayaDto> Biaya { get; set; }
        public int? IdUserRequest { get; set; }
    }

    public class ParamSppdPesertaDto
    {
        public int? IdPegawai { get; set; }
    }

    public class ParamSppdPesertaBiayaDto
    {
        public int? IdPegawai { get; set; }
        public int? IdBiayaSppd { get; set; }
        public decimal? Biaya { get; set; } = 0;
        public int? Qty { get; set; }
        public decimal? Jumlah { get; set; } = 0;
        public string Keterangan { get; set; }
    }

    public class ParamSppdFilterDto : ParamSppdDto
    {
        public int? IdPegawai { get; set; }
        public string NoPegawai { get; set; }
        public string NamaPegawai { get; set; }
    }
}
