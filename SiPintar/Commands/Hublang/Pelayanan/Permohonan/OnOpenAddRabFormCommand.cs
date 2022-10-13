using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using Newtonsoft.Json.Linq;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Hublang.Pelayanan;
using SiPintar.Views.Hublang.Pelayanan.Permohonan;

namespace SiPintar.Commands.Hublang.Pelayanan.Permohonan
{
    public class OnOpenAddRabFormCommand : AsyncCommandBase
    {
        private readonly PermohonanViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnOpenAddRabFormCommand(PermohonanViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _viewModel.IsFor = "add";
            _viewModel.IsCariPelangganForm = true;

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

            _viewModel.DataPermohonan.Clear();
            _viewModel.DetailRabPipaPersil.Clear();
            _viewModel.DetailRabPipaDistribusi.Clear();
            _viewModel.SelectedPaketRabPipaDistribusi = null;
            _viewModel.SelectedPaketRabPipaPersil = null;
            _viewModel.OnHitungRabCommand.Execute(null);

            var dg = await DialogHelper.ShowDialogGlobalLoadingAsync(_isTest, false, "PerencanaanRootDialog", "Mohon tunggu", "sedang memproses tindakan...", "");

            if (_viewModel.SelectedTipePermohonanComboBox == null || _viewModel.SelectedTipePermohonanComboBox.IdTipePermohonan == null)
            {
                DialogHelper.CloseDialog(_isTest, false, "PerencanaanRootDialog", dg);
                await DialogHelper.ShowDialogGlobalViewAsync(
                    _isTest, false,
                    "PerencanaanRootDialog",
                    $"Tambah {_viewModel.FiturName} {_viewModel.SelectedJenisPelanggan}",
                    $"Tipe {_viewModel.FiturName} belum dipilih.",
                    "warning",
                    "Ok",
                    false,
                    "perencanaan");
            }
            else
            {
                _viewModel.DataParamPermohonan.Clear();
                _viewModel.KelurahanForm = new MasterKelurahanDto();
                _viewModel.RayonForm = new MasterRayonDto();
                _viewModel.SelectedDataPelanggan = null;

                var anyError = await SetParameterAsync();

                DialogHelper.CloseDialog(_isTest, false, "PerencanaanRootDialog", dg);

                if (!_isTest && !anyError)
                {
                    await DialogHelper.ShowCustomDialogViewAsync(_isTest, false, "PerencanaanRootDialog", GetInstance);
                }
            }

            await Task.FromResult(_isTest);
        }

        [ExcludeFromCodeCoverage]
        private async Task<bool> SetParameterAsync()
        {
            var anyError = false;

            if (!_isTest)
            {
                _viewModel.SelectedTipePermohonan = _viewModel.SelectedTipePermohonanComboBox;
                var dataParamList = _viewModel.SelectedTipePermohonan.DetailParameter.Where(x => x.IdListData != null).ToList();
                foreach (var item in dataParamList)
                {
                    var temp = new ParamPermohonanList() { Name = item.Parameter, Data = new List<ParamPermohonanData>() };

                    var response = await Task.Run(() => _restApi.GetAsync($"/api/{_restApi.GetApiVersion()}/{item.EndPoint}", null));
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
                            DialogHelper.ShowSnackbar(_isTest, response.Data.Ui_msg, "danger");
                        }
                    }
                    else
                    {
                        anyError = true;
                        DialogHelper.ShowSnackbar(_isTest, response.Error.Message, "danger");
                    }

                    _viewModel.DataParamPermohonan.Add(temp);
                }
            }

            return anyError;
        }

        [ExcludeFromCodeCoverage]
        private object GetInstance() => new DialogRabFormView(_viewModel, _restApi);
    }
}
