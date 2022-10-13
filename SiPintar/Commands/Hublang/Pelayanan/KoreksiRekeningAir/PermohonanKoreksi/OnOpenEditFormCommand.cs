using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using MaterialDesignThemes.Wpf;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Hublang.Pelayanan.KoreksiRekeningAir;
using SiPintar.Views.Hublang.Pelayanan.KoreksiRekeningAir.PermohonanKoreksi;

namespace SiPintar.Commands.Hublang.Pelayanan.KoreksiRekeningAir.PermohonanKoreksi
{
    public class OnOpenEditFormCommand : AsyncCommandBase
    {
        private readonly PermohonanKoreksiViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnOpenEditFormCommand(PermohonanKoreksiViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_viewModel.Parent.IsLoading || _viewModel.SelectedData == null)
                return;

            object dg = DialogHelper.ShowDialogGlobalLoadingAsync(_isTest, false, "HublangRootDialog", "Mohon tunggu", "sedang memproses tindakan...", "");

            _viewModel.IsFor = parameter as string;
            _viewModel.IsCanEdit = _viewModel.IsFor == "edit";
            _viewModel.IsNewFotoBukti1 = false;
            _viewModel.IsNewFotoBukti2 = false;
            _viewModel.IsNewFotoBukti3 = false;
            _viewModel.FotoBukti1Uri = null;
            _viewModel.FotoBukti2Uri = null;
            _viewModel.FotoBukti3Uri = null;
            _viewModel.IsFotoBukti1FormChecked = false;
            _viewModel.IsFotoBukti2FormChecked = false;
            _viewModel.IsFotoBukti3FormChecked = false;

            _viewModel.AlasanForm = _viewModel.SelectedData.KeteranganWpf;
            _viewModel.BiayaForm = _viewModel.SelectedData.NonAirReguler != null ? _viewModel.SelectedData.NonAirReguler.Total : 0;

            await GetFotoAsync();

            await ((AsyncCommandBase)_viewModel.GetPiutangListCommand).ExecuteAsync(null);

            DialogHelper.CloseDialog(_isTest, false, "HublangRootDialog", dg);

            ShowDialog();

            await Task.FromResult(_isTest);
        }

        [ExcludeFromCodeCoverage]
        private void ShowDialog()
        {
            if (!_isTest)
            {
                if (DialogHost.IsDialogOpen("HublangRootDialog"))
                    DialogHost.Close("HublangRootDialog");
                _ = DialogHost.Show(new KoreksiFormView(_viewModel), "HublangRootDialog");
            }
        }

        [ExcludeFromCodeCoverage]
        private async Task GetFotoAsync()
        {
            if (!_isTest)
            {
                if (_viewModel.SelectedData.IdPermohonan == null)
                    return;

                var param = new Dictionary<string, dynamic> { { "IdPermohonan", _viewModel.SelectedData.IdPermohonan } };

                var response = await _restApi.GetAsync($"/api/{_restApi.GetApiVersion()}/permohonan-pelanggan-air-link-foto", param);
                if (!response.IsError)
                {
                    var result = response.Data;
                    if (result.Status)
                    {
                        var data = result.Data.ToObject<ObservableCollection<NamaFotoDto>>();

                        if (data != null)
                        {
                            _viewModel.SelectedData.FotoBukti1 = data[0].FotoBukti1;
                            _viewModel.SelectedData.FotoBukti2 = data[0].FotoBukti2;
                            _viewModel.SelectedData.FotoBukti3 = data[0].FotoBukti3;

                            _viewModel.FotoBukti1Uri = await Task.Run(() => ImageUriHelper.SetUriAsync(data[0].FotoBukti1));
                            _viewModel.FotoBukti2Uri = await Task.Run(() => ImageUriHelper.SetUriAsync(data[0].FotoBukti2));
                            _viewModel.FotoBukti3Uri = await Task.Run(() => ImageUriHelper.SetUriAsync(data[0].FotoBukti3));
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
