using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Windows.Input;
using AppBusiness.Data.DTOs;
using SiPintar.Commands.Akuntansi.Jurnal.Kas.PenerimaanKas;
using SiPintar.Commands.Global;
using SiPintar.Utilities;

namespace SiPintar.ViewModels.Akuntansi.Jurnal.Kas
{
    public class PenerimaanKasViewModel : VmBase
    {
        public PenerimaanKasViewModel(KasViewModel parentViewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _ = parentViewModel;

            OnLoadCommand = new OnLoadCommand(this, restApi, isTest);
            OnOpenConfirmProsesJurnalCommand = new OnOpenConfirmProsesJurnalCommand(this, restApi, isTest);
            OnProsesJurnalCommand = new OnProsesJurnalCommand(this, restApi, isTest);
            OnCetakDataCommand = new OnCetakDataCommand(this, restApi, isTest);
            OnCetakJpkTransaksiCommand = new OnCetakCommand(restApi, "akuntansi", null!, "Jurnal Penerimaan Kas", ErrorHandlingCetak, isTest);
            OnCetakJpkPerkiraanCommand = new OnCetakCommand(restApi, "akuntansi", null!, "Jurnal Penerimaan Kas", ErrorHandlingCetak, isTest);
            OnCetakJpkPerkiraanRekapCommand = new OnCetakCommand(restApi, "akuntansi", null!, "Jurnal Penerimaan Kas", ErrorHandlingCetak, isTest);
        }

        public ICommand OnOpenConfirmProsesJurnalCommand { get; }
        public ICommand OnProsesJurnalCommand { get; }
        public ICommand OnCetakDataCommand { get; }
        public ICommand OnCetakJpkTransaksiCommand { get; }
        public ICommand OnCetakJpkPerkiraanCommand { get; }
        public ICommand OnCetakJpkPerkiraanRekapCommand { get; }

        private ObservableCollection<MasterPeriodeAkuntansiDto>? _periodeList;
        public ObservableCollection<MasterPeriodeAkuntansiDto>? PeriodeList
        {
            get => _periodeList;
            set { _periodeList = value; OnPropertyChanged(); }
        }

        private ObservableCollection<MasterWilayahDto>? _wilayahList;
        public ObservableCollection<MasterWilayahDto>? WilayahList
        {
            get => _wilayahList;
            set { _wilayahList = value; OnPropertyChanged(); }
        }

        public static IEnumerable<string> UraianList
        {
            get => new List<string>
            {
                TipeUraian.URAIAN_TRANSAKSI,
                TipeUraian.URAIAN_PERKIRAAN,
                TipeUraian.REKAP_URAIAN_PERKIRAAN,
            };
        }

        private string? _selectedUraian;
        public string? SelectedUraian
        {
            get => _selectedUraian;
            set { _selectedUraian = value; OnPropertyChanged(); }
        }

        private MasterWilayahDto? _selectedWilayah;
        public MasterWilayahDto? SelectedWilayah
        {
            get => _selectedWilayah;
            set { _selectedWilayah = value; OnPropertyChanged(); }
        }

        private MasterPeriodeAkuntansiDto? _selectedPeriode;
        public MasterPeriodeAkuntansiDto? SelectedPeriode
        {
            get => _selectedPeriode;
            set { _selectedPeriode = value; OnPropertyChanged(); }
        }

        private bool _isKonsolidasi;
        public bool IsKonsolidasi
        {
            get => _isKonsolidasi;
            set { _isKonsolidasi = value; OnPropertyChanged(); }
        }
    }

    [ExcludeFromCodeCoverage]
    public static class TipeUraian
    {
        public const string URAIAN_TRANSAKSI = "Uraian Transaksi";
        public const string URAIAN_PERKIRAAN = "Uraian Perkiraan";
        public const string REKAP_URAIAN_PERKIRAAN = "Rekap Uraian Perkiraan";
    }
}
