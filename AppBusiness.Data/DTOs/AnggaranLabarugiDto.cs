using System;

namespace AppBusiness.Data.DTOs
{

    public class AnggaranLabaRugiDto : ICloneable
    {
        public int? IdPdam { get; set; }
        public int? IdAnggaranLabaRugi { get; set; }
        public int? IdGroupLabaRugi { get; set; }
        public string Header { get; set; }
        public string Uraian { get; set; }
        public int? Tahun { get; set; }
        public int? IdPerkiraan3 { get; set; }
        public string KodePerkiraan { get; set; }
        public string NamaPerkiraan { get; set; }
        public int? IdWilayah { get; set; }
        public string KodeWilayah { get; set; }
        public string NamaWilayah { get; set; }
        public decimal? Anggaran1 { get; set; }
        public decimal? Anggaran2 { get; set; }
        public decimal? Anggaran3 { get; set; }
        public decimal? Anggaran4 { get; set; }
        public decimal? Anggaran5 { get; set; }
        public decimal? Anggaran6 { get; set; }
        public decimal? Anggaran7 { get; set; }
        public decimal? Anggaran8 { get; set; }
        public decimal? Anggaran9 { get; set; }
        public decimal? Anggaran10 { get; set; }
        public decimal? Anggaran11 { get; set; }
        public decimal? Anggaran12 { get; set; }
        public decimal? Realisasi1 { get; set; }
        public decimal? Realisasi2 { get; set; }
        public decimal? Realisasi3 { get; set; }
        public decimal? Realisasi4 { get; set; }
        public decimal? Realisasi5 { get; set; }
        public decimal? Realisasi6 { get; set; }
        public decimal? Realisasi7 { get; set; }
        public decimal? Realisasi8 { get; set; }
        public decimal? Realisasi9 { get; set; }
        public decimal? Realisasi10 { get; set; }
        public decimal? Realisasi11 { get; set; }
        public decimal? Realisasi12 { get; set; }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
