using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace AppBusiness.Data.DTOs
{
    public class LaporanPerputaranUangDto : ICloneable
    {
        public int? IdPdam { get; set; }
        public int? IdPerputaranUang { get; set; }
        public int? IdPerputaranUangMaster { get; set; }
        public string Header { get; set; }
        public int? IdKelompok { get; set; }
        public string Uraian { get; set; }
        public int? Tahun { get; set; }
        public int? IdPerkiraan3 { get; set; }
        public string KodePerkiraan3 { get; set; }
        public string NamaPerkiraan3 { get; set; }
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
        public bool? FlagHapus { get; set; } = false;
        public DateTime? WaktuUpdate { get; set; }
        public decimal? TotalAnggaran { get; set; }

        public object Clone()
        {
            return MemberwiseClone();
        }

        #region INotifyPropertyChanged Members
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
