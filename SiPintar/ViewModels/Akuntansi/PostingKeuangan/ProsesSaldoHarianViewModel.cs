using System;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Windows.Input;
using AppBusiness.Data.DTOs;
using SiPintar.Commands.Akuntansi.PostingKeuangan.ProsesSaldoHarian;
using SiPintar.Utilities;

namespace SiPintar.ViewModels.Akuntansi.PostingKeuangan
{
    public class ProsesSaldoHarianViewModel : ViewModelBase
    {
        public ProsesSaldoHarianViewModel(PostingKeuanganViewModel parentViewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _ = parentViewModel;

            OnLoadCommand = new OnLoadCommand(this, restApi, isTest);
            OnRefreshCommand = new OnRefreshCommand(this, restApi, isTest);
            OnSubmitFormCommand = new OnSubmitFormCommand(this, restApi, isTest);

        }

        public ICommand OnLoadCommand { get; }
        public ICommand OnRefreshCommand { get; }
        public ICommand OnSubmitFormCommand { get; }

        private bool _isLoading;
        public bool IsLoading
        {
            get => _isLoading;
            set { _isLoading = value; OnPropertyChanged(); }
        }

        private bool _isLoadingForm;
        public bool IsLoadingForm
        {
            get => _isLoadingForm;
            set { _isLoadingForm = value; OnPropertyChanged(); }
        }

        private bool _isEmpty;
        public bool IsEmpty
        {
            get => _isEmpty;
            set { _isEmpty = value; OnPropertyChanged(); }
        }

        private bool _isAdd;
        public bool IsAdd
        {
            get => _isAdd;
            set { _isAdd = value; OnPropertyChanged(); }
        }

        private ObservableCollection<SaldoSetorDto>? _dataList;
        public ObservableCollection<SaldoSetorDto>? DataList
        {
            get => _dataList;
            set { _dataList = value; OnPropertyChanged(); }
        }

        private ParamSaldoSetorDto? _prosesSaldoHarianForm;
        public ParamSaldoSetorDto? ProsesSaldoHarianForm
        {
            get => _prosesSaldoHarianForm;
            set { _prosesSaldoHarianForm = value; OnPropertyChanged(); }
        }

        private DateTime? _tglKasSebelumnya = DateTime.Now.AddDays(-1);
        public DateTime? TglKasSebelumnya
        {
            get => _tglKasSebelumnya;
            set { _tglKasSebelumnya = value; OnPropertyChanged(); }
        }

        private DateTime? _tglKasHariIni = DateTime.Now;
        public DateTime? TglKasHariIni
        {
            get => _tglKasHariIni;
            set { _tglKasHariIni = value; OnPropertyChanged(); }
        }

        private decimal? _totalPenyetoranBank;
        public decimal? TotalPenyetoranBank
        {
            get => _totalPenyetoranBank;
            set { _totalPenyetoranBank = value; OnPropertyChanged(); }
        }

        private ObservableCollection<MasterBankDto>? _bankList;
        public ObservableCollection<MasterBankDto>? BankList
        {
            get => _bankList;
            set { _bankList = value; OnPropertyChanged(); }
        }

        private ObservableCollection<MasterLoketDto>? _loketList;
        public ObservableCollection<MasterLoketDto>? LoketList
        {
            get => _loketList;
            set { _loketList = value; OnPropertyChanged(); }
        }

        private MasterLoketDto? _selectedLoket;
        public MasterLoketDto? SelectedLoket
        {
            get => _selectedLoket;
            set { _selectedLoket = value; OnPropertyChanged(); }
        }
    }
}
