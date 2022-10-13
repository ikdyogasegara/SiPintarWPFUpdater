using System;
using System.Collections.Generic;

namespace AppBusiness.Data.DTOs
{
    public class ParamDownloadHasilBacaDto
    {
        public int? IdUserRequest { get; set; }
        public int? IdPdam { get; set; }
        public int? IdPeriode { get; set; }
        public bool? LangsungPublish { get; set; }
        public List<ParamDownloadHasilBacaListDataDto> ListData { get; set; }
    }

    public class ParamDownloadHasilBacaListDataDto
    {
        public int? IdPelangganAir { get; set; }
        public decimal? StanSkrg { get; set; } = 0;
        public decimal? StanLalu { get; set; } = 0;
        public decimal? StanAngkat { get; set; } = 0;
        public decimal? Pakai { get; set; } = 0;
        public decimal? PakaiKalkulasi { get; set; } = 0;
        public decimal? BiayaPemakaian { get; set; } = 0;
        public decimal? Administrasi { get; set; } = 0;
        public decimal? Pemeliharaan { get; set; } = 0;
        public decimal? Retribusi { get; set; } = 0;
        public decimal? Pelayanan { get; set; } = 0;
        public decimal? AirLimbah { get; set; } = 0;
        public decimal? DendaPakai0 { get; set; } = 0;
        public decimal? AdministrasiLain { get; set; } = 0;
        public decimal? PemeliharaanLain { get; set; } = 0;
        public decimal? RetribusiLain { get; set; } = 0;
        public decimal? Ppn { get; set; } = 0;
        public decimal? Meterai { get; set; } = 0;
        public decimal? Rekair { get; set; } = 0;
        public decimal? Denda { get; set; } = 0;
        public decimal? Total { get; set; } = 0;
        public bool? Taksir { get; set; } = false;
        public bool? Taksasi { get; set; } = false;
        public bool? FlagBaca { get; set; } = true;
        public string PetugasBaca { get; set; }
        public string Kelainan { get; set; }
        public DateTime? WaktuBaca { get; set; }

        public bool? FlagKoreksi { get; set; } = true;
        public DateTime? WaktuKoreksi { get; set; }
        public bool? FlagVerifikasi { get; set; } = true;
        public DateTime? WaktuVerifikasi { get; set; }
        public decimal? Blok1 { get; set; } = 0;
        public decimal? Blok2 { get; set; } = 0;
        public decimal? Blok3 { get; set; } = 0;
        public decimal? Blok4 { get; set; } = 0;
        public decimal? Blok5 { get; set; } = 0;

        public decimal? Prog1 { get; set; } = 0;
        public decimal? Prog2 { get; set; } = 0;
        public decimal? Prog3 { get; set; } = 0;
        public decimal? Prog4 { get; set; } = 0;
        public decimal? Prog5 { get; set; } = 0;

        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public bool? AdaFotoRumah { get; set; } = false;
        public bool? AdaVideo { get; set; } = false;
        public string Memo { get; set; }
    }
}
