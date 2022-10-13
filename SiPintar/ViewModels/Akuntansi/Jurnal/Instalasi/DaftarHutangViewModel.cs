using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Windows.Input;
using AppBusiness.Data.DTOs;
using SiPintar.Commands.Akuntansi.Jurnal.Instalasi.DaftarHutang;
using SiPintar.Commands.Global;
using SiPintar.Utilities;

namespace SiPintar.ViewModels.Akuntansi.Jurnal.Instalasi
{
    public class DaftarHutangViewModel : ViewModelBase
    {
        public DaftarHutangViewModel(InstalasiViewModel parentViewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _ = parentViewModel;

            OnLoadCommand = new OnLoadCommand(this, restApi, isTest);
            OnOpenConfirmProsesJurnalCommand = new OnOpenConfirmProsesJurnalCommand(this, restApi, isTest);
            OnProsesJurnalCommand = new OnProsesJurnalCommand(this, restApi, isTest);
            OnCetakDataCommand = new OnCetakDataCommand(this, restApi, isTest);
            OnCetakCommand = new OnCetakCommand(restApi, "akuntansi", null!, "Jurnal Daftar Hutang Sudah & Harus Dibayar", ErrorHandlingCetak, isTest);
        }

        public ICommand OnLoadCommand { get; }
        public ICommand OnOpenConfirmProsesJurnalCommand { get; }
        public ICommand OnProsesJurnalCommand { get; }
        public ICommand OnCetakDataCommand { get; }
        public ICommand OnCetakCommand { get; }

        private DateTime? _periodeAwal;
        public DateTime? PeriodeAwal
        {
            get => _periodeAwal;
            set { _periodeAwal = value; OnPropertyChanged(); }
        }

        private DateTime? _periodeAkhir;
        public DateTime? PeriodeAkhir
        {
            get => _periodeAkhir;
            set { _periodeAkhir = value; OnPropertyChanged(); }
        }

        private ObservableCollection<MasterWilayahDto>? _wilayahList;
        public ObservableCollection<MasterWilayahDto>? WilayahList
        {
            get => _wilayahList;
            set { _wilayahList = value; OnPropertyChanged(); }
        }

        public static IEnumerable<string> ProsesList
        {
            get => new List<string>
            {
                TipeProses.DHHD,
                TipeProses.SISA_HUTANG,
                TipeProses.DHJP,
                TipeProses.DHHD_POTRAIT,
            };
        }

        private string? _selectedProses;
        public string? SelectedProses
        {
            get => _selectedProses;
            set { _selectedProses = value; OnPropertyChanged(); }
        }

        private MasterWilayahDto? _selectedWilayah;
        public MasterWilayahDto? SelectedWilayah
        {
            get => _selectedWilayah;
            set { _selectedWilayah = value; OnPropertyChanged(); }
        }

        private bool _isKonsolidasi;
        public bool IsKonsolidasi
        {
            get => _isKonsolidasi;
            set { _isKonsolidasi = value; OnPropertyChanged(); }
        }
    }

    [ExcludeFromCodeCoverage]
    public static class TipeProses
    {
        public const string DHHD = "DHHD";
        public const string SISA_HUTANG = "Sisa Hutang";
        public const string DHJP = "DHJP";
        public const string DHHD_POTRAIT = "DHHD Potrait";
    }
}
