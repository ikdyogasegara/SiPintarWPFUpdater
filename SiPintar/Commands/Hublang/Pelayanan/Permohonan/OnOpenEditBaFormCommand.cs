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
    public class OnOpenEditBaFormCommand : AsyncCommandBase
    {
        private readonly PermohonanViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnOpenEditBaFormCommand(PermohonanViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _viewModel.IsFor = parameter as string;

            object dg = DialogHelper.ShowDialogGlobalLoadingAsync(_isTest, false, "DistribusiRootDialog", "Mohon tunggu", "sedang memproses tindakan...", "");

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


            _viewModel.IsNewFotoBuktiBeritaAcara1 = false;
            _viewModel.IsNewFotoBuktiBeritaAcara2 = false;
            _viewModel.IsNewFotoBuktiBeritaAcara3 = false;
            _viewModel.FotoBuktiBeritaAcara1Uri = null;
            _viewModel.FotoBuktiBeritaAcara2Uri = null;
            _viewModel.FotoBuktiBeritaAcara3Uri = null;
            _viewModel.IsFotoBuktiBeritaAcara1FormChecked = false;
            _viewModel.IsFotoBuktiBeritaAcara2FormChecked = false;
            _viewModel.IsFotoBuktiBeritaAcara3FormChecked = false;
            _viewModel.DataPelanggan.Clear();
            _viewModel.SelectedPegawai = new ObservableCollection<MasterPegawaiDto>();
            _viewModel.SelectedData.Pelaksana?.ForEach(x =>
            {
                _viewModel.SelectedPegawai.Add(_viewModel.PegawaiList?.FirstOrDefault(i => i.IdPegawai == x.IdPegawai));
            });

            if ((_viewModel.SelectedTipePermohonanComboBox == null || _viewModel.SelectedTipePermohonanComboBox.IdTipePermohonan == null) && _viewModel.IsFor != "detail")
            {
                DialogHelper.CloseDialog(_isTest, false, "DistribusiRootDialog", dg);
                await DialogHelper.ShowDialogGlobalViewAsync(_isTest, false, "DistribusiRootDialog", $"Koreksi {_viewModel.FiturName}", $"Tipe {_viewModel.FiturName} belum dipilih.", "warning", "Ok", false, "distribusi");
            }
            else
            {
                if (_viewModel.SelectedData.StatusPermohonanText == "Selesai" && _viewModel.IsFor != "detail")
                {
                    DialogHelper.CloseDialog(_isTest, false, "DistribusiRootDialog", dg);
                    await DialogHelper.ShowDialogGlobalViewAsync(_isTest, false,
                        "DistribusiRootDialog",
                        "Koreksi Tidak diijinkan",
                        "Data yang sudah selesai tidak dapat dikoreksi !",
                        "warning",
                        "Batal",
                        false,
                        "distribusi");
                }
                else
                {
                    _viewModel.DataParamPermohonan.Clear();
                    _viewModel.SelectedDataPermohonan = _viewModel.SelectedData;

                    var anyError = await SetParameterAsync();
                    await GetFotoAsync();

                    _viewModel.IsLoadingForm = false;

                    DialogHelper.CloseDialog(_isTest, false, "DistribusiRootDialog", dg);

                    if (!_isTest && !anyError)
                    {
                        await DialogHost.Show(new DialogBaFormView(_viewModel, _restApi), "DistribusiRootDialog");
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

                var urlFoto = _viewModel.EndPointBa;

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
                            _viewModel.SelectedData.FotoBuktiBeritaAcara1 = data[0].FotoBukti1;
                            _viewModel.SelectedData.FotoBuktiBeritaAcara2 = data[0].FotoBukti2;
                            _viewModel.SelectedData.FotoBuktiBeritaAcara3 = data[0].FotoBukti3;

                            _viewModel.FotoBuktiBeritaAcara1Uri = await Task.Run(() => ImageUriHelper.SetUriAsync(data[0].FotoBukti1));
                            _viewModel.FotoBuktiBeritaAcara2Uri = await Task.Run(() => ImageUriHelper.SetUriAsync(data[0].FotoBukti2));
                            _viewModel.FotoBuktiBeritaAcara3Uri = await Task.Run(() => ImageUriHelper.SetUriAsync(data[0].FotoBukti3));
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
