using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using MaterialDesignThemes.Wpf;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Hublang.Pelayanan;
using SiPintar.Views.Hublang.Pelayanan.Permohonan;

namespace SiPintar.Commands.Hublang.Pelayanan.Permohonan
{
    public class OnOpenEditRabFormCommand : AsyncCommandBase
    {
        private readonly PermohonanViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnOpenEditRabFormCommand(PermohonanViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _viewModel.IsFor = parameter as string;

            object dg = DialogHelper.ShowDialogGlobalLoadingAsync(_isTest, false, "PerencanaanRootDialog", "Mohon tunggu", "sedang memproses tindakan...", "");

            _viewModel.IsLoadingForm = true;
            _viewModel.IsNewFotoBukti1 = false;
            _viewModel.IsNewFotoBukti2 = false;
            _viewModel.IsNewFotoBukti3 = false;
            _viewModel.FotoBukti1Uri = null;
            _viewModel.FotoBukti2Uri = null;
            _viewModel.FotoBukti3Uri = null;
            _viewModel.IsFotoBukti1FormChecked = false;
            _viewModel.IsFotoBukti2FormChecked = false;
            _viewModel.IsFotoBukti3FormChecked = false;

            _viewModel.IsNewFotoDenah1 = false;
            _viewModel.IsNewFotoDenah2 = false;
            _viewModel.IsNewFotoBuktiRab1 = false;
            _viewModel.IsNewFotoBuktiRab2 = false;
            _viewModel.IsNewFotoBuktiRab3 = false;
            _viewModel.FotoDenah1Uri = null;
            _viewModel.FotoDenah2Uri = null;
            _viewModel.FotoBuktiRab1Uri = null;
            _viewModel.FotoBuktiRab2Uri = null;
            _viewModel.FotoBuktiRab3Uri = null;
            _viewModel.IsFotoDenah1FormChecked = false;
            _viewModel.IsFotoDenah2FormChecked = false;
            _viewModel.IsFotoBuktiRab1FormChecked = false;
            _viewModel.IsFotoBuktiRab2FormChecked = false;
            _viewModel.IsFotoBuktiRab3FormChecked = false;

            _viewModel.DataPelanggan.Clear();
            _viewModel.SelectedPegawai = new ObservableCollection<MasterPegawaiDto>();
            _viewModel.SelectedData.Pelaksana?.ForEach(x =>
            {
                _viewModel.SelectedPegawai.Add(_viewModel.PegawaiList?.FirstOrDefault(i => i.IdPegawai == x.IdPegawai));
            });

            if ((_viewModel.SelectedTipePermohonanComboBox == null || _viewModel.SelectedTipePermohonanComboBox.IdTipePermohonan == null) && _viewModel.IsFor != "detail")
            {
                DialogHelper.CloseDialog(_isTest, false, "PerencanaanRootDialog", dg);
                await DialogHelper.ShowDialogGlobalViewAsync(_isTest, false, "PerencanaanRootDialog", $"Koreksi {_viewModel.FiturName}", $"Tipe {_viewModel.FiturName} belum dipilih.", "warning", "Ok", false, "perencanaan");
            }
            else
            {
                if (_viewModel.SelectedData.StatusPermohonanText == "Selesai" && _viewModel.IsFor != "detail")
                {
                    DialogHelper.CloseDialog(_isTest, false, "PerencanaanRootDialog", dg);
                    await DialogHelper.ShowDialogGlobalViewAsync(_isTest, false,
                        "PerencanaanRootDialog",
                        "Koreksi Tidak diijinkan",
                        "Data yang sudah selesai tidak dapat dikoreksi !",
                        "warning",
                        "Batal",
                        false,
                        "perencanaan");
                }
                else if (_viewModel.SelectedData.RAB != null && _viewModel.SelectedData.RAB.NonAirRab is { StatusTransaksi: true } && _viewModel.IsFor != "detail")
                {
                    DialogHelper.CloseDialog(_isTest, false, "PerencanaanRootDialog", dg);
                    await DialogHelper.ShowDialogGlobalViewAsync(_isTest, false,
                        "PerencanaanRootDialog",
                        "Koreksi Tidak diijinkan",
                        "Tidak dapat dikoreksi karena tagihan sudah dilunasi !",
                        "warning",
                        "Batal",
                        false,
                        "perencanaan");
                }
                else if (_viewModel.SelectedData.RAB != null && _viewModel.SelectedData.RAB.NonAirRab is { FlagAngsur: true } && _viewModel.IsFor != "detail")
                {
                    DialogHelper.CloseDialog(_isTest, false, "PerencanaanRootDialog", dg);
                    await DialogHelper.ShowDialogGlobalViewAsync(_isTest, false,
                        "PerencanaanRootDialog",
                        "Koreksi Tidak diijinkan",
                        "Tidak dapat dikoreksi karena tagihan sudah diangsur !",
                        "warning",
                        "Batal",
                        false,
                        "perencanaan");
                }
                else
                {
                    _viewModel.DataParamPermohonan.Clear();
                    _viewModel.SelectedDataPermohonan = _viewModel.SelectedData;

                    var anyError = await SetParameterAsync();
                    await GetFotoAsync();

                    _viewModel.IsLoadingForm = false;

                    DialogHelper.CloseDialog(_isTest, false, "PerencanaanRootDialog", dg);

                    if (!_isTest && !anyError)
                    {
                        await DialogHost.Show(new DialogRabFormView(_viewModel, _restApi), "PerencanaanRootDialog");
                    }
                }
            }

            _viewModel.IsLoadingForm = false;
            await Task.FromResult(_isTest);
        }

