using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using AppBusiness.Data.DTOs;
using SiPintar.Commands.Akuntansi.MasterData.MasterDataKeuangan.SaldoAwal;
using SiPintar.Utilities;

namespace SiPintar.ViewModels.Akuntansi.MasterData.MasterDataKeuangan
{
    public class SaldoAwalViewModel : ViewModelBase
    {
        public SaldoAwalViewModel(MasterDataKeuanganViewModel parentViewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _ = parentViewModel;

            OnLoadCommand = new OnLoadCommand(this, isTest);
            OnRefreshCommand = new OnRefreshCommand(this, restApi, isTest);
            OnOpenEditFormCommand = new OnOpenEditFormCommand(this, isTest);
            OnConfirmFormCommand = new OnConfirmFormCommand(this, isTest);
            OnSubmitEditFormCommand = new OnSubmitEditFormCommand(this, restApi, isTest);
        }

        public ICommand OnLoadCommand { get; }
        public ICommand OnRefreshCommand { get; }
        public ICommand OnOpenEditFormCommand { get; }
        public ICommand OnConfirmFormCommand { get; }
        public ICommand OnSubmitEditFormCommand { get; }

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

        private bool _isAdd;
        public bool IsAdd
        {
            get => _isAdd;
            set { _isAdd = value; OnPropertyChanged(); }
        }

        private MasterSaldoAwalRekonBankDto? _data;
        public MasterSaldoAwalRekonBankDto? Data
        {
            get => _data;
            set { _data = value; OnPropertyChanged(); }
        }

        private MasterSaldoAwalRekonBankDto? _saldoAwalForm;
        public MasterSaldoAwalRekonBankDto? SaldoAwalForm
        {
            get => _saldoAwalForm;
            set { _saldoAwalForm = value; OnPropertyChanged(); }
        }

        private ParamSaldoAwalRekonBankDto? _paramSaldoAwalForm;
        public ParamSaldoAwalRekonBankDto? ParamSaldoAwalForm
        {
            get => _paramSaldoAwalForm;
            set { _paramSaldoAwalForm = value; OnPropertyChanged(); }
        }

        private ObservableCollection<MasterBankDto>? _bankList;
        public ObservableCollection<MasterBankDto>? BankList
        {
            get => _bankList;
            set { _bankList = value; OnPropertyChanged(); }
        }

        private DateTime? _filterTglRekonsiliasi;
        public DateTime? FilterTglRekonsiliasi
        {
            get => _filterTglRekonsiliasi;
            set
            {
                _filterTglRekonsiliasi = value;
                OnPropertyChanged();
                OnRefreshCommand.Execute(null);
            }
        }

        private decimal? _totalSaldoAwal = decimal.Zero;
        public decimal? TotalSaldoAwal
        {
            get => _totalSaldoAwal;
            set { _totalSaldoAwal = value; OnPropertyChanged(); }
        }

    }

}
