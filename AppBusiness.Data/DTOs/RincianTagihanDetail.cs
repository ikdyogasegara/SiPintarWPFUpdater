using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace AppBusiness.Data.DTOs
{
    [ExcludeFromCodeCoverage]
    public class RincianTagihanDetail : INotifyPropertyChanged
    {
        private decimal _biayaPemakaian = 0;
        public decimal BiayaPemakaian
        {
            get { return _biayaPemakaian; }
            set
            {
                _biayaPemakaian = value;
                OnPropertyChanged();
            }
        }

        private decimal _administrasi = 0;
        public decimal Administrasi
        {
            get { return _administrasi; }
            set
            {
                _administrasi = value;
                OnPropertyChanged();
            }
        }

        private decimal _pemeliharaan = 0;
        public decimal Pemeliharaan
        {
            get { return _pemeliharaan; }
            set
            {
                _pemeliharaan = value;
                OnPropertyChanged();
            }
        }

        private decimal _retribusi = 0;
        public decimal Retribusi
        {
            get { return _retribusi; }
            set
            {
                _retribusi = value;
                OnPropertyChanged();
            }
        }

        private decimal _pelayanan = 0;
        public decimal Pelayanan
        {
            get { return _pelayanan; }
            set
            {
                _pelayanan = value;
                OnPropertyChanged();
            }
        }

        private decimal _airLimbah = 0;
        public decimal AirLimbah
        {
            get { return _airLimbah; }
            set
            {
                _airLimbah = value;
                OnPropertyChanged();
            }
        }

        private decimal _dendaPakai0 = 0;
        public decimal DendaPakai0
        {
            get { return _dendaPakai0; }
            set
            {
                _dendaPakai0 = value;
                OnPropertyChanged();
            }
        }

        private decimal _ppn = 0;
        public decimal Ppn
        {
            get { return _ppn; }
            set
            {
                _ppn = value;
                OnPropertyChanged();
            }
        }

        private decimal _meterai = 0;
        public decimal Meterai
        {
            get { return _meterai; }
            set
            {
                _meterai = value;
                OnPropertyChanged();
            }
        }

        private decimal _rekAir = 0;
        public decimal RekAir
        {
            get { return _rekAir; }
            set
            {
                _rekAir = value;
                OnPropertyChanged();
            }
        }

        private decimal _denda = 0;
        public decimal Denda
        {
            get { return _denda; }
            set
            {
                _denda = value;
                OnPropertyChanged();
            }
        }

        private decimal _lainnya = 0;
        public decimal Lainnya
        {
            get { return _lainnya; }
            set
            {
                _lainnya = value;
                OnPropertyChanged();
            }
        }

        private decimal _diskon = 0;
        public decimal Diskon
        {
            get { return _diskon; }
            set
            {
                _diskon = value;
                OnPropertyChanged();
            }
        }

        private decimal _lembarTagihan = 0;
        public decimal LembarTagihan
        {
            get { return _lembarTagihan; }
            set
            {
                _lembarTagihan = value;
                OnPropertyChanged();
            }
        }

        private decimal _totalRekeningAir = 0;
        public decimal TotalRekeningAir
        {
            get { return _totalRekeningAir; }
            set
            {
                _totalRekeningAir = value;
                OnPropertyChanged();
            }
        }


        private decimal _totalRekeningLimbah = 0;
        public decimal TotalRekeningLimbah
        {
            get { return _totalRekeningLimbah; }
            set
            {
                _totalRekeningLimbah = value;
                OnPropertyChanged();
            }
        }


        private decimal _totalRekeningLltt = 0;
        public decimal TotalRekeningLltt
        {
            get { return _totalRekeningLltt; }
            set
            {
                _totalRekeningLltt = value;
                OnPropertyChanged();
            }
        }


        private decimal _totalRekeningNonAir = 0;
        public decimal TotalRekeningNonAir
        {
            get { return _totalRekeningNonAir; }
            set
            {
                _totalRekeningNonAir = value;
                OnPropertyChanged();
            }
        }

        private decimal _totalAngsuran = 0;
        public decimal TotalAngsuran
        {
            get { return _totalAngsuran; }
            set
            {
                _totalAngsuran = value;
                OnPropertyChanged();
            }
        }

        private decimal _totalAngsuranAir = 0;
        public decimal TotalAngsuranAir
        {
            get { return _totalAngsuranAir; }
            set
            {
                _totalAngsuranAir = value;
                OnPropertyChanged();
            }
        }

        private decimal _totalAngsuranNonAir = 0;
        public decimal TotalAngsuranNonAir
        {
            get { return _totalAngsuranNonAir; }
            set
            {
                _totalAngsuranNonAir = value;
                OnPropertyChanged();
            }
        }

        public decimal TotalTagihan
        {
            get
            {
                return TotalRekeningAir +
                       TotalRekeningLimbah +
                       TotalRekeningLltt +
                       TotalRekeningNonAir +
                       TotalAngsuran;
            }

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