        [ExcludeFromCodeCoverage]
        private async Task<bool> SetParameterAsync()
        {
            var anyError = false;

            if (!_isTest)
            {
                _viewModel.SelectedTipePermohonan = _viewModel.TipePermohonan.FirstOrDefault(x => x.IdTipePermohonan == _viewModel.SelectedDataPermohonan.IdTipePermohonan);
                if (_viewModel.SelectedTipePermohonan != null)
                {
                    var dataParamList = _viewModel.SelectedTipePermohonan.DetailParameter.Where(x => x.IdListData != null).ToList();
                    foreach (var item in dataParamList)
                    {
                        var temp = new ParamPermohonanList()
                        {
                            Name = item.Parameter,
                            Data = new List<ParamPermohonanData>()
                        };
                        var response = await _restApi.GetAsync($"/api/{_restApi.GetApiVersion()}/{item.EndPoint}", null);
                        if (!response.IsError)
                        {
                            var result = response.Data;
                            if (result.Status && result.Data != null)
                            {
                                var tempData = result.Data.ToObject<ObservableCollection<JObject>>();
                                if (tempData != null)
                                {
                                    foreach (var s in tempData)
                                    {
                                        temp.Data.Add(new ParamPermohonanData() { Key = s.GetValue(item.FieldKey, StringComparison.OrdinalIgnoreCase)?.ToString(), Value = s.GetValue(item.FieldDisplayValue2, StringComparison.OrdinalIgnoreCase)?.ToString() });
                                    }
                                }
                            }
                            else
                            {
                                anyError = true;
                                DialogHelper.ShowSnackbar(_isTest, "danger", response.Data.Ui_msg);
                            }
                        }
                        else
                        {
                            anyError = true;
                            DialogHelper.ShowSnackbar(_isTest, "danger", response.Error.Message);
                        }
                        _viewModel.DataParamPermohonan.Add(temp);
                    }
                }
            }

            return anyError;
        }

        [ExcludeFromCodeCoverage]
        private async Task GetFotoAsync()
        {
            if (!_isTest)
            {
                if (_viewModel.SelectedData.IdPermohonan == null)
                    return;

                var urlFoto = _viewModel.EndPointRab;

                var param = new Dictionary<string, dynamic> { { "IdPermohonan", _viewModel.SelectedData.IdPermohonan } };

                var response = await _restApi.GetAsync($"/api/{_restApi.GetApiVersion()}/{urlFoto}-link-foto", param);
                if (!response.IsError)
                {
                    var result = response.Data;
                    if (result.Status)
                    {
                        var data = result.Data.ToObject<ObservableCollection<NamaFotoDto>>();

                        if (data != null)
                        {
                            _viewModel.SelectedData.FotoDenah1 = data[0].FotoDenah1;
                            _viewModel.SelectedData.FotoDenah2 = data[0].FotoDenah2;
                            _viewModel.SelectedData.FotoBuktiRab1 = data[0].FotoBukti1;
                            _viewModel.SelectedData.FotoBuktiRab2 = data[0].FotoBukti2;
                            _viewModel.SelectedData.FotoBuktiRab3 = data[0].FotoBukti3;

                            _viewModel.FotoDenah1Uri = await Task.Run(() => ImageUriHelper.SetUriAsync(data[0].FotoDenah1));
                            _viewModel.FotoDenah2Uri = await Task.Run(() => ImageUriHelper.SetUriAsync(data[0].FotoDenah2));
                            _viewModel.FotoBuktiRab1Uri = await Task.Run(() => ImageUriHelper.SetUriAsync(data[0].FotoBukti1));
                            _viewModel.FotoBuktiRab2Uri = await Task.Run(() => ImageUriHelper.SetUriAsync(data[0].FotoBukti2));
                            _viewModel.FotoBuktiRab3Uri = await Task.Run(() => ImageUriHelper.SetUriAsync(data[0].FotoBukti3));
                        }
                    }
                    else
                    {
                        DialogHelper.ShowSnackbar(_isTest, "danger", response.Data.Ui_msg);
                    }
                }
                else
                {
                    DialogHelper.ShowSnackbar(_isTest, "danger", response.Error.Message);
                }
            }
        }
    }
}
