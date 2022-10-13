using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace AppBusiness.Data.DTOs
{
    public class DaftarBiayaKasDto : ICloneable
    {
        public int? IdPdam { get; set; }
        public int? IdDaftarBiayaKas { get; set; }
        public int? IdPeriode { get; set; }
        public int? KodePeriode { get; set; }
        public string NamaPeriode { get; set; }
        public string NomorTransaksi { get; set; }
        public int? IdWilayah { get; set; }
        public string KodeWilayah { get; set; }
        public string NamaWilayah { get; set; }
        public int? IdPerkiraan3Debet { get; set; }
        public string KodePerkiraan3Debet { get; set; }
        public string NamaPerkiraan3Debet { get; set; }
        public string Uraian { get; set; }
        public DateTime WaktuInput { get; set; }
        public decimal? JumlahNominal { get; set; }
        public int? IdJenisHutang { get; set; }
        public string JenisHutang { get; set; }
        public int? IdPerkiraan3Kredit { get; set; }
        public string KodePerkiraan3Kredit { get; set; }
        public string NamaPerkiraan3Kredit { get; set; }
        public int? Kategori { get; set; }
        public int? SumberData { get; set; }
        public bool? FlagHapus { get; set; } = false;
        public DateTime? WaktuUpdate { get; set; }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
    public class DaftarBiayaKasDataGridDto : DaftarBiayaKasDto, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public string StatusPostingText { get; set; }

        public string KodeWilayahWpf
        {
            get { return KodeWilayah; }
            set
            {
                KodeWilayah = value;
                OnPropertyChanged();
            }
        }

        public string NamaWilayahWpf
        {
            get { return NamaWilayah; }
            set
            {
                NamaWilayah = value;
                OnPropertyChanged();
            }
        }

        public string KodePerkiraan3DebetWpf
        {
            get { return KodePerkiraan3Debet; }
            set
            {
                KodePerkiraan3Debet = value;
                OnPropertyChanged();
            }
        }

        public string NamaPerkiraan3DebetWpf
        {
            get { return NamaPerkiraan3Debet; }
            set
            {
                NamaPerkiraan3Debet = value;
                OnPropertyChanged();
            }
        }

        public string KodePerkiraan3KreditWpf
        {
            get { return KodePerkiraan3Kredit; }
            set
            {
                KodePerkiraan3Kredit = value;
                OnPropertyChanged();
            }
        }

        public string NamaPerkiraan3KreditWpf
        {
            get { return NamaPerkiraan3Kredit; }
            set
            {
                NamaPerkiraan3Kredit = value;
                OnPropertyChanged();
            }
        }

        public string UraianWpf
        {
            get { return Uraian; }
            set
            {
                Uraian = value;
                OnPropertyChanged();
            }
        }

        public DateTime WaktuInputWpf
        {
            get { return WaktuInput; }
            set
            {
                WaktuInput = value;
                OnPropertyChanged();
            }
        }

        public decimal? JumlahNominalWpf
        {
            get { return JumlahNominal; }
            set
            {
                JumlahNominal = value;
                OnPropertyChanged();
            }
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
