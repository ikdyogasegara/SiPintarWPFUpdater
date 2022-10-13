using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using AppBusiness.Data.DTOs;
using Newtonsoft.Json.Linq;
using SiPintar.Commands.Global;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Gudang.ProsesTransaksi;

namespace SiPintar.Commands.Gudang.ProsesTransaksi.TransaksiBarangKeluar
{
    public class OnSubmitPengajuanPengeluaranBarangCommand : AsyncCommandBase
    {
        private readonly TransaksiBarangKeluarViewModel _vm;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnSubmitPengajuanPengeluaranBarangCommand(TransaksiBarangKeluarViewModel vm, IRestApiClientModel restApi, bool isTest = false)
        {
            _vm = vm;
            _restApi = restApi;
            _isTest = isTest;
        }
        public override async Task ExecuteAsync(object parameter)
        {
            DialogHelper.CloseDialog(_isTest, true, "GudangRootDialog");
            object loading = DialogHelper.ShowDialogGlobalLoadingAsync(_isTest, true, "GudangRootDialog",
                "Mohon tunggu", "sedang memproses tindakan...");

            var param = new Dictionary<string, dynamic>();
            Type t = typeof(ParamPengajuanPengeluaranBarangDto);
            var props = t.GetProperties();
            foreach (var item in props)
            {
                if (item.Name != "IdPdam" && item.Name != "IdUserRequest" && item.Name != "IdPengajuanPengeluaran")
                {
                    if (item.Name == "Detail")
                    {
                        var tempDetail = new ObservableCollection<ParamPengajuanPengeluaranBarangDetailDto>();
                        foreach (var i in _vm.Form.DetailWpf)
                        {
                            tempDetail.Add(new ParamPengajuanPengeluaranBarangDetailDto()
                            {
                                IdBarang = i.IdBarang,
                                Harga_Jual = i.Harga_Jual,
                                Keterangan = i.Keterangan,
                                Qty = i.Qty
                            });
                        }
                        param.Add(item.Name, tempDetail);
                    }
                    else if (item.Name == "IdUser")
                    {
                        param.Add(item.Name, Application.Current.Resources["IdUser"]?.ToString());
                    }
                    else
                    {
                        param.Add(item.Name, item.GetValue(_vm.Form));
                    }
                }
            }

            var response = await Task.Run(() =>
                _restApi.PostAsync(
                    $"/api/{_restApi.GetApiVersion()}/pengajuan-pengeluaran-barang", param));
            DialogHelper.CloseDialog(_isTest, true, "GudangRootDialog", loading);
            if (!response.IsError)
            {
                if (response.Data.Status)
                {
                    DialogHelper.ShowSnackbar(_isTest, "success", response.Data.Ui_msg, "gudang");
                    var isCetakLaporan = parameter as bool?;
                    if (isCetakLaporan ?? false)
                    {
                        var result = response.Data.Data.ToObject<JArray>();
                        if (result?.First is JObject objResult)
                        {
                            var id = objResult.GetValue("IdPengajuanPengeluaran", StringComparison.OrdinalIgnoreCase).ToObject<int?>();
                            if (id.HasValue)
                            {
                                var bodyCetak = new Dictionary<string, dynamic>()
                                {
                                    { "IdPengajuanPengeluaran", id }
                                };
                                ((OnCetakCommand)_vm.OnCetakPengajuanCommand).IsCetak = true;
                                ((OnCetakCommand)_vm.OnCetakPengajuanCommand).IsPreview = true;
                                ((OnCetakCommand)_vm.OnCetakPengajuanCommand).TemplateName = "DaftarPengajuanPengeluaranBarang";
                                _ = ((AsyncCommandBase)_vm.OnCetakPengajuanCommand).ExecuteAsync(bodyCetak);
                            }
                        }
                    }
                    _ = ((AsyncCommandBase)_vm.OnLoadCommand).ExecuteAsync(null);
                }
                else
                {
                    DialogHelper.ShowSnackbar(_isTest, "danger", response.Data.Ui_msg, "gudang");
                }
            }
            else
            {
                DialogHelper.ShowSnackbar(_isTest, "danger", response.Error.Message, "gudang");
            }
            _vm.IsLoadingCari = false;
            await Task.FromResult(_isTest);
        }
    }
}
