using System.Collections.Generic;
using System.Threading.Tasks;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Hublang.Pelayanan;
using SiPintar.Views.Global.Dialog;

namespace SiPintar.Commands.Hublang.Pelayanan.BAPengembalian
{
    public class OnOpenPilihPelangganCommand : AsyncCommandBase
    {
        private readonly BaPengembalianViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnOpenPilihPelangganCommand(BaPengembalianViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {

            await DialogHelper.ShowCustomDialogViewAsync(_isTest, false, "HublangRootDialog", GetInstancePilihPelanggan);
            await Task.FromResult(_isTest);
        }

        public bool SetToDataLangganan()
        {
            _ = ((AsyncCommandBase)_viewModel.OnOpenAddFormCommand).ExecuteAsync(null);
            return true;
        }

        private object GetInstancePilihPelanggan() => new PilihPelangganDialog(_restApi, SetSelectedPelanggan, SetToDataLangganan, new List<JenisPelangganType>()
        {
            JenisPelangganType.AIR
        });

        public bool SetSelectedPelanggan(dynamic param)
        {
            _viewModel.SelectedPelanggan = param;
            return true;
        }
    }
}
