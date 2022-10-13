using System;

namespace AppBusiness.Data.DTOs
{
    public class ParamMasterGolonganDto : ICloneable
    {
        public int? IdPdam { get; set; }
        public int? IdGolongan { get; set; }
        public string KodeGolongan { get; set; }
        public string NamaGolongan { get; set; }
        public int? PeriodeMulaiBerlaku { get; set; }
        public string Kategori { get; set; }
        public string Uraian { get; set; }
        public string NomorSK { get; set; }
        public decimal? Administrasi { get; set; }
        public decimal? Pemeliharaan { get; set; }
        public decimal? Pelayanan { get; set; }
        public decimal? Retribusi { get; set; }
        public decimal? RetribusiPakai { get; set; }
        public decimal? AirLimbah { get; set; }
        public decimal? MinPakai { get; set; }
        public decimal? DendaPakai0 { get; set; }
        public decimal? MinDenda { get; set; }
        public decimal? Ppn { get; set; }
        public decimal? Bb1 { get; set; }
        public decimal? Bb2 { get; set; }
        public decimal? Bb3 { get; set; }
        public decimal? Bb4 { get; set; }
        public decimal? Bb5 { get; set; }
        public decimal? Ba1 { get; set; }
        public decimal? Ba2 { get; set; }
        public decimal? Ba3 { get; set; }
        public decimal? Ba4 { get; set; }
        public decimal? Ba5 { get; set; }
        public decimal? T1 { get; set; }
        public decimal? T2 { get; set; }
        public decimal? T3 { get; set; }
        public decimal? T4 { get; set; }
        public decimal? T5 { get; set; }
        public bool? T1Tetap { get; set; }
        public bool? T2Tetap { get; set; }
        public bool? T3Tetap { get; set; }
        public bool? T4Tetap { get; set; }
        public bool? T5Tetap { get; set; }
        public decimal? DendaTunggakan1 { get; set; }
        public decimal? DendaTunggakan2 { get; set; }
        public decimal? DendaTunggakan3 { get; set; }
        public decimal? DendaTunggakan4 { get; set; }
        public decimal? DendaTunggakanPerBulan { get; set; }
        public bool? TrfDendaBerdasarkanPersen { get; set; }
        public bool? Status { get; set; }
        public bool? FlagHapus { get; set; } = false;
        public int? IdUserRequest { get; set; }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }

    public class ParamMasterGolonganFilterDto : ParamMasterGolonganDto
    {
        public DateTime? WaktuUpdate { get; set; }
    }
}
