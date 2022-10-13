using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using AppBusiness.Data.DTOs;
using NPOI.POIFS.Properties;
using SiPintar.Commands.Gudang.MasterData.KategoriBarangMasuk;
using SiPintar.Helpers.TableHelper;
using SiPintar.Utilities;


namespace SiPintar.ViewModels.Gudang.MasterData
{
    public class KategoriBarangMasukViewModel : ViewModelBase
    {
        public KategoriBarangMasukViewModel(MasterDataViewModel parent, IRestApiClientModel restApi, bool isTest = false)
        {
            _ = parent;
            _isNamaKategoriBarangMasukChecked = false;
            _filterNamaKategoriBarangMasuk = null!;
            _isAdd = false;

            OnOpenAddFormCommand = new OnOpenAddFormCommand(this, isTest);
            OnSubmitAddFormCommand = new OnSubmitAddFormCommand(this, restApi, isTest);
            OnOpenEditFormCommand = new OnOpenEditFormCommand(this, isTest);
            OnSubmitEditFormCommand = new OnSubmitEditFormCommand(this, restApi, isTest);
            OnOpenDeleteFormCommand = new OnOpenDeleteFormCommand(this, isTest);
            OnSubmitDeleteFormCommand = new OnSubmitDeleteFormCommand(this, restApi, isTest);
            OnResetFilterCommand = new OnResetFilterCommand(this, isTest);

            DataKategoriBarangMasuk = new TableKategoriBarangMasuk(
                    url: "master-kategori-barang-masuk",
                    restApi: restApi,
                    moduleName: "gudang",
                    onOpenEditFormCommand: OnOpenEditFormCommand,
                    onOpenDeleteFormCommand: OnOpenDeleteFormCommand,
                    getFilter: () =>
                    {
                        var param = new Dictionary<string, dynamic>();
                        if (IsNamaKategoriBarangMasukChecked)
                        {
                            param.Add("Kategori", FilterNamaKategoriBarangMasuk ?? "");
                        }
                        return param;
                    },
                    isTest: isTest
                );

            OnRefreshCommand = DataKategoriBarangMasuk.RefreshPageCommand;
            OnTerapkanFilterCommand = DataKategoriBarangMasuk.FirstPageCommand;
        }

        public ICommand OnOpenAddFormCommand { get; }
        public ICommand OnSubmitAddFormCommand { get; }
        public ICommand OnOpenEditFormCommand { get; }
        public ICommand OnSubmitEditFormCommand { get; }
        public ICommand OnOpenDeleteFormCommand { get; }
        public ICommand OnSubmitDeleteFormCommand { get; }
        public ICommand OnResetFilterCommand { get; }
        public ICommand OnRefreshCommand { get; }
        public ICommand OnTerapkanFilterCommand { get; }

        private bool _isAdd;
        public bool IsAdd
        {
            get => _isAdd;
            set
            {
                _isAdd = value;
                OnPropertyChanged();
            }
        }

        public TableKategoriBarangMasuk DataKategoriBarangMasuk { get; }

        public ObservableCollection<MasterKategoriBarangMasukDto> Data { get => DataKategoriBarangMasuk.GetList<MasterKategoriBarangMasukDto>()!; }
        public MasterKategoriBarangMasukDto SelectedData { get => DataKategoriBarangMasuk.GetSelected<MasterKategoriBarangMasukDto>()!; }

        private bool _isNamaKategoriBarangMasukChecked;
        public bool IsNamaKategoriBarangMasukChecked
        {
            get { return _isNamaKategoriBarangMasukChecked; }
            set
            {
                _isNamaKategoriBarangMasukChecked = value;
                OnPropertyChanged();
            }
        }

        private string _filterNamaKategoriBarangMasuk;
        public string FilterNamaKategoriBarangMasuk
        {
            get { return _filterNamaKategoriBarangMasuk; }
            set
            {
                _filterNamaKategoriBarangMasuk = value;
                OnPropertyChanged();
            }
        }

        private KeyValuePair<bool, string>? _flag;
        public KeyValuePair<bool, string>? Flag
        {
            get { return _flag; }
            set
            {
                _flag = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<KeyValuePair<bool, string>> DaftarFlag { get; set; } = new ObservableCollection<KeyValuePair<bool, string>>()
        {
            new KeyValuePair<bool, string>(key: true, value: "Ya"),
            new KeyValuePair<bool, string>(key: false, value: "Tidak")
        };
    }

    public class TableKategoriBarangMasuk : TableHelper
    {
        public ICommand OnOpenEditFormCommand { get; }
        public ICommand OnOpenDeleteFormCommand { get; }
        public TableKategoriBarangMasuk(string url, IRestApiClientModel restApi, string moduleName, ICommand onOpenEditFormCommand, ICommand onOpenDeleteFormCommand,
            Func<Dictionary<string, dynamic>>? getFilter = null, bool isTest = false)
            : base(url, restApi, moduleName, getFilter, isTest)
        {
            OnOpenEditFormCommand = onOpenEditFormCommand;
            OnOpenDeleteFormCommand = onOpenDeleteFormCommand;
        }
    }
}
