using System;

namespace AppBusiness.Data.DTOs
{
    public class MasterTipeKeluargaDto
    {
        public int? IdPdam { get; set; }
        public int? IdTipeKeluarga { get; set; }
        public string KodeTipeKeluarga { get; set; }
        public bool? FlagKawin { get; set; } = false;
        public int? JumlahAnak { get; set; } = 0;
        public string Uraian { get; set; }
        public bool? FlagPersenPangan { get; set; }
        public decimal? NominalPangan { get; set; }
        public bool? FlagPersenKel { get; set; }
        public decimal? NominalKel { get; set; }
        public decimal? Ptkp { get; set; } = 0;
        public DateTime? WaktuUpdate { get; set; }
    }
}
