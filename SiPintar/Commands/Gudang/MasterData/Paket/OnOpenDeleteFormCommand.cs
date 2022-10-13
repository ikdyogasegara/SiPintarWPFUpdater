using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using AppBusiness.Data.DTOs;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Gudang.MasterData;

namespace SiPintar.Commands.Gudang.MasterData.Paket
{
    public class OnOpenDeleteFormCommand : AsyncCommandBase
    {
        private readonly PaketViewModel Vm;
        public readonly IRestApiClientModel RestApi;
        public readonly bool IsTest;
        private readonly string ApiVersion = Application.Current?.Resources["api_version"]?.ToString();

        public OnOpenDeleteFormCommand(PaketViewModel _vm, IRestApiClientModel _restApi, bool _isTest = false)
        {
            Vm = _vm;
            IsTest = _isTest;
            RestApi = _restApi;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            object dg = DialogHelper.ShowDialogGlobalLoadingAsync(
                isTest: IsTest,
                dispatcher: true,
                identfier: "GudangRootDialog",
                header: "Mohon tunggu",
                message1: "sedang mengambil data..."
                );
            int? IdPaketBarang = parameter as int?;
            var Response = await Task.Run(() => RestApi.GetAsync($"/api/{ApiVersion}/master-paket-barang", new Dictionary<string, dynamic>() {
                { "IdPaketBarang", IdPaketBarang.Value }
            }));
            if (!Response.IsError)
            {
                if (Response.Data.Status && Response.Data.Data != null)
                {
                    ObservableCollection<MasterBarangPaketDto> PaketTemp = Response.Data.Data.ToObject<ObservableCollection<MasterBarangPaketDto>>();
                    Vm.SelectedData = new MasterBarangPaketDto()
                    {
                        IdPaketBarang = IdPaketBarang.Value,
                        NamaPaketBarang = PaketTemp?.Count > 0 ? PaketTemp[0].NamaPaketBarang : ""
                    };
                }
                else
                {
                    DialogHelper.ShowSnackbar(IsTest, "danger", Response.Data.Ui_msg, "gudang");
                }
            }

            DialogHelper.CloseDialog(isTest: IsTest, dispatcher: true, Identifier: "GudangRootDialog", dialog: dg);

            await DialogHelper.ShowDialogGlobalYesCancelViewAsync(IsTest, true, "GudangRootDialog", "Hapus Paket",
                $"Ingin menghapus {Vm.SelectedData.NamaPaketBarang} ?",
                "confirmation", Vm.OnSubmitDeleteFormCommand, moduleName: "gudang");
            await Task.FromResult(IsTest);
        }
    }
}
