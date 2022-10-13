using System;
using System.Collections.Generic;
using System.Text;

namespace AppBusiness.Data.DTOs
{
    public class ParamJurnalRekeningAirDto
    {
        public int? IdPdam { get; set; }
        public int? IdJurnalRekAir { get; set; }
        public int? IdPeriode { get; set; }
        public int? IdGolongan { get; set; }
        public int? IdPerkiraan { get; set; }
        public int? Tahun { get; set; }
        public decimal? JumlahRekening { get; set; }
        public decimal? JumlahPakai { get; set; }
        public decimal? BiayaTotal { get; set; }
        public decimal? BiayaAir { get; set; }
        public decimal? BiayaAdministrasi { get; set; }
        public decimal? BiayaPemeliharaan { get; set; }
        public decimal? BiayaRetribusi { get; set; }
        public decimal? BiayaMeterai { get; set; }
        public DateTime? WaktuPosting { get; set; }
        public bool? FlagHapus { get; set; }
    }
    public class ParamJurnalRekeningAirFilterDto : ParamJurnalRekeningAirDto
    {
        public DateTime? WaktuUpdate { get; set; }
    }
}
