using System.Collections.Generic;
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
    public class OnOpenKoreksiHargaFormCommand : AsyncCommandBase
    {
        public readonly SupervisiPengajuanPembelianViewModel Vm;
        public readonly IRestApiClientModel RestApi;
        public readonly bool IsTest;
        private readonly string ApiVersion = Application.Current?.Resources["api_version"]?.ToString();

        public OnOpenKoreksiHargaFormCommand(SupervisiPengajuanPembelianViewModel _vm, IRestApiClientModel _restApi, bool _isTest = false)
        {
            Vm = _vm;
            RestApi = _restApi;
            IsTest = _isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (Vm.SelectedDataDetail == null)
            {
                await DialogHelper.ShowDialogGlobalViewAsync(IsTest, true, "GudangRootDialog", "Koreksi Harga",
                    "Data belum dipilih!", "error", "OK", false, "gudang");
            }
            else
            {
                if (Vm.SelectedDataDetail.FlagVerifikasi ?? false)
                {
                    await DialogHelper.ShowDialogGlobalViewAsync(IsTest, true, "GudangRootDialog", "Koreksi Harga",
                    "Data sudah diverifikasi!", "error", "OK", false, "gudang");
                }
                else
                {
                    object dg = DialogHelper.ShowDialogGlobalLoadingAsync(IsTest, true, "GudangRootDialog", "Mohon tunggu", "sedang mempersiapkan data...");
                    var ParamSend = new Dictionary<string, dynamic>();
                    ParamSend.Add("IdBarang", Vm.SelectedDataDetail.IdBarang);
                    var Response = await Task.Run(() => RestApi.GetAsync($"/api/{ApiVersion}/master-barang", ParamSend));
                    DialogHelper.CloseDialog(IsTest, true, "GudangRootDialog", dg);
                    if (!Response.IsError && Response.Data.Status && Response.Data.Data != null)
                    {
                        var Data = Response.Data.Data.ToObject<ObservableCollection<MasterBarangDto>>();
                        Vm.SelectedDataBarangKoreksiVerif = Data.Count > 0 ? Data[0] : null;
                    }
                    await DialogHelper.ShowCustomDialogViewAsync(isTest: IsTest, dispatcher: true, "GudangRootDialog", GetInstance);
                }
            }
            await Task.FromResult(IsTest);
        }

        private object GetInstance() => new DialogKoreksiHargaBarang(Vm);
    }
}
