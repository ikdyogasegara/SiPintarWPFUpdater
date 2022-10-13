using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using SiPintar.Helpers;
using SiPintar.Utilities.Services.Filter;
using SiPintar.ViewModels.Akuntansi.MasterData.MasterDataAkuntansi;

namespace SiPintar.Commands.Akuntansi.MasterData.MasterDataAkuntansi.SaldoAwalPerkiraan
{
    public class OnLoadCommand : AsyncCommandBase
    {
        private readonly SaldoAwalPerkiraanViewModel _viewModel;
        private readonly bool _isTest;

        public OnLoadCommand(SaldoAwalPerkiraanViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _viewModel.IsCloseDialog = true;

            await UpdateListData.ProcessAsync(_isTest, new List<string>() { "MasterPeriodeAkuntansi", "MasterWilayah" });
            _viewModel.MasterPeriodeAkuntansiList = MasterListGlobal.MasterPeriodeAkuntansi;
            var dataPeriode = _viewModel.MasterPeriodeAkuntansiList.OrderByDescending(x => x.Tahun).Select(x => x.Tahun).Distinct();

            if (dataPeriode != null)
            {
                var tahun = dataPeriode.Select((value, index) => new KeyValuePair<int, string>(index, value.ToString()!));
                _viewModel.TahunList = new ObservableCollection<KeyValuePair<int, string>>(tahun);
            }

            await DialogHelper.ShowDialogGlobalYesCancelViewAsync(
            _isTest,
            true,
            "AkuntansiRootDialog",
            $"Setting Saldo Awal?",
            $"Setting saldo awal perkiraan hanya dilakukan sekali di awal tahun. Jika ada perubahan maka harus dilakukan Posting Bulanan dari awal Tahun",
            "warning",
            _viewModel.OnRefreshCommand,
            "Lanjut",
            "Kembali",
            true,
            false,
            "Akuntansi");

            await Task.FromResult(_isTest);
        }


    }
}
