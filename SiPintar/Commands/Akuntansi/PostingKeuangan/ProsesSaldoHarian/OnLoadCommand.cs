using System;
using System.Threading.Tasks;
using SiPintar.Utilities;
using SiPintar.Utilities.Services.Filter;
using SiPintar.ViewModels.Akuntansi.PostingKeuangan;

namespace SiPintar.Commands.Akuntansi.PostingKeuangan.ProsesSaldoHarian
{
    public class OnLoadCommand : AsyncCommandBase
    {
        private readonly ProsesSaldoHarianViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnLoadCommand(ProsesSaldoHarianViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            // kalo ada action load page pertama kali letakkan disini ya... disini sudah otomatis dijalankan lho ketika memasuki halaman bersangkutan..

            _viewModel.IsLoading = true;

            await UpdateListData.ProcessAsync(_isTest, new()
            {
                "MasterBank",
                "MasterLoket",
            });

            _viewModel.BankList = MasterListGlobal.MasterBank;
            _viewModel.LoketList = MasterListGlobal.MasterLoket;

            _viewModel.IsEmpty = true;
            _viewModel.TglKasSebelumnya = DateTime.Now.AddDays(-1);
            _viewModel.TglKasHariIni = DateTime.Now;
            _viewModel.SelectedLoket = null;
            _viewModel.ProsesSaldoHarianForm = null;
            _viewModel.TotalPenyetoranBank = decimal.Zero;

            _viewModel.IsLoading = false;

            await Task.FromResult(_isTest);
        }
    }
}
