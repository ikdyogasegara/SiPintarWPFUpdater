using System.Collections.Generic;
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
    public class OnOpenEditFormCommand : AsyncCommandBase
    {
        private readonly PaketViewModel Vm;
        public readonly IRestApiClientModel RestApi;
        public readonly bool IsTest;
        private readonly string ApiVersion = Application.Current?.Resources["api_version"]?.ToString();

        public OnOpenEditFormCommand(PaketViewModel _vm, IRestApiClientModel _restApi, bool _isTest = false)
        {
            Vm = _vm;
            IsTest = _isTest;
            RestApi = _restApi;
        }
        public override async Task ExecuteAsync(object parameter)
        {
            Vm.IsAdd = false;
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

            Response = await Task.Run(() => RestApi.GetAsync($"/api/{ApiVersion}/master-barang", null));
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

            DialogHelper.CloseDialog(isTest: IsTest, dispatcher: true, Identifier: "GudangRootDialog", dialog: dg);

            _ = DialogHelper.ShowCustomDialogViewAsync(IsTest, true, "GudangRootDialog", GetInstance);
            Vm.IsLoading1 = true;
            var Param = new Dictionary<string, dynamic>();
            Param.Add("IdPaketBarang", IdPaketBarang.Value);
            Response = await Task.Run(() => RestApi.GetAsync($"/api/{ApiVersion}/master-paket-barang-detail", Param));
            if (!Response.IsError)
            {
                if (Response.Data.Status && Response.Data.Data != null)
                {
                    ObservableCollection<MasterBarangPaketDetailDto> DataBarang = Response.Data.Data.ToObject<ObservableCollection<MasterBarangPaketDetailDto>>();
                    var Temp = new ObservableCollection<MasterBarangPaketDetailDto>();
                    DataBarang.ForEach(x => Temp.Add(new MasterBarangPaketDetailDto()
                    {
                        IdBarang = x.IdBarang,
                        KodeBarang = x.KodeBarang,
                        NamaBarang = x.NamaBarang,
                        Qty = x.Qty
                    }));
                    Vm.Data1 = Temp;
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
