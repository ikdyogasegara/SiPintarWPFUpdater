using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace AppBusiness.Data.DTOs
{
    public class TagihanGlobalWpf : TagihanGlobalDto, INotifyPropertyChanged
    {
        private bool _isUpdating { get; set; }
        public bool IsUpdating
        {
            get { return _isUpdating; }
            set
            {
                _isUpdating = value;
                OnPropertyChanged();
            }
        }

        private bool _isSelected { get; set; } = true;
        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                _isSelected = value;
                OnPropertyChanged();
            }
        }

        private int? _groupTagihan { get; set; }
        public int? GroupTagihan
        {
            get { return _groupTagihan; }
            set
            {
                _groupTagihan = value;
                OnPropertyChanged();
            }
        }

        public decimal? DendaWpf
        {
            get { return Denda; }
            set
            {
                Denda = value;
                OnPropertyChanged();
            }
        }

        public decimal? TotalWpf
        {
            get { return Total; }
            set
            {
                Total = value;
                OnPropertyChanged();
            }
        }


        public decimal? TagihanWpf
        {
            get
            {
                if (JenisTagihan == "Air")
                {
                    return Rekair;
                }

                if (JenisTagihan == "Non Air")
                {
                    return Total;
                }

                return 0;
            }
        }

        public string NomorWpf
        {
            get
            {
                if (JenisTagihan == "Air")
                {
                    return NoSamb;
                }

                if (JenisTagihan == "Non Air")
                {
                    return NomorNonAir;
                }

                return "-";
            }
        }

        private string _uraianWpf { get; set; }
        public string UraianWpf
        {
            get { return _uraianWpf; }
            set
            {
                _uraianWpf = value;
                OnPropertyChanged();
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
