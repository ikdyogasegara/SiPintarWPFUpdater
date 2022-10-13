using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using AppBusiness.Data.DTOs;
using SiPintar.Commands.Gudang.ProsesTransaksi.PeriodePosting;
using SiPintar.Utilities;

namespace SiPintar.ViewModels.Gudang.ProsesTransaksi
{
    public class PeriodePostingViewModel : VmBase
    {
        public PeriodePostingViewModel(ProsesTransaksiViewModel _parent, IRestApiClientModel _restApi, bool _isTest = false)
        {
            _ = _parent;
            OnLoadCommand = new OnLoadCommand(this, _restApi, _isTest);
            OnRefreshCommand = new OnRefreshCommand(this, _restApi, _isTest);
            OnOpenBukaPeriodePostingCommand = new OnOpenBukaPeriodePostingCommand(this, _isTest);
            OnBukaPeriodePostingCommand = new OnBukaPeriodePostingCommand(this, _restApi, _isTest);
            OnOpenTutupPeriodePostingCommand = new OnOpenTutupPeriodePostingCommand(this, _isTest);
            OnTutupPeriodePostingCommand = new OnTutupPeriodePostingCommand(this, _restApi, _isTest);
            OnOpenAddFormCommand = new OnOpenAddFormCommand(this, _isTest);
            OnSubmitAddFormCommand = new OnSubmitAddFormCommand(this, _restApi, _isTest);
            OnOpenConfirmationAddCommand = new OnOpenConfirmationAddCommand(this, _isTest);


            //initialize daftar tahun
            var minTahun = DateTime.Now.Year - 5;
            var maxTahun = DateTime.Now.Year + 5;
            var daftarTahunTemp = new ObservableCollection<KeyValuePair<int, string>>();
            for (var i = minTahun; i <= maxTahun; i++)
            {
                daftarTahunTemp.Add(new KeyValuePair<int, string>(i, i.ToString()));
            }
            DaftarTahun = daftarTahunTemp;
        }

        public ICommand OnOpenBukaPeriodePostingCommand;
        public ICommand OnOpenTutupPeriodePostingCommand;
        public ICommand OnBukaPeriodePostingCommand;
        public ICommand OnTutupPeriodePostingCommand;
        public ICommand OnOpenConfirmationAddCommand;

        private ObservableCollection<MasterPeriodeGudangDto> _data;
        public ObservableCollection<MasterPeriodeGudangDto> Data
        {
            get { return _data; }
            set
            {
                _data = value;
                OnPropertyChanged();
            }
        }

        private MasterPeriodeGudangDto _selectedData;
        public MasterPeriodeGudangDto SelectedData
        {
            get { return _selectedData; }
            set
            {
                _selectedData = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<KeyValuePair<int, string>> _daftarBulan = new ObservableCollection<KeyValuePair<int, string>>() {
            new KeyValuePair<int, string>(1, "Januari"),
            new KeyValuePair<int, string>(2, "Februari"),
            new KeyValuePair<int, string>(3, "Maret"),
            new KeyValuePair<int, string>(4, "April"),
            new KeyValuePair<int, string>(5, "Mei"),
            new KeyValuePair<int, string>(6, "Juni"),
            new KeyValuePair<int, string>(7, "Juli"),
            new KeyValuePair<int, string>(8, "Agustus"),
            new KeyValuePair<int, string>(9, "September"),
            new KeyValuePair<int, string>(10, "Oktober"),
            new KeyValuePair<int, string>(11, "November"),
            new KeyValuePair<int, string>(12, "Desember"),
        };
        public ObservableCollection<KeyValuePair<int, string>> DaftarBulan
        {
            get { return _daftarBulan; }
            set
            {
                _daftarBulan = value;
                OnPropertyChanged();
            }
        }

        private KeyValuePair<int, string>? _bulanPeriode;
        public KeyValuePair<int, string>? BulanPeriode
        {
            get { return _bulanPeriode; }
            set
            {
                _bulanPeriode = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<KeyValuePair<int, string>> _daftarTahun;
        public ObservableCollection<KeyValuePair<int, string>> DaftarTahun
        {
            get { return _daftarTahun; }
            set
            {
                _daftarTahun = value;
                OnPropertyChanged();
            }
        }

        private KeyValuePair<int, string>? _tahunPeriode;
        public KeyValuePair<int, string>? TahunPeriode
        {
            get { return _tahunPeriode; }
            set
            {
                _tahunPeriode = value;
                OnPropertyChanged();
            }
        }
    }
}
