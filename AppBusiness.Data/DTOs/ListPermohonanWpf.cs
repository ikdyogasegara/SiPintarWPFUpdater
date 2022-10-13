using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace AppBusiness.Data.DTOs
{
    [ExcludeFromCodeCoverage]
    public class ListPermohonanWpf : ListPermohonanDto, INotifyPropertyChanged
    {
        public int? JumlahPelangganAirWpf
        {
            get { return JumlahPelangganAir; }
            set
            {
                JumlahPelangganAir = value;
                OnPropertyChanged();
            }
        }

        public int? JumlahPelangganLimbahWpf
        {
            get { return JumlahPelangganLimbah; }
            set
            {
                JumlahPelangganLimbah = value;
                OnPropertyChanged();
            }
        }

        public int? JumlahPelangganLlttWpf
        {
            get { return JumlahPelangganLltt; }
            set
            {
                JumlahPelangganLltt = value;
                OnPropertyChanged();
            }
        }

        public int? JumlahNonPelangganWpf
        {
            get { return JumlahNonPelanggan; }
            set
            {
                JumlahNonPelanggan = value;
                OnPropertyChanged();
            }
        }

        public int? JumlahWpf
        {
            get { return Jumlah; }
            set
            {
                Jumlah = value;
                OnPropertyChanged();
            }
        }

        public string IsAdaDataWpf
        {
            get
            {
                if (JumlahWpf > 0)
                {
                    return "Yes";
                }
                else
                {
                    return "No";
                }
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
