using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using SiPintar.Helpers;
using SiPintar.Utilities.Services.Filter;
using SiPintar.ViewModels.Akuntansi.PostingKeuangan;

namespace SiPintar.Commands.Akuntansi.PostingKeuangan.PengeluaranLainnya
{
    public class OnLoadCommand : AsyncCommandBase
    {
        private readonly PengeluaranLainnyaViewModel _viewModel;
        private readonly bool _isTest;

        public OnLoadCommand(PengeluaranLainnyaViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            object dg = DialogHelper.ShowDialogGlobalLoadingAsync(_isTest, true, "AkuntansiRootDialog", "Mohon tunggu", "sedang memproses tindakan...");

            _viewModel.IsLoading = true;

            await UpdateListData.ProcessAsync(_isTest, new List<string>()
            {
                "MasterPeriodeAkuntansi",
                "MasterWilayah",
                "MasterPerkiraan3",
            });

            _viewModel.PeriodeList = MasterListGlobal.MasterPeriodeAkuntansi;
            _viewModel.PeriodeTutupBukuList = new(MasterListGlobal.MasterPeriodeAkuntansi.Where(x => x.FlagTutupBuku == true));
            _viewModel.WilayahList = MasterListGlobal.MasterWilayah;
            _viewModel.Perkiraan3List = new ObservableCollection<MasterPerkiraan3Dto>(MasterListGlobal.MasterPerkiraan3.OrderBy(x => x.KodePerkiraan3));

            await (_viewModel.OnRefreshCommand as AsyncCommandBase)!.ExecuteAsync(null!);

            _viewModel.IsLoading = false;

            DialogHelper.CloseDialog(_isTest, true, "AkuntansiRootDialog", dg);

            await Task.FromResult(_isTest);
        }


    }
}
