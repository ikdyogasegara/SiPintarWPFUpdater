using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using AppBusiness.Data.DTOs;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Gudang.ProsesTransaksi;
using SiPintar.Views.Gudang.ProsesTransaksi.SupervisiPengajuanPembelian;

namespace SiPintar.Commands.Gudang.ProsesTransaksi.SupervisiPengajuanPembelian
{
    public class OnOpenLaporanSuratPerjanjianKerjaCommand : AsyncCommandBase
    {
        private readonly SupervisiPengajuanPembelianViewModel Vm;
        public readonly IRestApiClientModel RestApi;
        public readonly bool IsTest;
        private readonly string ApiVersion = Application.Current?.Resources["api_version"]?.ToString();

        public OnOpenLaporanSuratPerjanjianKerjaCommand(SupervisiPengajuanPembelianViewModel _vm, IRestApiClientModel _restApi, bool _isTest = false)
        {
            Vm = _vm;
            RestApi = _restApi;
            IsTest = _isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            DialogHelper.CloseDialog(IsTest, true, "GudangRootDialog");
            object dg = DialogHelper.ShowDialogGlobalLoadingAsync(IsTest, true, "GudangRootDialog", "Mohon tunggu", "sedang mempersiapkan data...");
            var Response = await Task.Run(() => RestApi.GetAsync($"/api/{ApiVersion}/master-suplier", null));
            if (!Response.IsError && Response.Data.Status && Response.Data.Data != null)
            {
                Vm.DataSuplier = Response.Data.Data.ToObject<ObservableCollection<MasterSuplierDto>>();
            }
            DialogHelper.CloseDialog(IsTest, true, "GudangRootDialog", dg);
            await DialogHelper.ShowCustomDialogViewAsync(
                    IsTest,
                    true,
                    "GudangRootDialog",
                    GetInstance
                );
            await Task.FromResult(IsTest);
        }

        private object GetInstance() => new DialogLaporanSuratPerjanjianKerjaView(Vm);
    }
}
