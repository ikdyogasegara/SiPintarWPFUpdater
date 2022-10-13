using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using AppBusiness.Data.DTOs;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.Utilities.Services.Filter;
using SiPintar.ViewModels.Akuntansi.MasterData.MasterDataAkuntansi;

namespace SiPintar.Commands.Akuntansi.MasterData.MasterDataAkuntansi.AnggaranPerputaranUang
{
    public class OnLoadCommand : AsyncCommandBase
    {
        private readonly AnggaranPerputaranUangViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnLoadCommand(AnggaranPerputaranUangViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _ = _restApi;

            var dg = await DialogHelper.ShowDialogGlobalLoadingAsync(_isTest, true, "AkuntansiRootDialog", "Mohon tunggu", "sedang memuat data attribute...", "");

            _viewModel.LaporanPerputaranUang = new ObservableCollection<LaporanPerputaranUangDto>();
            _viewModel.DataJenisList = new ObservableCollection<KeyValuePair<string, string>>();
            _viewModel.DataUraianList = new ObservableCollection<LaporanPerputaranUangDto>();
            _viewModel.SelectedDataJenis = new KeyValuePair<string, string>();
            _viewModel.SelectedDataUraian = new LaporanPerputaranUangDto();
            _viewModel.SelectedTahun = new KeyValuePair<int, string>();
            _viewModel.SelectedWilayah = new MasterWilayahDto();

            _viewModel.SelectedJenisAnggaran = parameter.ToString()!;

            _viewModel.IsKonsolidasi = Application.Current.Resources["TipeAkuntansi"].ToString() == "Konsolidasi";

            await UpdateListData.ProcessAsync(_isTest, new List<string>() { "MasterPeriodeAkuntansi", "MasterWilayah" });

            _viewModel.MasterPeriodeAkuntansiList = MasterListGlobal.MasterPeriodeAkuntansi;
            _viewModel.WilayahList = MasterListGlobal.MasterWilayah;

            if (_viewModel.WilayahList[0].IdWilayah != null)
                _viewModel.WilayahList.Insert(0, new MasterWilayahDto { KodeWilayah = "--", NamaWilayah = "KANTOR PUSAT" });

            if (_viewModel.IsKonsolidasi)
                _viewModel.SelectedWilayah = _viewModel.WilayahList[0];

            var dataPeriode = _viewModel.MasterPeriodeAkuntansiList.OrderByDescending(x => x.Tahun).Select(x => x.Tahun).Distinct();

            if (dataPeriode != null)
            {
                var tahun = dataPeriode.Select((value, index) => new KeyValuePair<int, string>(index, value.ToString()!));
                _viewModel.TahunList = new ObservableCollection<KeyValuePair<int, string>>(tahun);
            }

            _viewModel.IsEmpty = true;
            DialogHelper.CloseDialog(_isTest, true, "AkuntansiRootDialog", dg);
            await Task.FromResult(_isTest);
        }
    }
}
