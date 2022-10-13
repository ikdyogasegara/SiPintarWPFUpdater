﻿using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace AppBusiness.Data.DTOs
{
    public class SaldoAwalPerkiraanDto : ICloneable
    {
        public int? IdPdam { get; set; }
        public int? IdSaldoPerkiraan { get; set; }
        public int? IdWilayah { get; set; }
        public string KodeWilayah { get; set; }
        public string NamaWilayah { get; set; }
        public int? IdPerkiraan1 { get; set; }
        public string KodePerkiraan1 { get; set; }
        public string NamaPerkiraan1 { get; set; }
        public int? IdPerkiraan2 { get; set; }
        public string KodePerkiraan2 { get; set; }
        public string NamaPerkiraan2 { get; set; }
        public int? IdPerkiraan3 { get; set; }
        public string KodePerkiraan3 { get; set; }
        public string NamaPerkiraan3 { get; set; }
        public int? Tahun { get; set; }
        public decimal? DebetAwal { get; set; }
        public decimal? KreditAwal { get; set; }
        public decimal? SaldoAkhir { get; set; }
        public decimal? Bln00 { get; set; }
        public decimal? Bln01 { get; set; }
        public decimal? Bln02 { get; set; }
        public decimal? Bln03 { get; set; }
        public decimal? Bln04 { get; set; }
        public decimal? Bln05 { get; set; }
        public decimal? Bln06 { get; set; }
        public decimal? Bln07 { get; set; }
        public decimal? Bln08 { get; set; }
        public decimal? Bln09 { get; set; }
        public decimal? Bln10 { get; set; }
        public decimal? Bln11 { get; set; }
        public decimal? Bln12 { get; set; }
        public DateTime? TglSaldo { get; set; }
        public bool? FlagHapus { get; set; } = false;
        public DateTime? WaktuUpdate { get; set; }


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
