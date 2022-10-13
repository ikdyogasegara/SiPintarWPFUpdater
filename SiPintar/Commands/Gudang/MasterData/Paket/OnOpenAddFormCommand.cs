using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using AppBusiness.Data.DTOs;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Gudang.MasterData;
using SiPintar.Views.Gudang.MasterData.Paket;

namespace SiPintar.Commands.Gudang.MasterData.Paket
{
    public class OnOpenAddFormCommand : AsyncCommandBase
    {
        private readonly PaketViewModel Vm;
        public readonly IRestApiClientModel RestApi;
        public readonly bool IsTest;
        private readonly string ApiVersion = Application.Current?.Resources["api_version"]?.ToString();

        public OnOpenAddFormCommand(PaketViewModel _vm, IRestApiClientModel _restApi, bool _isTest = false)
        {
            Vm = _vm;
            IsTest = _isTest;
            RestApi = _restApi;
        }
        public override async Task ExecuteAsync(object parameter)
        {
            Vm.IsAdd = true;
            Vm.Data1 = new ObservableCollection<MasterBarangPaketDetailDto>();
            _ = DialogHelper.ShowCustomDialogViewAsync(IsTest, true, "GudangRootDialog", GetInstance);
            Vm.IsLoading1 = true;
            var Response = await Task.Run(() => RestApi.GetAsync($"/api/{ApiVersion}/master-barang", null));
            if (!Response.IsError)
            {
                if (Response.Data.Status && Response.Data.Data != null)
                {
                    Vm.DaftarBarang = Response.Data.Data.ToObject<ObservableCollection<MasterBarangDto>>();
                }
                else
                {
                    DialogHelper.ShowSnackbar(IsTest, "danger", Response.Data.Ui_msg, "gudang");
                }
            }
            Vm.IsLoading1 = false;
            await Task.FromResult(IsTest);
        }

        private object GetInstance() => new DialogFormView(Vm);
    }
}
